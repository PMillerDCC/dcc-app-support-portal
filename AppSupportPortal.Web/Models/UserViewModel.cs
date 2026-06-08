using System.ComponentModel.DataAnnotations;

namespace AppSupportPortal.Web.Models;

public class UserViewModel
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "User name is required.")]
    [StringLength(100)]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }
}
