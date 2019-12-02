using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PublicAPI.Model
{
	public class MetricsContext : DbContext
	{
		public DbSet<Metric> Metrics { get; set; }

		public MetricsContext(DbContextOptions<MetricsContext> options) : base(options) { }
	}

	public class Metric
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.UtcNow;
		public double Value { get; set; }
	}
}
