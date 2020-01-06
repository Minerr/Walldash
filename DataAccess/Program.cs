using System;
using System.Configuration;

namespace DataAccess
{
	public class Program
	{
		public static readonly string CONNECTION_STRING_WALLDASH = ConfigurationManager.ConnectionStrings["DbWalldash"].ConnectionString;

		static void Main(string[] args)
		{
		}
	}
}
