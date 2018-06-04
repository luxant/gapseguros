using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class CoverageTypeRepository : ICoverageTypeRepository
	{
		private readonly GAPSegurosContext _context;

		public CoverageTypeRepository(GAPSegurosContext context)
		{
			_context = context;
		}

		public async Task<CoverageType> Create(CoverageType model)
		{
			_context.Add(model);
			await _context.SaveChangesAsync();

			return model;
		}

		public async Task DeleteById(int id)
		{
			var policy = await _context.CoverageType.SingleOrDefaultAsync(m => m.CoverageTypeId == id);
			_context.CoverageType.Remove(policy);
			await _context.SaveChangesAsync();
		}

		public Task<IQueryable<CoverageType>> GetAll()
		{
			var result = _context.CoverageType.AsQueryable();

			return Task.FromResult(result);
		}

		public Task<CoverageType> GetById(int? id)
		{
			return _context.CoverageType
				.SingleOrDefaultAsync(m => m.CoverageTypeId == id);
		}

		public async Task Update(CoverageType model)
		{
			_context.Update(model);
			await _context.SaveChangesAsync();
		}
	}
}
