using System;
using System.Collections.Generic;

namespace DataAccess.Database
{
    public partial class Coverage
    {
        public Coverage()
        {
            CoverageByPolicy = new HashSet<CoverageByPolicy>();
        }

        public int CoverageId { get; set; }
        public string Name { get; set; }

        public ICollection<CoverageByPolicy> CoverageByPolicy { get; set; }
    }
}
