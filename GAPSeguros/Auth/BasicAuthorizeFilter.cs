using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DTO;
using DataAccess.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GAPSeguros.Auth
{
	public class BasicAuthorizeFilter : IAuthorizationFilter
	{
		private readonly string username;
		private readonly string password;

		public BasicAuthorizeFilter(string username, string password)
		{
			this.username = username;
			this.password = password;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			string authHeader = context.HttpContext.Request.Headers["Authorization"];
			if (authHeader != null && authHeader.StartsWith("Basic "))
			{
				// Get the encoded username and password
				var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
				// Decode from Base64 to string
				var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
				// Split username and password
				var username = decodedUsernamePassword.Split(':', 2)[0];
				var password = decodedUsernamePassword.Split(':', 2)[1];
				// Check if login is correct
				if (IsAuthorized(username, password))
				{
					return;
				}
			}

			// Return unauthorized
			context.Result = new UnauthorizedResult();
		}

		// Make your own implementation of this
		public bool IsAuthorized(string username, string password)
		{
			// Check that username and password are correct
			return this.username.Equals(username, StringComparison.InvariantCultureIgnoreCase)
				   && this.password.Equals(password);
		}
	}
}
