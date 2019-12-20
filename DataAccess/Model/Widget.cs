using System;

namespace DataAccess.Model
{
	public class Widget
	{
		public int Id { get; set; }
		public int AccountId { get; set; }
		public int DashboardId { get; set; }
		public string Alias { get; set; }
		public int MetricCompositionId { get; set; }
	}
}
