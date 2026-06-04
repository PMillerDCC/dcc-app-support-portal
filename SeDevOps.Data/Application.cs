using System;
using System.Collections.Generic;
using System.Text;

namespace SeDevOps.Data.Entities;

public class Application
{
    public int ApplicationId { get; set; }
    public string ApplicationName { get; set; }
    public string ApplicationDescription { get; set; }
    public string ApplicationDeveloper { get; set; }

    public int ServerId { get; set; }
    public Server Server { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    // Navigation
    public ICollection<Note> Notes { get; set; }
}
