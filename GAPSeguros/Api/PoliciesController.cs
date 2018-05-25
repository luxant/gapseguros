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
	public class PoliciesController : Controller
	{
		private readonly IPolicyRepository _policyRepository;

		public PoliciesController(IPolicyRepository policyRepository)
		{
			_policyRepository = policyRepository;
		}

		//GET: api/<controller>
		[HttpGet("GetAll")]
		public async Task<object> GetAll()
		{
			var result = await _policyRepository.GetAll();

			return result
				.Select(x => AutoMapper.Mapper.Map<Policy, PolicyDTO>(x))
				.ToList();
		}
	}
}
