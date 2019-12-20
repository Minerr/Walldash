using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace DataAccess.Handlers
{
	public static class MetricHandler
	{
		#region CRUD
		public static Metric Get(int accountId, int metricId)
		{
			using(SqlConnection conn = new SqlConnection(Program.CONNECTION_STRING_WALLDASH))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand("dbo.Metric_Get", conn);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("AccountId", accountId);
				cmd.Parameters.AddWithValue("MetricId", metricId);

				XmlReader reader = cmd.ExecuteXmlReader();
				Metric metric = (Metric) reader.ReadContentAs(typeof(Metric), null);

				return metric;
			}
		}

		public static List<Metric> GetAll(int accountId)
		{
			using(SqlConnection conn = new SqlConnection(Program.CONNECTION_STRING_WALLDASH))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand("dbo.Metric_GetAll", conn);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("AccountId", accountId);

				XmlReader reader = cmd.ExecuteXmlReader();
				List<Metric> metrics = (List<Metric>)reader.ReadContentAs(typeof(Metric), null);

				return metrics;
			}
		}

		public static int Save(int accountId, Metric metric)
		{
			using(SqlConnection conn = new SqlConnection(Program.CONNECTION_STRING_WALLDASH))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand("dbo.Metric_Save", conn);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("AccountId", accountId);
				cmd.Parameters.AddWithValue("Alias", metric.Alias);
				cmd.Parameters.AddWithValue("Number", metric.Number);
				cmd.Parameters.AddWithValue("Timestamp", metric.Timestamp);

				return (int)cmd.ExecuteScalar();
			}
		}

		public static void Update(int accountId, int metricId, Metric metric)
		{
			// Id's must be the same
			if(metric.Id == metricId)
			{
				using(SqlConnection conn = new SqlConnection(Program.CONNECTION_STRING_WALLDASH))
				{
					conn.Open();

					SqlCommand cmd = new SqlCommand("dbo.Metric_Update", conn);
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("MetricId", metricId);
					cmd.Parameters.AddWithValue("AccountId", accountId);
					cmd.Parameters.AddWithValue("Alias", metric.Alias);
					cmd.Parameters.AddWithValue("Number", metric.Number);
					cmd.Parameters.AddWithValue("Timestamp", metric.Timestamp);

					cmd.ExecuteNonQuery();
				}
			}
		}

		public static void Delete(int accountId, int metricId)
		{
			using(SqlConnection conn = new SqlConnection(Program.CONNECTION_STRING_WALLDASH))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand("dbo.Metric_Delete", conn);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("MetricId", metricId);
				cmd.Parameters.AddWithValue("AccountId", accountId);

				cmd.ExecuteNonQuery();
			}
		}
		#endregion

		#region Extra methods
		public static List<Metric> GetAllByAlias(int accountId, string alias)
		{
			using(SqlConnection conn = new SqlConnection(Program.CONNECTION_STRING_WALLDASH))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand("dbo.Metric_GetAllByAlias", conn);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("AccountId", accountId);
				cmd.Parameters.AddWithValue("Alias", alias);

				XmlReader reader = cmd.ExecuteXmlReader();
				List<Metric> metrics = (List<Metric>)reader.ReadContentAs(typeof(Metric), null);

				return metrics;
			}
		}

		public static List<Metric> GetAllByPeriod(int accountId, DateTimeOffset dateFrom, DateTimeOffset dateTo)
		{
			using(SqlConnection conn = new SqlConnection(Program.CONNECTION_STRING_WALLDASH))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand("dbo.Metric_GetAllByPeriod", conn);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("AccountId", accountId);
				cmd.Parameters.AddWithValue("DateFrom", dateFrom);
				cmd.Parameters.AddWithValue("DateTo", dateTo);

				XmlReader reader = cmd.ExecuteXmlReader();
				List<Metric> metrics = (List<Metric>)reader.ReadContentAs(typeof(Metric), null);

				return metrics;
			}
		}

		public static List<string> GetAliases(int accountId)
		{
			using(SqlConnection conn = new SqlConnection(Program.CONNECTION_STRING_WALLDASH))
			{
				conn.Open();

				SqlCommand cmd = new SqlCommand("dbo.Metric_GetAliases", conn);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("AccountId", accountId);

				XmlReader reader = cmd.ExecuteXmlReader();
				List<string> aliases = (List<string>)reader.ReadContentAs(typeof(Metric), null);

				return aliases;
			}
		}
		#endregion
	}
}
