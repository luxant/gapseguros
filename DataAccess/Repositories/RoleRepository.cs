using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class RoleRepository : IRoleRepository
	{
		private readonly GAPSegurosContext _context;

		public RoleRepository(GAPSegurosContext context)
		{
			_context = context;
		}

		public async Task<Role> Create(Role model)
		{
			_context.Add(model);
			await _context.SaveChangesAsync();

			return model;
		}

		public async Task DeleteById(int id)
		{
			var model = await _context.Role.SingleOrDefaultAsync(m => m.RoleId == id);
			_context.Role.Remove(model);
			await _context.SaveChangesAsync();
		}

		public Task<IQueryable<Role>> GetAll()
		{
			var result = _context.Role.AsQueryable();

			return Task.FromResult(result);
		}

		public Task<Role> GetById(int? id)
		{
			return _context.Role
				.SingleOrDefaultAsync(m => m.RoleId == id);
		}

		public async Task Update(Role model)
		{
			_context.Update(model);
			await _context.SaveChangesAsync();
		}
	}
}
