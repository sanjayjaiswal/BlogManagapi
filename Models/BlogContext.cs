using BloManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogManagementApi.Models
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        public DbSet<Blog>? blogs { get; set; } = null;
    }
}
