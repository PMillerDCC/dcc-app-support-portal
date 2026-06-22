using System.ComponentModel.DataAnnotations;

namespace AppSupportPortal.Web.Models;

public class ServerViewModel
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Hostname { get; set; }
    [Required]
    public string IPAddress { get; set; }
}
