using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GAPSeguros.Auth;
using Microsoft.AspNetCore.Mvc;

namespace GAPSeguros.Controllers
{
	[RoleAuthorize(DataAccess.Enums.Role.Admin)]
	public class BaseController : Controller
	{

	}
}