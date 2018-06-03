using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Database;
using DataAccess.Repositories;

namespace GAPSeguros.Controllers
{
	public class RoleByUsersController : Controller
	{
		private readonly GAPSegurosContext _context;
		private readonly IRoleByUserRepository _roleByUserRepository;

		public RoleByUsersController(GAPSegurosContext context, IRoleByUserRepository roleByUserRepository)
		{
			_context = context;
			_roleByUserRepository = roleByUserRepository;
		}

		// GET: RoleByUsers
		public async Task<IActionResult> Index()
		{
			return View(await _roleByUserRepository.GetAll());
		}

		// GET: RoleByUsers/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var roleByUser = await _roleByUserRepository.GetById(id);
			if (roleByUser == null)
			{
				return NotFound();
			}

			return View(roleByUser);
		}

		// GET: RoleByUsers/Create
		public IActionResult Create()
		{
			ViewData["RoleId"] = new SelectList(_context.Role, "RoleId", "Name");
			ViewData["UserId"] = new SelectList(_context.User, "UserId", "Name");
			return View();
		}

		// POST: RoleByUsers/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("RoleByUserId,RoleId,UserId")] RoleByUser roleByUser)
		{
			if (ModelState.IsValid)
			{
				await _roleByUserRepository.Create(roleByUser);
				return RedirectToAction(nameof(Index));
			}
			ViewData["RoleId"] = new SelectList(_context.Role, "RoleId", "Name", roleByUser.RoleId);
			ViewData["UserId"] = new SelectList(_context.User, "UserId", "Name", roleByUser.UserId);
			return View(roleByUser);
		}

		// GET: RoleByUsers/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var roleByUser = await _roleByUserRepository.GetById(id);
			if (roleByUser == null)
			{
				return NotFound();
			}
			ViewData["RoleId"] = new SelectList(_context.Role, "RoleId", "Name", roleByUser.RoleId);
			ViewData["UserId"] = new SelectList(_context.User, "UserId", "Name", roleByUser.UserId);
			return View(roleByUser);
		}

		// POST: RoleByUsers/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("RoleByUserId,RoleId,UserId")] RoleByUser roleByUser)
		{
			if (id != roleByUser.RoleByUserId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _roleByUserRepository.Update(roleByUser);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!RoleByUserExists(roleByUser.RoleByUserId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			ViewData["RoleId"] = new SelectList(_context.Role, "RoleId", "Name", roleByUser.RoleId);
			ViewData["UserId"] = new SelectList(_context.User, "UserId", "Name", roleByUser.UserId);
			return View(roleByUser);
		}

		// GET: RoleByUsers/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var roleByUser = await _roleByUserRepository.GetById(id);
			if (roleByUser == null)
			{
				return NotFound();
			}

			return View(roleByUser);
		}

		// POST: RoleByUsers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _roleByUserRepository.DeleteById(id);

			return RedirectToAction(nameof(Index));
		}

		private bool RoleByUserExists(int id)
		{
			return _roleByUserRepository.GetById(id) != null;
		}
	}
}
