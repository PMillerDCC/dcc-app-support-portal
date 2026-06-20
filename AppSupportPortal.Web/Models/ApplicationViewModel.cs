using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AppSupportPortal.Web.Models;

public class ApplicationViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter a name.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Please enter a description.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Please select a server.")]
    public int? ServerId { get; set; }

    public string? ServerName { get; set; }

    public IEnumerable<SelectListItem>? Servers { get; set; }
}
