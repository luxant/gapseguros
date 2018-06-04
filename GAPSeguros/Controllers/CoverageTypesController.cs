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

namespace GAPSeguros.Controllers
{
	public class CoverageTypesController : Controller
	{
		private readonly ICoverageTypeRepository _coverageTypeRepository;
		private readonly AbstractValidator<CoverageType> _abstractValidator;

		public CoverageTypesController(ICoverageTypeRepository coverageTypeRepository, AbstractValidator<CoverageType> abstractValidator)
		{
			_coverageTypeRepository = coverageTypeRepository;
			_abstractValidator = abstractValidator;
		}

		// GET: CoverageTypes
		public async Task<IActionResult> Index()
		{
			return View(await _coverageTypeRepository.GetAll());
		}

		// GET: CoverageTypes/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var coverageType = await _coverageTypeRepository.GetById(id);
			if (coverageType == null)
			{
				return NotFound();
			}

			return View(coverageType);
		}

		// GET: CoverageTypes/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: CoverageTypes/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("CoverageTypeId,Name")] CoverageType coverageType)
		{
			if (ModelState.IsValid)
			{
				await _coverageTypeRepository.Create(coverageType);

				return RedirectToAction(nameof(Index));
			}
			return View(coverageType);
		}

		// GET: CoverageTypes/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var coverageType = await _coverageTypeRepository.GetById(id);

			if (coverageType == null)
			{
				return NotFound();
			}
			return View(coverageType);
		}

		// POST: CoverageTypes/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("CoverageTypeId,Name")] CoverageType coverageType)
		{
			if (id != coverageType.CoverageTypeId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _coverageTypeRepository.Update(coverageType);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CoverageTypeExists(coverageType.CoverageTypeId))
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
			return View(coverageType);
		}

		// GET: CoverageTypes/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var coverageType = await _coverageTypeRepository.GetById(id);
			if (coverageType == null)
			{
				return NotFound();
			}

			return View(coverageType);
		}

		// POST: CoverageTypes/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var entity = await _coverageTypeRepository.GetById(id);

			var result = _abstractValidator.Validate(entity, ruleSet: "delete");


			if (result.IsValid)
			{
				await _coverageTypeRepository.DeleteById(id);

				return RedirectToAction(nameof(Index));
			}
			else
			{
				result.AddToModelState(ModelState, null);

				return await Delete(id);
			}
		}

		private bool CoverageTypeExists(int id)
		{
			return _coverageTypeRepository.GetById(id) != null;
		}
	}
}
