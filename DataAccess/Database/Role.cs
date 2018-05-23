using System;
using System.Collections.Generic;

namespace DataAccess.Database
{
    public partial class Role
    {
        public Role()
        {
            RoleByUser = new HashSet<RoleByUser>();
        }

        public int RoleId { get; set; }
        public string Name { get; set; }

        public ICollection<RoleByUser> RoleByUser { get; set; }
    }
}
