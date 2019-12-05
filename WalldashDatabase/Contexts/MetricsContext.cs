using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace WalldashDatabase.Contexts
{
	public class MetricsContext : DbContext
	{
		public DbSet<Metric> Metrics { get; set; }


		public MetricsContext(DbContextOptions<MetricsContext> options) : base(options)
		{ }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(ConfigurationBuilder);
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Metric>()
				.Property(m => m.Alias)
				.IsRequired();
			modelBuilder.Entity<Metric>()
				.Property(m => m.TimeStamp)
				.IsRequired();
			modelBuilder.Entity<Metric>()
				.Property(m => m.Value)
				.IsRequired();
		}
	}

	public class Metric
	{
		public int Id { get; set; }
		public string Alias { get; set; }
		public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.UtcNow;
		public double Value { get; set; }
	}
}
