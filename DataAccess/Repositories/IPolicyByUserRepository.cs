using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public interface IPolicyByUserRepository : IRepository<PolicyByUser>
	{
		IQueryable<PolicyByUser> GetPolicyAssignations(int policyId);
	}
}
