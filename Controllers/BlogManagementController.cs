using BlogManagementApi.Helper;
using BloManagementApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.Common;
using System.Linq;
using System.Net;

namespace BlogManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogManagementController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Blog>> Get()
        {
            return JsonFileHelper.ReadFromJsonFile<Blog>();
        }

        [HttpGet("{blogId}")]
        public ActionResult<Blog> Get(int blogId)
        {
            var blog = JsonFileHelper.ReadFromJsonFile<Blog>();
            var blogs = blog.FirstOrDefault(p => p.blogId == blogId);
            if (blogs == null)
            {
                return NotFound("Requested record (Blog No - " + blogId + ") is not available.");
            }
            return blogs;
        }

        [HttpPost]
        public ActionResult<Blog> Post([FromBody] Blog newblog)
        {
            var blog = JsonFileHelper.ReadFromJsonFile<Blog>();
            var lastIndex = blog.Count();
            newblog.blogId = (lastIndex + 1);
            blog.Add(newblog);
            JsonFileHelper.WriteToJsonFile(blog);
            if (newblog.blogId <= lastIndex)
            {
                return BadRequest("Requested record are not valid.");
            }
            return Ok("Successfully saved the record.");//CreatedAtAction(nameof(GetBlog), new { blogId = newblog.blogId }, newblog);
        }

        [HttpPut("{blogId}")]
        public ActionResult Put(int blogId, [FromBody] Blog updatedblog)
        {
            if (blogId != updatedblog.blogId)
            {
                return BadRequest("Blog blogId " + blogId + " is not matching");
            }
            var blog = JsonFileHelper.ReadFromJsonFile<Blog>();
            var blogs = blog.FirstOrDefault(p => p.blogId == blogId);

            if (blogs == null)
            {
                return NotFound("Requested record (Blog blogId" + blogId + ") is not exist for update.");
            }

            blogs.Username = updatedblog.Username;
            blogs.DateCreated = updatedblog.DateCreated;
            blogs.BlogText = updatedblog.BlogText;
            JsonFileHelper.WriteToJsonFile(blog);

            return Ok("Blog blogId " + blogId + " has updated"); //NoContent();
        }

        [HttpDelete("{blogId}")]
        public ActionResult Delete(int blogId)
        {
            var blog = JsonFileHelper.ReadFromJsonFile<Blog>();
            var blogs = blog.FirstOrDefault(p => p.blogId == blogId);

            if (blogs == null)
            {
                return NotFound("Requested record (Blog blogId" + blogId + ") is not exist for delete.");
            }

            blog.Remove(blogs);
            JsonFileHelper.WriteToJsonFile(blog);

            return Ok("Blog blogId " + blogId + " has deleted."); //StatusCode(StatusCodes.Status204NoContent); //NoContent();
        }
    }
}