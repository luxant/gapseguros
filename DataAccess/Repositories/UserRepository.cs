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
	public class UserRepository : IUserRepository
	{
		private readonly GAPSegurosContext _context;

		public UserRepository(GAPSegurosContext context)
		{
			_context = context;
		}

		public async Task<User> Create(User model)
		{
			// We convert the password into an SHA1 hash
			var sha1Provider = new SHA1CryptoServiceProvider();

			var result = sha1Provider.ComputeHash(Encoding.UTF8.GetBytes(model.Password));

			model.Password = BitConverter.ToString(result).Replace("-", string.Empty).ToLowerInvariant();

			//_context.Add(model);
			//await _context.SaveChangesAsync();

			return model;
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

		public Task<User> GetByUserName(string userName)
		{
			return _context.User
				.SingleOrDefaultAsync(m => m.Name == userName);
		}

		public Task<User> GetByUserNameAndRoles(string userName)
		{
			return _context.User
				.Include(x => x.RoleByUser)
				.SingleOrDefaultAsync(m => m.Name == userName);
		}

		public Task<IQueryable<User>> SearchUsersByTerm(string serachTerm)
		{
			var result = _context.User
				.Include(x => x.PolicyByUser)
					.ThenInclude(x => x.Policy) // Include user already assigned policies
				.Where(x => x.Name.Contains(serachTerm));

			return Task.FromResult(result);
		}

		public async Task Update(User model)
		{
			_context.Update(model);
			await _context.SaveChangesAsync();
		}

		public Task<User> ValidateUserNameAndPassword(string name, string password)
		{
			return _context.User
				.Include(x => x.RoleByUser)
				.FirstOrDefaultAsync(x => x.Name.Contains(name) && x.Password == password);
		}
	}
}
