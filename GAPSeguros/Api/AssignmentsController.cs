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
	public class AssignmentsController : Controller
	{
		private readonly IPolicyByUserRepository _policyByUsersRepository;

		public AssignmentsController(IPolicyByUserRepository policyByUsersRepository)
		{
			_policyByUsersRepository = policyByUsersRepository;
		}

		//GET: api/<controller>
		[HttpPost("assignPolicyToUser/{userId:int}/{policyId:int}")]
		public async Task<object> AssignPolicyToUser(int userId, int policyId)
		{
			var policyByUser = new PolicyByUser() { PolicyId = policyId, UserId = userId };

			var result = await _policyByUsersRepository.Create(policyByUser);

			return new ResponseDTO()
			{
				Success = true,
				Data = AutoMapper.Mapper.Map<PolicyByUser, PolicyByUserDTO>(result)
			};
		}

		//GET: api/<controller>
		[HttpDelete("unassignPolicyToUser/{policyByUserId:int}")]
		public async Task<object> UnassignPolicyToUser(int policyByUserId)
		{
			await _policyByUsersRepository.DeleteById(policyByUserId);

			return new ResponseDTO()
			{
				Success = true,
			};
		}
	}
}
