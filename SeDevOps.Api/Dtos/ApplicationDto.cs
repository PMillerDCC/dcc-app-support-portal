using System;

namespace SeDevOps.Api.Dtos;

public class ApplicationDto
{
    public int ApplicationId { get; set; }
    public string ApplicationName { get; set; }
    public string ApplicationDescription { get; set; }
    public string ApplicationDeveloper { get; set; }
    public int ServerId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
