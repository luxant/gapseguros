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
	public class PoliciesController : Controller
	{
		private readonly GAPSegurosContext _context;
		private readonly IPolicyRepository _policyRepository;
		private readonly ICoverageRepository _coverageRepository;

		public PoliciesController(GAPSegurosContext context, IPolicyRepository policyRepository, ICoverageRepository coverageRepository)
		{
			_context = context;
			_policyRepository = policyRepository;
			_coverageRepository = coverageRepository;
		}

		// GET: Policies
		public async Task<IActionResult> Index()
		{
			return View(await _policyRepository.GetAll());
		}

		// GET: Policies/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var policy = await _context.Policy
				.Include(p => p.RiskType)
				.SingleOrDefaultAsync(m => m.PolicyId == id);
			if (policy == null)
			{
				return NotFound();
			}

			return View(policy);
		}

		// GET: Policies/Create
		public IActionResult Create()
		{
			ViewData["RiskTypeId"] = new SelectList(_context.RiskType, "RiskTypeId", "Name");

			return View();
		}

		// POST: Policies/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("PolicyId,Name,Description,ValidityStart,CoverageSpan,Price,RiskTypeId")] Policy policy)
		{
			if (ModelState.IsValid)
			{
				_context.Add(policy);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["RiskTypeId"] = new SelectList(_context.RiskType, "RiskTypeId", "Name", policy.RiskTypeId);
			return View(policy);
		}

		// GET: Policies/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var policy = await _context.Policy.SingleOrDefaultAsync(m => m.PolicyId == id);
			if (policy == null)
			{
				return NotFound();
			}
			ViewData["RiskTypeId"] = new SelectList(_context.RiskType, "RiskTypeId", "Name", policy.RiskTypeId);
			return View(policy);
		}

		// POST: Policies/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("PolicyId,Name,Description,ValidityStart,CoverageSpan,Price,RiskTypeId")] Policy policy)
		{
			if (id != policy.PolicyId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(policy);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PolicyExists(policy.PolicyId))
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
			ViewData["RiskTypeId"] = new SelectList(_context.RiskType, "RiskTypeId", "Name", policy.RiskTypeId);
			return View(policy);
		}

		// GET: Policies/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var policy = await _context.Policy
				.Include(p => p.RiskType)
				.SingleOrDefaultAsync(m => m.PolicyId == id);
			if (policy == null)
			{
				return NotFound();
			}

			return View(policy);
		}

		// POST: Policies/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var policy = await _context.Policy.SingleOrDefaultAsync(m => m.PolicyId == id);
			_context.Policy.Remove(policy);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool PolicyExists(int id)
		{
			return _context.Policy.Any(e => e.PolicyId == id);
		}
	}
}
