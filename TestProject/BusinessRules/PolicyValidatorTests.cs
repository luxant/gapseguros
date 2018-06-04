using DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using DataAccess.Validation;
using DataAccess.Repositories;
using Moq;
using FluentValidation;

namespace TestProject.BusinessRules
{
	public class PolicyValidatorTests
	{
		[Fact]
		public void TestForeignKeyConstraintIsValidated()
		{
			// Arrange
			var assignments = new List<PolicyByUser>()
			{
				new PolicyByUser(),
				new PolicyByUser(),
				new PolicyByUser(),
			};

			// We define the data that will be returned by the repository
			var mockRepository = new Mock<IPolicyByUserRepository>();
			var mockICoverageTypeByPolicyRepository = new Mock<ICoverageTypeByPolicyRepository>();

			mockRepository.Setup(x => x.GetPolicyAssignations(It.IsAny<int>())).Returns(assignments.AsQueryable());

			var validator = new PolicyValidator(mockRepository.Object, mockICoverageTypeByPolicyRepository.Object);

			var policy = new Policy();

			// Act
			var result = validator.Validate(policy, ruleSet: "delete");

			// Assert
			Assert.Contains(result.Errors, x => x.ErrorMessage == "The policy is already assigned to a user, please delete the relationship first");
		}

		[Fact]
		public void TestErrorIsReturnedIfPorcentageIsAbove50AndTheRiskIsHigh()
		{
			// Arrange
			var mockRepository = new Mock<IPolicyByUserRepository>();
			var mockICoverageTypeByPolicyRepository = new Mock<ICoverageTypeByPolicyRepository>();


			var validator = new PolicyValidator(mockRepository.Object, mockICoverageTypeByPolicyRepository.Object);

			var policy = new Policy()
			{
				Coverage = 60,
				RiskTypeId = (int)DataAccess.Enums.RiskType.High
			};

			// Act
			var result = validator.Validate(policy);

			// Assert
			Assert.Contains(result.Errors, x => x.ErrorMessage == "The coverage percentage can go above 50% because the risk is High");
		}

		[Fact]
		public void TestNoErrorIsReturnedIfPorcentageIsBelowOrEquals50AndTheRiskIsHigh()
		{
			// Arrange
			var mockRepository = new Mock<IPolicyByUserRepository>();
			var mockICoverageTypeByPolicyRepository = new Mock<ICoverageTypeByPolicyRepository>();

			var validator = new PolicyValidator(mockRepository.Object, mockICoverageTypeByPolicyRepository.Object);

			var policy = new Policy()
			{
				Coverage = 50,
				RiskTypeId = (int)DataAccess.Enums.RiskType.High
			};

			// Act
			var result = validator.Validate(policy);

			// Assert
			Assert.DoesNotContain(result.Errors, x => x.ErrorMessage == "The coverage percentage can go above 50% because the risk is High");
		}
	}
}
