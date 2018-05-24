using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly GAPSegurosContext _context;

		public UserRepository(GAPSegurosContext context)
		{
			_context = context;
		}

		public async Task Create(User model)
		{
			_context.Add(model);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteById(int id)
		{
			var policy = await _context.User.SingleOrDefaultAsync(m => m.UserId == id);
			_context.User.Remove(policy);
			await _context.SaveChangesAsync();
		}

		public Task<IQueryable<User>> GetAll()
		{
			var result = _context.User.AsQueryable();

			return Task.FromResult(result);
		}

		public Task<User> GetById(int? id)
		{
			return _context.User
				.SingleOrDefaultAsync(m => m.UserId == id);
		}

		public async Task Update(User model)
		{
			_context.Update(model);
			await _context.SaveChangesAsync();
		}
	}
}
