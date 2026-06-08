using System;

namespace SeDevOps.Api.Dtos;

public class ServerDto
{
    public int ServerId { get; set; }
    public string ServerName { get; set; }
    public string ServerDescription { get; set; }
    public string ServerStatus { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
