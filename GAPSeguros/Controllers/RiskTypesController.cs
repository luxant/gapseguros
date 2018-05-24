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
	public class RiskTypesController : Controller
	{
		private readonly IRiskTypeRepository _riskTypeRepository;

		public RiskTypesController(IRiskTypeRepository riskTypeRepository)
		{
			_riskTypeRepository = riskTypeRepository;
		}

		// GET: RiskTypes
		public async Task<IActionResult> Index()
		{
			return View(await _riskTypeRepository.GetAll());
		}

		// GET: RiskTypes/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var riskType = await _riskTypeRepository.GetById(id);
			if (riskType == null)
			{
				return NotFound();
			}

			return View(riskType);
		}

		// GET: RiskTypes/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: RiskTypes/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("RiskTypeId,Name")] RiskType riskType)
		{
			if (ModelState.IsValid)
			{
				await _riskTypeRepository.Create(riskType);

				return RedirectToAction(nameof(Index));
			}
			return View(riskType);
		}

		// GET: RiskTypes/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var riskType = await _riskTypeRepository.GetById(id);

			if (riskType == null)
			{
				return NotFound();
			}
			return View(riskType);
		}

		// POST: RiskTypes/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("RiskTypeId,Name")] RiskType riskType)
		{
			if (id != riskType.RiskTypeId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _riskTypeRepository.Update(riskType);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!RiskTypeExists(riskType.RiskTypeId))
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
			return View(riskType);
		}

		// GET: RiskTypes/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var riskType = await _riskTypeRepository.GetById(id);
			if (riskType == null)
			{
				return NotFound();
			}

			return View(riskType);
		}

		// POST: RiskTypes/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _riskTypeRepository.DeleteById(id);

			return RedirectToAction(nameof(Index));
		}

		private bool RiskTypeExists(int id)
		{
			return _riskTypeRepository.GetById(id) != null;
		}
	}
}
