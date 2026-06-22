using System.ComponentModel.DataAnnotations;

namespace AppSupportPortal.Web.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
