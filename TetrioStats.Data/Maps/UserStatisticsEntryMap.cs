using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TetrioStats.Data.Domain;

namespace TetrioStats.Data.Maps;

public class UserStatisticsEntryMap
	: IEntityTypeConfiguration<UserStatisticsEntry>
{
	public void Configure(
		EntityTypeBuilder<UserStatisticsEntry> builder)
	{
		builder.ToTable("UserStatisticsEntries");

		builder.HasKey(t => t.UserStatisticsEntryID);
		builder
			.Property(t => t.UserStatisticsEntryID)
			.IsRequired()
			.ValueGeneratedOnAdd();
    
		builder
			.Property(t => t.TimeStamp)
			.IsRequired();

		builder
			.Property(t => t.XP)
			.IsRequired();

		builder
			.Property(t => t.GamesPlayed)
			.IsRequired();

		builder
			.Property(t => t.GamesWon)
			.IsRequired();

		builder
			.Property(t => t.TotalGamePlayDuration)
			.HasColumnName("GamePlayTime")
			.IsRequired();

		builder
			.Property(t => t.TetraLeagueGamesPlayed)
			.HasColumnName("TetraGamesPlayed")
			.IsRequired();

		builder
			.Property(t => t.TetraLeagueGamesWon)
			.HasColumnName("TetraGamesWon")
			.IsRequired();

		builder
			.Property(t => t.TetraLeagueRating)
			.HasColumnName("TR")
			.IsRequired();

		builder
			.Property(t => t.GlickoRating)
			.HasColumnName("Glicko")
			.IsRequired();

		builder
			.Property(t => t.GlickoRatingDeviation)
			.HasColumnName("GlickoRD")
			.IsRequired();

		builder
			.Property(t => t.UserRank)
			.HasColumnName("Rank")
			.IsRequired();

		builder
			.Property(t => t.AverageRollingAPM)
			.HasColumnName("APM")
			.IsRequired();

		builder
			.Property(t => t.AverageRollingPPS)
			.HasColumnName("PPS")
			.IsRequired();

		builder
			.Property(t => t.AverageRollingVsScore)
			.HasColumnName("VS")
			.IsRequired();

		builder
			.Property(t => t.GlobalLeaderBoardsStanding)
			.HasColumnName("GlobalStanding")
			.IsRequired();

		builder
			.Property(t => t.LocalLeaderBoardsStanding)
			.HasColumnName("LocalStanding")
			.IsRequired();

		builder
			.Property(t => t.Percentile)
			.IsRequired();

		builder
			.Property(t => t.PercentileRank)
			.IsRequired();
	}
}