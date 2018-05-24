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

		public async Task<Policy> Create(Policy model)
		{
			_context.Add(model);
			await _context.SaveChangesAsync();

			return model;
		}

		public async Task DeleteById(int id)
		{
			var policy = await _context.Policy.SingleOrDefaultAsync(m => m.PolicyId == id);
			_context.Policy.Remove(policy);
			await _context.SaveChangesAsync();
		}

		public Task<IQueryable<Policy>> GetAll()
		{
			var result = _context.Policy
				.Include(x => x.RiskType)
				.AsQueryable();

			return Task.FromResult(result);
		}

		public Task<Policy> GetById(int? id)
		{
			return _context.Policy
				.Include(p => p.RiskType)
				.SingleOrDefaultAsync(m => m.PolicyId == id);
		}

		public async Task Update(Policy model)
		{
			_context.Update(model);
			await _context.SaveChangesAsync();
		}
	}
}
