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

		public UserValidator(IRoleByUserRepository roleByUserRepository)
		{
			_roleByUserRepository = roleByUserRepository;

			RuleFor(x => x.Name).NotEmpty().Length(1, 50);
			RuleFor(x => x.Password).Length(1, 40);

			RuleSet("delete", () =>
			{
				RuleFor(x => x).Must(NotHaveAnyRoleAssigned)
					.WithName(".")
					.WithMessage("The user has one or more roles already assigned, please delete the relationship first");
			});
		}

		private bool NotHaveAnyRoleAssigned(User user)
		{
			return _roleByUserRepository.GetUserRoles(user.UserId).Result.Count() == 0;
		}
	}
}
