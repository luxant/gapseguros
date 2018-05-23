using System;
using System.Collections.Generic;

namespace DataAccess.Database
{
    public partial class User
    {
        public User()
        {
            PolicyByUser = new HashSet<PolicyByUser>();
            RoleByUser = new HashSet<RoleByUser>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }

        public ICollection<PolicyByUser> PolicyByUser { get; set; }
        public ICollection<RoleByUser> RoleByUser { get; set; }
    }
}
