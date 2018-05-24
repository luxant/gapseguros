using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		Task<IQueryable<User>> SearchUsersByTerm(string serachTerm);
	}
}
