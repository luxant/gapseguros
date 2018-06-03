using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public interface IRoleByUserRepository : IRepository<RoleByUser>
	{
		Task<IQueryable<RoleByUser>> GetUserRoles(int userId);
	}
}
