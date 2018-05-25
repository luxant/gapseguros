using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Database;
using DataAccess.DTO;
using DataAccess.Repositories;
using GAPSeguros.Auth;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GAPSeguros.Api
{
	[Route("api/[controller]")]
	[BasicAuthorize("my_username", "my_password")]
	public class UsersController : Controller
	{
		private readonly IUserRepository _userRepository;

		public UsersController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		//GET: api/<controller>
		[HttpGet("GetAll/{searchTerm?}")]
		public async Task<IEnumerable<UserDTO>> GetAll(string searchTerm)
		{
			var result = await _userRepository.SearchUsersByTerm(searchTerm);

			return result
				.Select(x => AutoMapper.Mapper.Map<User, UserDTO>(x))
				.ToList();
		}
	}
}
