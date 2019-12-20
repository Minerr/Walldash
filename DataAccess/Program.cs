using DbUp;
using System;
using System.Configuration;
using System.Reflection;

namespace DataAccess
{
	public class Program
	{
		public static readonly string CONNECTION_STRING_WALLDASH = ConfigurationManager.ConnectionStrings["DbWalldash"].ConnectionString;

		static int Main(string[] args)
		{
			// ### DbUP ###
			var connectionString = ConfigurationManager.ConnectionStrings["DbWalldash"].ConnectionString;

			var upgrader =
				DeployChanges.To
					.SqlDatabase(connectionString)
					.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
					.LogToConsole()
					.Build();

			var result = upgrader.PerformUpgrade();

			if(!result.Successful)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(result.Error);
				Console.ResetColor();
				#if DEBUG
				Console.ReadLine();
				#endif
				return -1;
			}

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Success!");
			Console.ResetColor();
			// ### End of DbUP ###


			return 0;
		}
	}
}
