using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class RiskTypeRepository : IRiskTypeRepository
	{
		private readonly GAPSegurosContext _context;

		public RiskTypeRepository(GAPSegurosContext context)
		{
			_context = context;
		}

		public async Task<RiskType> Create(RiskType model)
		{
			_context.Add(model);
			await _context.SaveChangesAsync();

			return model;
		}

		public async Task DeleteById(int id)
		{
			var policy = await _context.RiskType.SingleOrDefaultAsync(m => m.RiskTypeId == id);
			_context.RiskType.Remove(policy);
			await _context.SaveChangesAsync();
		}

		public Task<IQueryable<RiskType>> GetAll()
		{
			var result = _context.RiskType.AsQueryable();

			return Task.FromResult(result);
		}

		public Task<RiskType> GetById(int? id)
		{
			return _context.RiskType
				.SingleOrDefaultAsync(m => m.RiskTypeId == id);
		}

		public async Task Update(RiskType model)
		{
			_context.Update(model);
			await _context.SaveChangesAsync();
		}
	}
}
