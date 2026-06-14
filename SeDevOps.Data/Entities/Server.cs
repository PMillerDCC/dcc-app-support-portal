using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace SeDevOps.Data.Entities
{
    public class Server
    {
        public int Id { get; set; }
        public string Hostname { get; set; } = string.Empty;
        public string IPAddress { get; set; } = string.Empty;

        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
