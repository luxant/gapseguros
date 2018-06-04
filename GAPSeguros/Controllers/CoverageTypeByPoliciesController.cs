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
	public class CoverageTypeByPoliciesController : Controller
	{
		private readonly ICoverageTypeByPolicyRepository _coverageTypeByPolicyRepository;
		private readonly ICoverageTypeRepository _coverageTypeRepository;
		private readonly IPolicyRepository _policyRepository;

		public CoverageTypeByPoliciesController(ICoverageTypeByPolicyRepository coverageTypeByPolicyRepository, ICoverageTypeRepository coverageTypeRepository, IPolicyRepository policyRepository)
		{
			_coverageTypeByPolicyRepository = coverageTypeByPolicyRepository;
			_coverageTypeRepository = coverageTypeRepository;
			_policyRepository = policyRepository;
		}

		// GET: CoverageTypeByPolicies
		public async Task<IActionResult> Index()
		{
			return View(await _coverageTypeByPolicyRepository.GetAll());
		}

		// GET: CoverageTypeByPolicies/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var coverageTypeByPolicy = await _coverageTypeByPolicyRepository.GetById(id);
			if (coverageTypeByPolicy == null)
			{
				return NotFound();
			}

			return View(coverageTypeByPolicy);
		}

		// GET: CoverageTypeByPolicies/Create
		public async Task<IActionResult> Create()
		{
			ViewData["CoverageTypeId"] = new SelectList(await _coverageTypeRepository.GetAll(), "CoverageTypeId", "Name");
			ViewData["PolicyId"] = new SelectList(await _policyRepository.GetAll(), "PolicyId", "Name");
			return View();
		}

		// POST: CoverageTypeByPolicies/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("CoverageTypeByPolicyId,CoverageTypeId,PolicyId")] CoverageTypeByPolicy coverageTypeByPolicy)
		{
			if (ModelState.IsValid)
			{
				await _coverageTypeByPolicyRepository.Create(coverageTypeByPolicy);

				return RedirectToAction(nameof(Index));
			}
			ViewData["CoverageTypeId"] = new SelectList(await _coverageTypeRepository.GetAll(), "CoverageTypeId", "Name", coverageTypeByPolicy.CoverageTypeId);
			ViewData["PolicyId"] = new SelectList(await _policyRepository.GetAll(), "PolicyId", "Name", coverageTypeByPolicy.PolicyId);
			return View(coverageTypeByPolicy);
		}

		// GET: CoverageTypeByPolicies/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var coverageTypeByPolicy = await _coverageTypeByPolicyRepository.GetById(id);

			if (coverageTypeByPolicy == null)
			{
				return NotFound();
			}
			ViewData["CoverageTypeId"] = new SelectList(await _coverageTypeRepository.GetAll(), "CoverageTypeId", "Name", coverageTypeByPolicy.CoverageTypeId);
			ViewData["PolicyId"] = new SelectList(await _policyRepository.GetAll(), "PolicyId", "Name", coverageTypeByPolicy.PolicyId);
			return View(coverageTypeByPolicy);
		}

		// POST: CoverageTypeByPolicies/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("CoverageTypeByPolicyId,CoverageTypeId,PolicyId")] CoverageTypeByPolicy coverageTypeByPolicy)
		{
			if (id != coverageTypeByPolicy.CoverageTypeByPolicyId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _coverageTypeByPolicyRepository.Update(coverageTypeByPolicy);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CoverageTypeByPolicyExists(coverageTypeByPolicy.CoverageTypeByPolicyId))
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
			ViewData["CoverageTypeId"] = new SelectList(await _coverageTypeRepository.GetAll(), "CoverageTypeId", "Name", coverageTypeByPolicy.CoverageTypeId);
			ViewData["PolicyId"] = new SelectList(await _policyRepository.GetAll(), "PolicyId", "Name", coverageTypeByPolicy.PolicyId);
			return View(coverageTypeByPolicy);
		}

		// GET: CoverageTypeByPolicies/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var coverageTypeByPolicy = await _coverageTypeByPolicyRepository.GetById(id);

			if (coverageTypeByPolicy == null)
			{
				return NotFound();
			}

			return View(coverageTypeByPolicy);
		}

		// POST: CoverageTypeByPolicies/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _coverageTypeByPolicyRepository.DeleteById(id);

			return RedirectToAction(nameof(Index));
		}

		private bool CoverageTypeByPolicyExists(int id)
		{
			return _coverageTypeByPolicyRepository.GetById(id) != null;
		}
	}
}
