using System;
using System.Collections.Generic;
using System.Text;

namespace SeDevOps.Data.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int ServerId { get; set; }
        public Server? Server { get; set; }

        public ICollection<Note>? Notes { get; set; } = new List<Note>();
    }
}
