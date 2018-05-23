using System;
using System.Collections.Generic;

namespace DataAccess.Database
{
    public partial class RoleByUser
    {
        public int RoleByUserId { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }

        public Role Role { get; set; }
        public User User { get; set; }
    }
}
