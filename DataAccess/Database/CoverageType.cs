using System;
using System.Collections.Generic;

namespace DataAccess.Database
{
    public partial class CoverageType
    {
        public CoverageType()
        {
            CoverageTypeByPolicy = new HashSet<CoverageTypeByPolicy>();
        }

        public int CoverageTypeId { get; set; }
        public string Name { get; set; }

        public ICollection<CoverageTypeByPolicy> CoverageTypeByPolicy { get; set; }
    }
}
