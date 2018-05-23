using System;
using System.Collections.Generic;

namespace DataAccess.Database
{
    public partial class PolicyByUser
    {
        public int PolicyByUserId { get; set; }
        public int PolicyId { get; set; }
        public int UserId { get; set; }

        public Policy Policy { get; set; }
        public User User { get; set; }
    }
}
