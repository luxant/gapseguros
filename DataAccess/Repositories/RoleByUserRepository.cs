using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class RoleByUserRepository : IRoleByUserRepository
	{
		private readonly GAPSegurosContext _context;

		public RoleByUserRepository(GAPSegurosContext context)
		{
			_context = context;
		}

		public async Task<RoleByUser> Create(RoleByUser model)
		{
			_context.Add(model);
			await _context.SaveChangesAsync();

			return model;
		}

		public async Task DeleteById(int id)
		{
			var entity = await _context.RoleByUser.SingleOrDefaultAsync(m => m.RoleByUserId == id);
			_context.RoleByUser.Remove(entity);
			await _context.SaveChangesAsync();
		}

		public Task<IQueryable<RoleByUser>> GetAll()
		{
			var result = _context.RoleByUser
				.Include(r => r.Role)
				.Include(r => r.User)
				.AsQueryable();

			return Task.FromResult(result);
		}

		public Task<RoleByUser> GetById(int? id)
		{
			return _context.RoleByUser
				.Include(r => r.Role)
				.Include(r => r.User)
				.SingleOrDefaultAsync(m => m.RoleByUserId == id);
		}

		public async Task Update(RoleByUser model)
		{
			_context.Update(model);
			await _context.SaveChangesAsync();
		}

		public Task<IQueryable<RoleByUser>> GetUserRoles(int userId)
		{
			return Task.FromResult(_context.RoleByUser
				.Where(m => m.UserId == userId));
		}
	}
}
