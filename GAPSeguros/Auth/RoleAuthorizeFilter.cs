using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DTO;
using DataAccess.Enums;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GAPSeguros.Auth
{
	public class RoleAuthorizeFilter : IAuthorizationFilter
	{
		private readonly IUserRepository _userRepository;
		private readonly Role[] _roles;

		public RoleAuthorizeFilter(IUserRepository userRepository, params DataAccess.Enums.Role[] roles)
		{
			_userRepository = userRepository;
			_roles = roles;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var userNameType = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

			if (userNameType == null)
			{
				// Return unauthorized
				context.Result = new ForbidResult();
				return;
			}

			// We check the user exist in the database
			var user = _userRepository.GetByUserNameAndRoles(userNameType.Value).Result;

			if (user == null)
			{
				// Return unauthorized
				context.Result = new ForbidResult();
				return;
			}

			var allowedRolesId = _roles.Select(x => (int)x);
			var userRoleIds = user.RoleByUser.Select(x => x.RoleId);

			// We check that any of the role the user has is allowed for the action invoked
			if (userRoleIds.Any(userRoleId => allowedRolesId.Contains(userRoleId)))
			{
				return;
			}

			// Return unauthorized
			context.Result = new ForbidResult();
		}
	}
}
