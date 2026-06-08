using System.ComponentModel.DataAnnotations;

namespace AppSupportPortal.Web.Models;

public class ServerViewModel
{
    public int ServerId { get; set; }

    [Required(ErrorMessage = "Server name is required.")]
    [StringLength(100)]
    public string ServerName { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(300)]
    public string ServerDescription { get; set; }

    [Required]
    public string ServerStatus { get; set; }
}
