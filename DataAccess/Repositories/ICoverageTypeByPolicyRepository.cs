using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public interface ICoverageTypeByPolicyRepository : IRepository<CoverageTypeByPolicy>
	{
		IQueryable<CoverageTypeByPolicy> GetByCoverageTypeId(int coverageTypeId);
		IQueryable<CoverageTypeByPolicy> GetPolicyAssignations(int policyId);
	}
}
