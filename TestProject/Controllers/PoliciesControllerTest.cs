using AutoMapper;
using DataAccess.Database;
using DataAccess.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using GAPSeguros;
using GAPSeguros.Controllers;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace TestProject
{
	public class PoliciesControllerTest
	{
		[Fact]
		public void TestDeleteWhenPolicyExists()
		{
			// Arrange

			// We configure the in-memory database
			var dbOptions = new DbContextOptionsBuilder<GAPSegurosContext>()
				.UseInMemoryDatabase(databaseName: "Add_writes_to_database")
				.Options;

			var toBeDeletedPolicy = new Policy() { PolicyId = 1, RiskType = new RiskType() };
			//toBeDeletedPolicy.RiskType.Policy.Add(toBeDeletedPolicy);

			// We define the initial data that will be in the DB
			var articlesSeeds = new List<Policy>();
			articlesSeeds.Add(toBeDeletedPolicy);
			articlesSeeds.Add(new Policy() { PolicyId = 2, RiskType = new RiskType() });


			// Run the test against one instance of the context
			using (var context = new GAPSegurosContext(dbOptions))
			{
				context.AddRange(articlesSeeds);

				context.SaveChanges();
			}

			// Use a separate instance of the context to verify correct data was saved to database
			using (var context = new GAPSegurosContext(dbOptions))
			{
				var policyRepository = new PolicyRepository(context);

				// Act
				var controller = new PoliciesController(policyRepository, null, null);
				var result = controller.Delete(id: 1).Result;

				// Assert values

				// We check that the IActionResult received is an instance of ViewResult
				var viewResult = Assert.IsType<ViewResult>(result);

				// We check that the model send to the view is a Policy instance
				var model = Assert.IsAssignableFrom<Policy>(viewResult.ViewData.Model);

				// We check the Policy entity but not the RiskType entity because of circular reference
				model.Should().BeEquivalentTo(toBeDeletedPolicy,
					options => options
						.Excluding(ctx => ctx.SelectedMemberInfo.MemberType == typeof(RiskType))
				);
			}
		}
	}
}
