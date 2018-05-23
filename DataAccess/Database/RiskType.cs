using System;
using System.Collections.Generic;

namespace DataAccess.Database
{
    public partial class RiskType
    {
        public RiskType()
        {
            Policy = new HashSet<Policy>();
        }

        public int RiskTypeId { get; set; }
        public string Name { get; set; }

        public ICollection<Policy> Policy { get; set; }
    }
}
