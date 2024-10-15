using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloManagementApi.Models
{
    public class Blog
    {
        public int blogId { get; set; }
        public string? Username { get; set; }
        public DateTime DateCreated { get; set; }
        public string? BlogText { get; set; }
    }
}
