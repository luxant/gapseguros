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
	public class UserValidator : AbstractValidator<User>
	{
		private readonly IRoleByUserRepository _roleByUserRepository;
		private readonly IPolicyByUserRepository _policyByUserRepository;

		public UserValidator(IRoleByUserRepository roleByUserRepository, IPolicyByUserRepository policyByUserRepository)
		{
			_roleByUserRepository = roleByUserRepository;
			_policyByUserRepository = policyByUserRepository;

			RuleFor(x => x.Name).NotEmpty().Length(1, 50);
			RuleFor(x => x.Password).Length(1, 40);

			RuleSet("delete", () =>
			{
				RuleFor(x => x).Must(NotHaveAnyRoleAssigned)
					.WithName(".")
					.WithMessage("The user has one or more roles already assigned, please delete the relationship first");

				RuleFor(x => x).Must(NotHaveAnyPolicyAssigned)
					.WithName(".")
					.WithMessage("The user has one or more policies already assigned, please delete the relationship first");
			});
		}

		private bool NotHaveAnyPolicyAssigned(User user)
		{
			return _policyByUserRepository.GetUserPolicies(user.UserId).Count() == 0;
		}

		private bool NotHaveAnyRoleAssigned(User user)
		{
			return _roleByUserRepository.GetUserRoles(user.UserId).Result.Count() == 0;
		}
	}
}
