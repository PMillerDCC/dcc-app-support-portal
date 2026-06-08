using System.ComponentModel.DataAnnotations;

namespace AppSupportPortal.Web.Models;

public class NoteViewModel
{
    public int NoteId { get; set; }

    [Required(ErrorMessage = "Note text is required.")]
    [StringLength(500, ErrorMessage = "Note cannot exceed 500 characters.")]
    public string NoteText { get; set; }

    [Required(ErrorMessage = "Please select an application.")]
    public int ApplicationId { get; set; }

    [Required(ErrorMessage = "Please select a user.")]
    public int UserId { get; set; }

    public IEnumerable<ApplicationViewModel>? Applications { get; set; }
    public IEnumerable<UserViewModel>? Users { get; set; }
}