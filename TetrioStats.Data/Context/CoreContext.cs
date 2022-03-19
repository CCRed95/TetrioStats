using Microsoft.EntityFrameworkCore;
using TetrioStats.Data.Domain;
using TetrioStats.Data.Maps;

namespace TetrioStats.Data.Context
{
	public class CoreContext
		: DbContext
	{
		public DbSet<User> Users { get; set; }

		public DbSet<GameRecord> GameRecords { get; set; }

		public DbSet<UserStatisticsEntry> UserStatisticsEntries { get; set; }

		public DbSet<TLStatsEntry> TLStatsEntries { get; set; }



		protected override void OnConfiguring(
			DbContextOptionsBuilder optionsBuilder)
		{
			const string userId = "podcache_tetriostats_public";
			const string password = "90+o2X^J=1OM";

			var connectionStr =
				"server=66.29.135.228;" +
				$"user id={userId};" +
				"persistsecurityinfo=True;" +
				"database=podcache_tetriostats;" +
				$"pwd={password}";

			optionsBuilder.UseMySql(connectionStr);
		}

		protected override void OnModelCreating(
			ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder
				.WithConfiguration<GameRecordMap, GameRecord>()
				.WithConfiguration<UserMap, User>()
				.WithConfiguration<UserStatisticsEntryMap, UserStatisticsEntry>()
				.WithConfiguration<TLStatsEntryMap, TLStatsEntry>()
				.WithConfiguration<CountryMap, Country>();
			
			//modelBuilder
			//		.Entity<Country>()
			//		.HasData()
		}
	}
}