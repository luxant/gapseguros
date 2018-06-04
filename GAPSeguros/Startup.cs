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
using DataAccess.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

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
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
					.AddCookie();

			services.AddMvc()
				.AddXmlSerializerFormatters() // We add the XML support for the API reponse
				.AddJsonOptions(options =>
				{
					options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
				})
				.AddFluentValidation(fv => // We configure the validators
				{
					fv.RegisterValidatorsFromAssemblyContaining<PolicyValidator>();
				});

			// We configure the dependency injection for the DB context
			services.AddDbContext<GAPSegurosContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("GAPSeguros"))
			);

			// We add automapper as a service
			services.AddAutoMapper(typeof(Startup));

			// Repository DI
			services.AddScoped<IPolicyRepository, PolicyRepository>();
			services.AddScoped<IRiskTypeRepository, RiskTypeRepository>();
			services.AddScoped<IPolicyByUserRepository, PolicyByUserRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IRoleByUserRepository, RoleByUserRepository>();
			services.AddScoped<ICoverageTypeRepository, CoverageTypeRepository>();
			services.AddScoped<ICoverageTypeByPolicyRepository, CoverageTypeByPolicyRepository>();

			// Validators DI
			services.AddScoped<AbstractValidator<Policy>, PolicyValidator>();
			services.AddScoped<AbstractValidator<User>, UserValidator>();
			services.AddScoped<AbstractValidator<CoverageType>, CoverageTypeValidator>();


			services.AddAuthorization(options =>
			{
				options.AddPolicy("AdministratorsOnlyAllowed", policy =>
				{
					policy.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
					policy.RequireAuthenticatedUser();
					policy.RequireAssertion(context =>
					{
						var stringRoleId = ((int)DataAccess.Enums.Role.Admin).ToString();

						return context.User.Claims.Any(x => x.Type == ClaimTypes.Role && x.Value == "1");
					});
				});
			});
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

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
