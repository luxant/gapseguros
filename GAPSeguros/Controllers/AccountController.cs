using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Database;
using DataAccess.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace GAPSeguros.Controllers
{
	public class AccountController : BaseController
	{
		private readonly IUserRepository _userRepository;

		public AccountController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		// GET: Policies
		public async Task<IActionResult> AccessDenied()
		{
			return View();
		}

		// POST: Account/AccessDenied
		[HttpPost]
		public async Task<IActionResult> AccessDenied([Bind("Name,Password")] User user, string returnUrl)
		{
			var result = await _userRepository.ValidateUserNameAndPassword(user.Name, user.Password);

			if (result != null)
			{
				// Since the login was successful, we autenticate the user
				var claims = new List<Claim> {
					new Claim(ClaimTypes.NameIdentifier, user.Name),
				};

				var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

				await HttpContext.SignInAsync(
				   CookieAuthenticationDefaults.AuthenticationScheme,
				   new ClaimsPrincipal(claimsIdentity),
				   new AuthenticationProperties());


				return Redirect(returnUrl);
			}

			ModelState.AddModelError("", "User name or/and password is/are invalid");

			return View();
		}


		// GET: Users
		public async Task<IActionResult> Index()
		{
			return View(await _userRepository.GetAll());
		}

		// GET: Users/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = await _userRepository.GetById(id);
			if (user == null)
			{
				return NotFound();
			}

			return View(user);
		}

		// GET: Users/Create
		public async Task<IActionResult> Create()
		{
			return View();
		}

		// POST: Users/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name,Password")] User user)
		{
			if (ModelState.IsValid)
			{
				await _userRepository.Create(user);

				return RedirectToAction(nameof(Index));
			}
			return View(user);
		}

		// GET: Users/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = await _userRepository.GetById(id);
			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}

		// POST: Users/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Edit(int id, [Bind("UserId,Name,Password")] User user)
		//{
		//	if (id != user.UserId)
		//	{
		//		return NotFound();
		//	}

		//	if (ModelState.IsValid)
		//	{
		//		try
		//		{
		//			_context.Update(user);
		//			await _context.SaveChangesAsync();
		//		}
		//		catch (DbUpdateConcurrencyException)
		//		{
		//			if (!UserExists(user.UserId))
		//			{
		//				return NotFound();
		//			}
		//			else
		//			{
		//				throw;
		//			}
		//		}
		//		return RedirectToAction(nameof(Index));
		//	}
		//	return View(user);
		//}

		//// GET: Users/Delete/5
		//public async Task<IActionResult> Delete(int? id)
		//{
		//	if (id == null)
		//	{
		//		return NotFound();
		//	}

		//	var user = await _context.User
		//		.SingleOrDefaultAsync(m => m.UserId == id);
		//	if (user == null)
		//	{
		//		return NotFound();
		//	}

		//	return View(user);
		//}

		//// POST: Users/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> DeleteConfirmed(int id)
		//{
		//	var user = await _context.User.SingleOrDefaultAsync(m => m.UserId == id);
		//	_context.User.Remove(user);
		//	await _context.SaveChangesAsync();
		//	return RedirectToAction(nameof(Index));
		//}

		//private bool UserExists(int id)
		//{
		//	return _context.User.Any(e => e.UserId == id);
		//}
	}
}
