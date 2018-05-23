using System;
using System.Collections.Generic;

namespace DataAccess.Database
{
    public partial class CoverageByPolicy
    {
        public int CoverageByPolicyId { get; set; }
        public int PolicyId { get; set; }
        public int CoverageId { get; set; }

        public Coverage Coverage { get; set; }
        public Policy Policy { get; set; }
    }
}
