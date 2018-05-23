using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SuperZapatosGAP.Auth
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class BasicAuthorizeAttribute : TypeFilterAttribute
	{
		public BasicAuthorizeAttribute(string username, string password)
			: base(typeof(BasicAuthorizeFilter))
		{
			Arguments = new object[]
			{
				username,
				password
			};
		}
	}
}
