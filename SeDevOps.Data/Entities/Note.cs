using System;
using System.Collections.Generic;
using System.Text;

namespace SeDevOps.Data.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }
        public User User { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
