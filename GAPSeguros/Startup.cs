using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Formatters;
using DataAccess.Repositories;

namespace GAPSeguros
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc()
				.AddXmlSerializerFormatters() // We add the XML support for the API reponse
				.AddJsonOptions(options =>
				{
					options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
				})
				.AddFluentValidation(fv => // We configure the validators
				{
					//fv.RegisterValidatorsFromAssemblyContaining<ArticleValidator>();
					//fv.RegisterValidatorsFromAssemblyContaining<StoresValidator>();
				});

			// We configure the dependency injection for the DB context
			services.AddDbContext<GAPSegurosContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("GAPSeguros"))
			);

			// We add automapper as a service
			services.AddAutoMapper(typeof(Startup));


			services.AddScoped<IPolicyRepository, PolicyRepository>();
			services.AddScoped<IRiskTypeRepository, RiskTypeRepository>();
			services.AddScoped<IPolicyByUserRepository, PolicyByUserRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
