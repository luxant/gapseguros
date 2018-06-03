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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GAPSeguros.Auth
{
	public class RoleAuthorizeFilter : IAuthorizationFilter
	{
		private readonly IRoleByUserRepository _roleByUserRepository;
		private readonly Role[] _roles;

		public RoleAuthorizeFilter(IRoleByUserRepository roleByUserRepository, params DataAccess.Enums.Role[] roles)
		{
			_roleByUserRepository = roleByUserRepository;
			_roles = roles;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (ActionHasAllowAnonymousAttributed(context))
			{
				return;
			}

			var userNameType = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

			if (userNameType == null)
			{
				// Return unauthorized
				context.Result = new ForbidResult();
				return;
			}

			// We check the user exist in the database
			var userRoles = _roleByUserRepository.GetUserRoles(int.Parse(userNameType.Value)).Result;

			if (!userRoles.Any())
			{
				// Return unauthorized
				context.Result = new ForbidResult();
				return;
			}

			var allowedRolesId = _roles.Select(x => (int)x);
			var userRoleIds = userRoles.Select(x => x.RoleId);

			// We check that any of the role the user has is allowed for the action invoked
			if (userRoleIds.Any(userRoleId => allowedRolesId.Contains(userRoleId)))
			{
				return;
			}

			// Return unauthorized
			context.Result = new ForbidResult();
		}

		private bool ActionHasAllowAnonymousAttributed(AuthorizationFilterContext context)
		{
			var actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;

			var actionHasAnonymousAttribute = actionDescriptor.MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any();

			return actionHasAnonymousAttribute;
		}
	}
}
