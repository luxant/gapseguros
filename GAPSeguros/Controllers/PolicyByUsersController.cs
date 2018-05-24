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
	public class PolicyByUsersController : Controller
	{
		private readonly IPolicyByUserRepository _policyByUserRepository;
		private readonly IPolicyRepository _policyRepository;
		private readonly IUserRepository _userRepository;

		public PolicyByUsersController(IPolicyByUserRepository policyByUserRepository, IPolicyRepository policyRepository, IUserRepository userRepository)
		{
			_policyByUserRepository = policyByUserRepository;
			_policyRepository = policyRepository;
			_userRepository = userRepository;
		}

		// GET: PolicyByUsers
		public async Task<IActionResult> Index()
		{
			return View(await _policyByUserRepository.GetAll());
		}

		// GET: PolicyByUsers/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var policyByUser = await _policyByUserRepository.GetById(id);

			if (policyByUser == null)
			{
				return NotFound();
			}

			return View(policyByUser);
		}

		// GET: PolicyByUsers/Create
		public async Task<IActionResult> Create()
		{
			var policies = await _policyRepository.GetAll();
			var users = await _userRepository.GetAll();

			ViewData["PolicyId"] = new SelectList(policies, "PolicyId", "Name");
			ViewData["UserId"] = new SelectList(users, "UserId", "Name");
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
				await _policyByUserRepository.Create(policyByUser);

				return RedirectToAction(nameof(Index));
			}

			var policies = await _policyRepository.GetAll();
			var users = await _userRepository.GetAll();

			ViewData["PolicyId"] = new SelectList(policies, "PolicyId", "Name");
			ViewData["UserId"] = new SelectList(users, "UserId", "Name");
			return View(policyByUser);
		}

		// GET: PolicyByUsers/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var policyByUser = await _policyByUserRepository.GetById(id);

			if (policyByUser == null)
			{
				return NotFound();
			}

			var policies = await _policyRepository.GetAll();
			var users = await _userRepository.GetAll();

			ViewData["PolicyId"] = new SelectList(policies, "PolicyId", "Name");
			ViewData["UserId"] = new SelectList(users, "UserId", "Name");
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
					await _policyByUserRepository.Update(policyByUser);
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

			var policies = await _policyRepository.GetAll();
			var users = await _userRepository.GetAll();

			ViewData["PolicyId"] = new SelectList(policies, "PolicyId", "Name");
			ViewData["UserId"] = new SelectList(users, "UserId", "Name");
			return View(policyByUser);
		}

		// GET: PolicyByUsers/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var policyByUser = await _policyByUserRepository.GetById(id);
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
			await _policyByUserRepository.DeleteById(id);

			return RedirectToAction(nameof(Index));
		}

		private bool PolicyByUserExists(int id)
		{
			return _policyByUserRepository.GetById(id) != null;
		}
	}
}
