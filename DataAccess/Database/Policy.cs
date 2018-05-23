using System;
using System.Collections.Generic;

namespace DataAccess.Database
{
    public partial class Policy
    {
        public Policy()
        {
            CoverageByPolicy = new HashSet<CoverageByPolicy>();
            PolicyByUser = new HashSet<PolicyByUser>();
        }

        public int PolicyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ValidityStart { get; set; }
        public int CoverageSpan { get; set; }
        public decimal Price { get; set; }
        public int RiskTypeId { get; set; }

        public RiskType RiskType { get; set; }
        public ICollection<CoverageByPolicy> CoverageByPolicy { get; set; }
        public ICollection<PolicyByUser> PolicyByUser { get; set; }
    }
}
