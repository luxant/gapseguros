using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GAPSeguros.Controllers
{
	public class AssignmentsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}