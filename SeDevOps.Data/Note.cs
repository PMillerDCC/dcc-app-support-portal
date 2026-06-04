using System;
using System.Collections.Generic;
using System.Text;

namespace SeDevOps.Data.Entities;

    public class Note
    {
        public int NoteId { get; set; }
        public string NoteText { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }

