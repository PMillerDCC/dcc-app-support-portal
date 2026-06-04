using System;
using System.Collections.Generic;
using System.Text;

namespace SeDevOps.Data.Entities;

    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }

        // Navigation
        public ICollection<UserRole> UserRoles { get; set; }
    }

