using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TetrioStats.Data.Domain;

namespace TetrioStats.Data.Maps;

public class TLStatsEntryMap
	: IEntityTypeConfiguration<TLStatsEntry>
{
	public void Configure(
		EntityTypeBuilder<TLStatsEntry> builder)
	{
		builder.ToTable("TLStatsEntries");

		builder
			.HasKey(t => t.TLStatsEntryID)
			.IsClustered(false);

		builder
			.Property(t => t.TLStatsEntryID)
			.IsRequired()
			.ValueGeneratedOnAdd();

		builder
			.Property(t => t.UserID)
			.IsRequired();
		//	.HasColumnType("varchar(50)");
		//.HasMaxLength(50);

		builder
			.Property(t => t.Username)
			.IsRequired();
		//.HasColumnType("varchar(6)");
		//		.HasMaxLength(34);

		builder
			.Property(t => t.DateTimeUtc)
			.IsRequired();

		builder
			.Property(t => t.XP)
			.IsRequired(false);

		builder
			.Property(t => t.Country)
			//	.HasColumnType("varchar(6)")
			//.HasMaxLength(6)
			.IsRequired();

		builder
			.Property(t => t.GP)
			.IsRequired(false);

		builder
			.Property(t => t.GW)
			.IsRequired(false);

		builder
			.Property(t => t.TR)
			.IsRequired(false);

		builder
			.Property(t => t.Glicko)
			.IsRequired(false);

		builder
			.Property(t => t.RD)
			.IsRequired(false);

		builder
			.Property(t => t.UserRank)
			//	.HasColumnType("varchar(6)")
			//.HasMaxLength(6)
			.IsRequired();

		builder
			.Property(t => t.APM)
			.IsRequired(false);

		builder
			.Property(t => t.PPS)
			.IsRequired(false);

		builder
			.Property(t => t.VS)
			.IsRequired(false);

		builder
			.HasIndex(t => t.UserID)
			.IsUnique(false)
			.IsClustered();
	}
}