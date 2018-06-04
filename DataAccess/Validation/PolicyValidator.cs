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
	public class PolicyValidator : AbstractValidator<Policy>
	{
		private readonly IPolicyByUserRepository _policyByUserRepository;
		private readonly ICoverageTypeByPolicyRepository _coverageTypeByPolicyRepository;

		public PolicyValidator(IPolicyByUserRepository policyByUserRepository, ICoverageTypeByPolicyRepository coverageTypeByPolicyRepository)
		{
			_policyByUserRepository = policyByUserRepository;
			_coverageTypeByPolicyRepository = coverageTypeByPolicyRepository;

			RuleFor(x => x.Coverage).InclusiveBetween(0, 100);
			RuleFor(x => x.Name).NotEmpty().Length(1, 40);
			RuleFor(x => x.Description).NotEmpty().Length(1, 120);
			RuleFor(x => x.ValidityStart).GreaterThanOrEqualTo(DateTime.Now.Date);
			RuleFor(x => x.CoverageSpan).GreaterThan(0);
			RuleFor(x => x.Price).GreaterThan(0);
			RuleFor(x => x.RiskTypeId).GreaterThan(0);

			// Bussines rule
			RuleFor(x => x).Must(BeLowerThan50IfRiskIsHigh).WithName(x => nameof(x.Coverage)).WithMessage("The coverage percentage can go above 50% because the risk is High");

			RuleSet("delete", () =>
			{
				RuleFor(x => x).Must(NotBeAssignedToAUser)
					.WithName(".")
					.WithMessage("The policy is already assigned to a user, please delete the relationship first");

				RuleFor(x => x).Must(NotBeAssignedToCoverageType)
					.WithName(".")
					.WithMessage("The policy is already assigned to a coverage type, please delete the relationship first");
			});
		}

		private bool NotBeAssignedToCoverageType(Policy policy)
		{
			return _coverageTypeByPolicyRepository.GetPolicyAssignations(policy.PolicyId).Count() == 0;
		}

		private bool NotBeAssignedToAUser(Policy policy)
		{
			return _policyByUserRepository.GetPolicyAssignations(policy.PolicyId).Count() == 0;
		}

		private bool BeLowerThan50IfRiskIsHigh(Policy policy)
		{
			return policy.RiskTypeId == (int)Enums.RiskType.High && policy.Coverage > 50 ? false : true;
		}
	}
}
