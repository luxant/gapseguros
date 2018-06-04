using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class CoverageTypeByPolicyRepository : ICoverageTypeByPolicyRepository
	{
		private readonly GAPSegurosContext _context;

		public CoverageTypeByPolicyRepository(GAPSegurosContext context)
		{
			_context = context;
		}

		public async Task<CoverageTypeByPolicy> Create(CoverageTypeByPolicy model)
		{
			_context.Add(model);
			await _context.SaveChangesAsync();

			return model;
		}

		public async Task DeleteById(int id)
		{
			var coverageTypeByPolicy = await _context.CoverageTypeByPolicy.SingleOrDefaultAsync(m => m.CoverageTypeByPolicyId == id);
			_context.CoverageTypeByPolicy.Remove(coverageTypeByPolicy);
			await _context.SaveChangesAsync();
		}

		public Task<IQueryable<CoverageTypeByPolicy>> GetAll()
		{
			var result = _context.CoverageTypeByPolicy
				.Include(p => p.Policy)
				.Include(p => p.CoverageType)
				.AsQueryable();

			return Task.FromResult(result);
		}

		public IQueryable<CoverageTypeByPolicy> GetByCoverageTypeId(int coverageTypeId)
		{
			return _context.CoverageTypeByPolicy.Where(x => x.CoverageTypeId == coverageTypeId);
		}

		public Task<CoverageTypeByPolicy> GetById(int? id)
		{
			return _context.CoverageTypeByPolicy
				.Include(p => p.Policy)
				.Include(p => p.CoverageType)
				.SingleOrDefaultAsync(m => m.CoverageTypeByPolicyId == id);
		}

		public IQueryable<CoverageTypeByPolicy> GetPolicyAssignations(int policyId)
		{
			return _context.CoverageTypeByPolicy.Where(x => x.PolicyId == policyId);
		}

		public async Task Update(CoverageTypeByPolicy model)
		{
			_context.Update(model);
			await _context.SaveChangesAsync();
		}
	}
}
