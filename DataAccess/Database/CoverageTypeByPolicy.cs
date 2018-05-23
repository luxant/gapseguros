using System;
using System.Collections.Generic;

namespace DataAccess.Database
{
    public partial class CoverageTypeByPolicy
    {
        public int CoverageTypeByPolicyId { get; set; }
        public int CoverageTypeId { get; set; }
        public int PolicyId { get; set; }

        public CoverageType CoverageType { get; set; }
        public Policy Policy { get; set; }
    }
}
