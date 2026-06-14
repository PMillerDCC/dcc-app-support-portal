using System;
using System.Collections.Generic;
using System.Text;

namespace SeDevOps.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
