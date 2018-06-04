using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GAPSeguros.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GAPSeguros.Controllers
{
	[Authorize(Policy = "AdministratorsOnlyAllowed")]
	public class BaseController : Controller
	{

	}
}