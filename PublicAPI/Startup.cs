using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PublicAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(PublicAPI.Startup))]
namespace PublicAPI
{
	class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			string SqlConnection = Environment.GetEnvironmentVariable("SqlConnectionString");
			builder.Services.AddDbContext<MetricsContext>(options => options.UseSqlServer(SqlConnection));
		}
	}
}
