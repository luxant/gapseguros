using DataAccess.Database;
using DataAccess.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Validation
{
	public class CoverageTypeValidator : AbstractValidator<CoverageType>
	{
		private ICoverageTypeByPolicyRepository _coverageTypeByPolicyRepository;

		public CoverageTypeValidator(ICoverageTypeByPolicyRepository coverageTypeByPolicyRepository)
		{
			_coverageTypeByPolicyRepository = coverageTypeByPolicyRepository;

			RuleFor(x => x.Name).NotEmpty().Length(1, 50);

			RuleSet("delete", () =>
			{
				RuleFor(x => x).Must(NotHaveAnyPolicyAssigned)
					.WithName(".")
					.WithMessage("The coverage type has one or more policies already assigned, please delete the relationship first");
			});
		}

		private bool NotHaveAnyPolicyAssigned(CoverageType entity)
		{
			return _coverageTypeByPolicyRepository.GetByCoverageTypeId(entity.CoverageTypeId).Count() == 0;
		}
	}
}
