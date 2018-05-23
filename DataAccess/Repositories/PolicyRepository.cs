using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class PolicyRepository : IPolicyRepository
	{
		private readonly GAPSegurosContext _context;

		public PolicyRepository(GAPSegurosContext context)
		{
			_context = context;
		}

		public Task<IQueryable<Policy>> GetAll()
		{
			var result = _context.Policy
				.Include(x => x.RiskType)
				.AsQueryable();

			return Task.FromResult(result);
		}
	}
}
