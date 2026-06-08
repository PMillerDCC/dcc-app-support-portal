using System;

namespace SeDevOps.Api.Dtos;

public class NoteDto
{
    public int NoteId { get; set; }
    public string NoteText { get; set; }
    public DateTime CreatedDate { get; set; }
    public int ApplicationId { get; set; }
    public int UserId { get; set; }
}
