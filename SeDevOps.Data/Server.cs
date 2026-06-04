using System;
using System.Collections.Generic;
using System.Text;

namespace SeDevOps.Data.Entities;

    public class Server
    {
        public int ServerId { get; set; }
        public string ServerName { get; set; }
        public string ServerDescription { get; set; }
        public string ServerStatus { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        // Navigation
        public ICollection<Application> Applications { get; set; }
    }

