using System;
using System.Collections.Generic;
using System.Text;

namespace SeDevOps.Data.Entities;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    // Navigation
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<Note> Notes { get; set; }
}
