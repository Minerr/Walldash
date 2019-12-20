using System;

namespace DataAccess.Model
{
	public class Metric
	{
		public int Id { get; set; }
		public string Alias { get; set; }
		public int Number { get; set; }
		public DateTimeOffset Timestamp { get; set; }
	}
}
