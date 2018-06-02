using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GAPSeguros.Auth
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class RoleAuthorizeAttribute : TypeFilterAttribute
	{
		public RoleAuthorizeAttribute(params DataAccess.Enums.Role[] roles)
			: base(typeof(RoleAuthorizeFilter))
		{
			Arguments = new object[]
			{
				roles
			};
		}
	}
}
