using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TetrioStats.Data.Domain;

namespace TetrioStats.Data.Maps
{
	public class GameRecordMap
		: IEntityTypeConfiguration<GameRecord>
	{
		public void Configure(
			EntityTypeBuilder<GameRecord> builder)
		{
			builder.ToTable("GameRecords");

			builder.HasKey(t => t.GameRecordID);
			builder
				.Property(t => t.GameRecordID)
				.IsRequired()
				.ValueGeneratedOnAdd();

			builder
				.Property(t => t.GameType)
				.IsRequired();

			builder
				.Property(t => t.TetrioGameRecordID)
				.IsRequired();

			builder
				.Property(t => t.StreamKey)
				.IsRequired();

			builder
				.Property(t => t.ReplayID)
				.IsRequired();

			builder
				.Property(t => t.UserID)
				.IsRequired();

			builder
				.Property(t => t.Username)
				.IsRequired();

			builder
				.Property(t => t.TimeStamp)
				.IsRequired();

			builder
				.Property(t => t.IsMultiplayer)
				.IsRequired();

			builder
				.Property(t => t.FinalTime)
				.IsRequired();

			builder
				.Property(t => t.Kills)
				.IsRequired();

			builder
				.Property(t => t.LinesCleared)
				.IsRequired();

			builder
				.Property(t => t.LevelLines)
				.IsRequired();

			builder
				.Property(t => t.LevelLinesNeeded)
				.IsRequired();

			builder
				.Property(t => t.Inputs)
				.IsRequired();

			builder
				.Property(t => t.Holds)
				.IsRequired();

			builder
				.Property(t => t.Score)
				.IsRequired();

			builder
				.Property(t => t.ZenLevel)
				.IsRequired();

			builder
				.Property(t => t.ZenProgress)
				.IsRequired();

			builder
				.Property(t => t.Level)
				.IsRequired();

			builder
				.Property(t => t.Combo)
				.IsRequired();

			builder
				.Property(t => t.CurrentComboPower)
				.IsRequired(false);

			builder
				.Property(t => t.TopCombo)
				.IsRequired();

			builder
				.Property(t => t.BTB)
				.IsRequired();

			builder
				.Property(t => t.TopBTB)
				.IsRequired();

			builder
				.Property(t => t.TSpins)
				.IsRequired();

			builder
				.Property(t => t.TotalPiecesPlaced)
				.IsRequired();

			builder
				.Property(t => t.LineClearsSingles)
				.IsRequired();

			builder
				.Property(t => t.LineClearsDoubles)
				.IsRequired();

			builder
				.Property(t => t.LineClearsTriples)
				.IsRequired();

			builder
				.Property(t => t.LineClearsQuads)
				.IsRequired();

			builder
				.Property(t => t.LineClearsRealTSpins)
				.IsRequired();

			builder
				.Property(t => t.LineClearsMiniTSpins)
				.IsRequired();

			builder
				.Property(t => t.LineClearsMiniTSpinSingles)
				.IsRequired();

			builder
				.Property(t => t.LineClearsTSpinSingles)
				.IsRequired();

			builder
				.Property(t => t.LineClearsMiniTSpinDoubles)
				.IsRequired();

			builder
				.Property(t => t.LineClearsTSpinDoubles)
				.IsRequired();

			builder
				.Property(t => t.LineClearsTSpinTriples)
				.IsRequired();

			builder
				.Property(t => t.LineClearsTSpinQuads)
				.IsRequired();

			builder
				.Property(t => t.LineClearsAllClears)
				.IsRequired();

			builder
				.Property(t => t.GarbageTotalSent)
				.IsRequired();

			builder
				.Property(t => t.GarbageTotalReceived)
				.IsRequired();

			builder
				.Property(t => t.GarbageAttack)
				.IsRequired();

			builder
				.Property(t => t.GarbageTotalCleared)
				.IsRequired();

			builder
				.Property(t => t.FinesseCombo)
				.IsRequired();

			builder
				.Property(t => t.FinesseFaults)
				.IsRequired();

			builder
				.Property(t => t.FinessePerfectPieces)
				.IsRequired();
		}
	}
}