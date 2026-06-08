using System.ComponentModel.DataAnnotations;

namespace AppSupportPortal.Web.Models;

public class ApplicationViewModel
{
    public int ApplicationId { get; set; }

    [Required(ErrorMessage = "Application name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string ApplicationName { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string ApplicationDescription { get; set; }

    [Required(ErrorMessage = "Developer name is required.")]
    [StringLength(100, ErrorMessage = "Developer name cannot exceed 100 characters.")]
    public string ApplicationDeveloper { get; set; }

    [Required(ErrorMessage = "Please select a server.")]
    public int ServerId { get; set; }

    public IEnumerable<ServerViewModel>? Servers { get; set; }
}
