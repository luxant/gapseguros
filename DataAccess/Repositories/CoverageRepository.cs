using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class CoverageRepository : ICoverageRepository
	{
		private readonly GAPSegurosContext _context;

		public CoverageRepository(GAPSegurosContext context)
		{
			_context = context;
		}

		public Task<IQueryable<Coverage>> GetAll()
		{
			var result = _context.Coverage
				.AsQueryable();

			return Task.FromResult(result);
		}
	}
}
