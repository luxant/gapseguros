using AutoMapper;
using DataAccess.Database;
using DataAccess.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using SuperZapatosGAP;
using SuperZapatosGAP.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace TestProject
{
	public class UnitTests
	{
		private readonly TestServer _server;
		private readonly HttpClient _client;

		public UnitTests()
		{
			// Arrange
			_server = new TestServer(new WebHostBuilder()
				.UseStartup<Startup>());
			_client = _server.CreateClient();
		}

		[Fact]
		public void GetAllArticles()
		{
			// We configure the in-memory database
			var options = new DbContextOptionsBuilder<GAPSuperZapatosContext>()
				.UseInMemoryDatabase(databaseName: "Add_writes_to_database")
				.Options;

			// We define the initial data that will be in the DB
			var articlesSeeds = new List<Articles>();
			articlesSeeds.Add(new Articles() { Id = 1 });
			articlesSeeds.Add(new Articles() { Id = 2 });

			// Expected result
			var expected = new ResponseArticleDTO()
			{
				Success = true,
				Articles = Mapper.Map<IEnumerable<ArticlesDTO>>(articlesSeeds),
				TotalElements = articlesSeeds.Count
			};


			// Run the test against one instance of the context
			using (var context = new GAPSuperZapatosContext(options))
			{
				context.AddRange(articlesSeeds);

				context.SaveChanges();
			}

			// Use a separate instance of the context to verify correct data was saved to database
			using (var context = new GAPSuperZapatosContext(options))
			{
				var service = new ArticlesController(context, Mapper.Instance);
				var result = (ResponseArticleDTO)service.Get();

				// Assert the object are equivalent
				expected.Should().BeEquivalentTo(result);
			}
		}
	}
}
