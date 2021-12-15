using Microsoft.EntityFrameworkCore;
using TetrioStats.Data.Domain;
using TetrioStats.Data.Maps;

namespace TetrioStats.Data.Context
{
	public class LocalCoreContext
		: DbContextBase
	{
		public override string DatabaseName => "TetrioStats";


		public DbSet<User> Users { get; set; }

		public DbSet<GameRecord> GameRecords { get; set; }

		public DbSet<UserStatisticsEntry> UserStatisticsEntries { get; set; }

		public DbSet<TLStatsEntry> TLStatsEntries { get; set; }



		protected override void OnModelCreating(
			ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder
				.WithConfiguration<GameRecordMap, GameRecord>()
				.WithConfiguration<UserMap, User>()
				.WithConfiguration<UserStatisticsEntryMap, UserStatisticsEntry>()
				.WithConfiguration<TLStatsEntryMap, TLStatsEntry>();
		}
	}
}