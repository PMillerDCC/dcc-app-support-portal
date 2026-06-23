using System.ComponentModel.DataAnnotations;

namespace AppSupportPortal.Web.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Display(Name = "Role Type")]
        public string RoleName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
