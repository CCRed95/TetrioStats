using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TetrioStats.Data.Migrations.CoreMYSQLContext
{
	public partial class InitialCoreMYSQLContext : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
					name: "TLStatsEntries",
					columns: table => new
					{
						TLStatsEntryID = table.Column<int>(nullable: false)
									.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
						UserID = table.Column<string>(nullable: true),
						Username = table.Column<string>(nullable: true),
						DateTimeUtc = table.Column<DateTime>(nullable: false),
						XP = table.Column<double>(nullable: true),
						Country = table.Column<string>(nullable: false),
						GP = table.Column<int>(nullable: true),
						GW = table.Column<int>(nullable: true),
						TR = table.Column<double>(nullable: true),
						Glicko = table.Column<double>(nullable: true),
						RD = table.Column<double>(nullable: true),
						UserRank = table.Column<string>(nullable: false),
						APM = table.Column<double>(nullable: true),
						PPS = table.Column<double>(nullable: true),
						VS = table.Column<double>(nullable: true)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_TLStatsEntries", x => x.TLStatsEntryID);
					});

			migrationBuilder.CreateTable(
					name: "Users",
					columns: table => new
					{
						UserID = table.Column<int>(nullable: false)
									.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
						TetrioUserID = table.Column<string>(fixedLength: true, maxLength: 24, nullable: false),
						Username = table.Column<string>(maxLength: 16, nullable: false)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Users", x => x.UserID);
					});

			migrationBuilder.CreateTable(
					name: "GameRecords",
					columns: table => new
					{
						GameRecordID = table.Column<int>(nullable: false)
									.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
						GameType = table.Column<int>(nullable: false),
						TetrioGameRecordID = table.Column<string>(nullable: false),
						StreamKey = table.Column<string>(nullable: false),
						ReplayID = table.Column<string>(nullable: false),
						UserID = table.Column<int>(nullable: false),
						Username = table.Column<string>(nullable: false),
						TimeStamp = table.Column<DateTime>(nullable: false),
						IsMultiplayer = table.Column<bool>(nullable: false),
						FinalTime = table.Column<TimeSpan>(nullable: false),
						Kills = table.Column<int>(nullable: false),
						LinesCleared = table.Column<int>(nullable: false),
						LevelLines = table.Column<int>(nullable: false),
						LevelLinesNeeded = table.Column<int>(nullable: false),
						Inputs = table.Column<int>(nullable: false),
						Holds = table.Column<int>(nullable: false),
						Score = table.Column<int>(nullable: false),
						ZenLevel = table.Column<int>(nullable: false),
						ZenProgress = table.Column<int>(nullable: false),
						Level = table.Column<int>(nullable: false),
						Combo = table.Column<int>(nullable: false),
						CurrentComboPower = table.Column<int>(nullable: true),
						TopCombo = table.Column<int>(nullable: false),
						BTB = table.Column<int>(nullable: false),
						TopBTB = table.Column<int>(nullable: false),
						TSpins = table.Column<int>(nullable: false),
						TotalPiecesPlaced = table.Column<int>(nullable: false),
						LineClearsSingles = table.Column<int>(nullable: false),
						LineClearsDoubles = table.Column<int>(nullable: false),
						LineClearsTriples = table.Column<int>(nullable: false),
						LineClearsQuads = table.Column<int>(nullable: false),
						LineClearsRealTSpins = table.Column<int>(nullable: false),
						LineClearsMiniTSpins = table.Column<int>(nullable: false),
						LineClearsMiniTSpinSingles = table.Column<int>(nullable: false),
						LineClearsTSpinSingles = table.Column<int>(nullable: false),
						LineClearsMiniTSpinDoubles = table.Column<int>(nullable: false),
						LineClearsTSpinDoubles = table.Column<int>(nullable: false),
						LineClearsTSpinTriples = table.Column<int>(nullable: false),
						LineClearsTSpinQuads = table.Column<int>(nullable: false),
						LineClearsAllClears = table.Column<int>(nullable: false),
						GarbageTotalSent = table.Column<int>(nullable: false),
						GarbageTotalReceived = table.Column<int>(nullable: false),
						GarbageAttack = table.Column<int>(nullable: false),
						GarbageTotalCleared = table.Column<int>(nullable: false),
						FinesseCombo = table.Column<int>(nullable: false),
						FinesseFaults = table.Column<int>(nullable: false),
						FinessePerfectPieces = table.Column<int>(nullable: false)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_GameRecords", x => x.GameRecordID);
						table.ForeignKey(
											name: "FK_GameRecords_Users_UserID",
											column: x => x.UserID,
											principalTable: "Users",
											principalColumn: "UserID",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "UserStatisticsEntries",
					columns: table => new
					{
						UserStatisticsEntryID = table.Column<int>(nullable: false)
									.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
						UserID = table.Column<int>(nullable: false),
						TimeStamp = table.Column<DateTime>(nullable: false),
						XP = table.Column<double>(nullable: false),
						GamesPlayed = table.Column<int>(nullable: false),
						GamesWon = table.Column<int>(nullable: false),
						GamePlayTime = table.Column<TimeSpan>(nullable: false),
						TetraGamesPlayed = table.Column<int>(nullable: false),
						TetraGamesWon = table.Column<int>(nullable: false),
						TR = table.Column<double>(nullable: false),
						Glicko = table.Column<double>(nullable: false),
						GlickoRD = table.Column<double>(nullable: false),
						Rank = table.Column<string>(nullable: false),
						APM = table.Column<double>(nullable: false),
						PPS = table.Column<double>(nullable: false),
						VS = table.Column<double>(nullable: false),
						GlobalStanding = table.Column<int>(nullable: false),
						LocalStanding = table.Column<int>(nullable: false),
						Percentile = table.Column<double>(nullable: false),
						PercentileRank = table.Column<string>(nullable: false)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_UserStatisticsEntries", x => x.UserStatisticsEntryID);
						table.ForeignKey(
											name: "FK_UserStatisticsEntries_Users_UserID",
											column: x => x.UserID,
											principalTable: "Users",
											principalColumn: "UserID",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateIndex(
					name: "IX_GameRecords_UserID",
					table: "GameRecords",
					column: "UserID");

			migrationBuilder.CreateIndex(
					name: "IX_TLStatsEntries_UserID",
					table: "TLStatsEntries",
					column: "UserID");

			migrationBuilder.CreateIndex(
					name: "IX_Users_TetrioUserID",
					table: "Users",
					column: "TetrioUserID",
					unique: true);

			migrationBuilder.CreateIndex(
					name: "IX_Users_Username",
					table: "Users",
					column: "Username",
					unique: true);

			migrationBuilder.CreateIndex(
					name: "IX_UserStatisticsEntries_UserID",
					table: "UserStatisticsEntries",
					column: "UserID");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "GameRecords");

			migrationBuilder.DropTable(
					name: "TLStatsEntries");

			migrationBuilder.DropTable(
					name: "UserStatisticsEntries");

			migrationBuilder.DropTable(
					name: "Users");
		}
	}
}
