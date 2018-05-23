using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Database;

namespace GAPSeguros.Controllers
{
	public class PolicyByUsersController : Controller
	{
		private readonly GAPSegurosContext _context;

		public PolicyByUsersController(GAPSegurosContext context)
		{
			_context = context;
		}

		// GET: PolicyByUsers
		public async Task<IActionResult> Index()
		{
			var gAPSegurosContext = _context.PolicyByUser.Include(p => p.Policy).Include(p => p.User);
			return View(await gAPSegurosContext.ToListAsync());
		}

		// GET: PolicyByUsers/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var policyByUser = await _context.PolicyByUser
				.Include(p => p.Policy)
				.Include(p => p.User)
				.SingleOrDefaultAsync(m => m.PolicyByUserId == id);
			if (policyByUser == null)
			{
				return NotFound();
			}

			return View(policyByUser);
		}

		// GET: PolicyByUsers/Create
		public IActionResult Create()
		{
			ViewData["PolicyId"] = new SelectList(_context.Policy, "PolicyId", "Name");
			ViewData["UserId"] = new SelectList(_context.User, "UserId", "Name");
			return View();
		}

		// POST: PolicyByUsers/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("PolicyByUserId,PolicyId,UserId")] PolicyByUser policyByUser)
		{
			if (ModelState.IsValid)
			{
				_context.Add(policyByUser);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["PolicyId"] = new SelectList(_context.Policy, "PolicyId", "Name", policyByUser.PolicyId);
			ViewData["UserId"] = new SelectList(_context.User, "UserId", "Name", policyByUser.UserId);
			return View(policyByUser);
		}

		// GET: PolicyByUsers/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var policyByUser = await _context.PolicyByUser.SingleOrDefaultAsync(m => m.PolicyByUserId == id);
			if (policyByUser == null)
			{
				return NotFound();
			}
			ViewData["PolicyId"] = new SelectList(_context.Policy, "PolicyId", "Name", policyByUser.PolicyId);
			ViewData["UserId"] = new SelectList(_context.User, "UserId", "Name", policyByUser.UserId);
			return View(policyByUser);
		}

		// POST: PolicyByUsers/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("PolicyByUserId,PolicyId,UserId")] PolicyByUser policyByUser)
		{
			if (id != policyByUser.PolicyByUserId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(policyByUser);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PolicyByUserExists(policyByUser.PolicyByUserId))
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
			ViewData["PolicyId"] = new SelectList(_context.Policy, "PolicyId", "Name", policyByUser.PolicyId);
			ViewData["UserId"] = new SelectList(_context.User, "UserId", "Name", policyByUser.UserId);
			return View(policyByUser);
		}

		// GET: PolicyByUsers/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var policyByUser = await _context.PolicyByUser
				.Include(p => p.Policy)
				.Include(p => p.User)
				.SingleOrDefaultAsync(m => m.PolicyByUserId == id);
			if (policyByUser == null)
			{
				return NotFound();
			}

			return View(policyByUser);
		}

		// POST: PolicyByUsers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var policyByUser = await _context.PolicyByUser.SingleOrDefaultAsync(m => m.PolicyByUserId == id);
			_context.PolicyByUser.Remove(policyByUser);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool PolicyByUserExists(int id)
		{
			return _context.PolicyByUser.Any(e => e.PolicyByUserId == id);
		}
	}
}
