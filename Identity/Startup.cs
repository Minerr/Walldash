using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DataAccess.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Identity
{
	public class Startup
	{
		private readonly IConfiguration _configuration;
		private readonly string _connectionString;
		private readonly string _migrationsAssembly;

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
			_connectionString = DataAccess.Program.CONNECTION_STRING_WALLDASH;
			_migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

			// configure identity server with in-memory users, but EF stores for clients and resources
			services.AddIdentityServer()
				.AddConfigurationStore(builder =>
					builder.UseSqlServer(connectionString, options =>
						options.MigrationsAssembly(migrationsAssembly)))
				.AddOperationalStore(builder =>
					builder.UseSqlServer(connectionString, options =>
						options.MigrationsAssembly(migrationsAssembly)));

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if(env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseIdentityServer();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
			});
		}
	}
}
