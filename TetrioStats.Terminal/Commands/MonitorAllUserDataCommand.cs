using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Ccr.Colorization.Mappings;
using Ccr.Std.Core.Extensions;
using Ccr.Std.Core.Extensions.NumericExtensions;
using Ccr.Std.Core.Numerics.Ranges;
using Ccr.Terminal.Application;
using Ccr.Terminal.Extensions;
using Ccr.Terminal.Extensions.Fluent.Json;
using TetrioStats.Api;
using TetrioStats.Api.Domain.Rankings;
using TetrioStats.Api.Domain.Streams;
using TetrioStats.Data.Context;
using TetrioStats.Data.Domain;
using TetrioStats.Data.Enums;
using TetrioStats.Terminal.Extensions;
using static Ccr.Terminal.ExtendedConsole;

namespace TetrioStats.Terminal.Commands
{
	public class MonitorAllUserDataCommand
		: TerminalCommand<string>
	{
		private static readonly TetrioApiClient _client = new TetrioApiClient();
		private static DateTime _timeCacheExpires;
		private static DateTime _time40LCacheExpires;
		private static readonly LocalCoreContext _coreContext = new LocalCoreContext();

		private IReadOnlyDictionary<UserRankGrade, DoubleRange> _trRankingsThresholds;
		private static readonly IReadOnlyDictionary<string, Color> _rankToStringMap
			= new Dictionary<string, Color>
			{
				["D"] = FromHex("#745e73"),
				["D+"] = FromHex("#6f4c6e"),
				["C-"] = FromHex("#5b3668"),
				["C"] = FromHex("#542064"),
				["C+"] = FromHex("#421b60"),
				["B-"] = FromHex("#4c3dab"),
				["B"] = FromHex("#3b4c9f"),
				["B+"] = FromHex("#3f709c"),
				["A-"] = FromHex("#2e9379"),
				["A"] = FromHex("#369145"),
				["A+"] = FromHex("#3a9d2f"),
				["S-"] = FromHex("#9e8925"),
				["S"] = FromHex("#b78"),
				["S+"] = FromHex("#cea125"),
				["SS"] = FromHex("#df9020"),
				["U"] = FromHex("#ae5128"),
				["X"] = FromHex("#aa41b1"),
				["Z"] = FromHex("#737373"),
			};


		private static Color FromHex(string hex)
		{
			return ColorTranslator.FromHtml(hex);
		}

		public override string CommandName => "monitor-all";

		public override string ShortCommandName => "ma";


		public override void Execute(string args)
		{
			XConsole.Write("Enter a tetr.io username to monitor: ", Swatch.Teal);

			var userName = XConsole.ReadLine();
			userName = userName.ToLower();

			var verbose = XConsole.PromptYesNo("Verbose JSON output?");

			XConsole.Write("Fetching TR Ranking Thresholds... ", Color.MediumTurquoise);

			_trRankingsThresholds = UserRankExtensions.CalculateRankGradeTRThresholds();

			XConsole.WriteLine("Done.", Color.MediumSpringGreen);


			var reversedTrRankingsThresholds = _trRankingsThresholds.Reverse()
				.ToArray();

			UserRankGrade lastThreshold = null;

			foreach (var threshold in reversedTrRankingsThresholds)
			{
				var currentGradeStr = threshold.Key.ToString();
				var color = _rankToStringMap[currentGradeStr];

				const double p = .60;
				const double u = .70;

				var darkened = Color.FromArgb(
					1,
					color.R.ScaleDown(p),
					color.G.ScaleDown(p),
					color.B.ScaleDown(p));

				XConsole
					.SetBackgroundColor(darkened)
					.WriteLine("-".Repeat(Console.BufferWidth), darkened);

				darkened = Color.FromArgb(
					1,
					color.R.ScaleDown(p),
					color.G.ScaleDown(p),
					color.B.ScaleDown(p));

				var lightened = Color.FromArgb(
					1,
					color.R.ScaleUp(u),
					color.G.ScaleUp(u),
					color.B.ScaleUp(u));

				var rankPercentile = (lastThreshold == null)
					? threshold.Key.RankPercentile
					: threshold.Key.RankPercentile - lastThreshold.RankPercentile;

				XConsole
					.SetBackgroundColor(darkened)
					.SetBold()
					.Write(
						$"    {threshold.Key}     |    ",
						lightened)
					.Write(
						$"{threshold.Value.Maximum.Round()} TR      ",
						Color.White)
					.WriteLine(
						$"({rankPercentile}% of player base)",
						lightened)
					.WriteLine("-".Repeat(Console.BufferWidth - Console.CursorLeft), darkened)
					.SetNormalIntensity()
					.SetBackgroundColor(Color.Black);

				lastThreshold = threshold.Key;
			}

			XConsole.SetBackgroundColor(Color.Black);

			XConsole
				.WriteLine()
				.Write("Monitoring account: ", Swatch.Cyan)
				.WriteLine(userName, Swatch.Amber);

			var firstQuery = true;

			_timeCacheExpires = DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(1));
			_time40LCacheExpires = DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(1));

			while (true)
			{
				var timeRemaining = _timeCacheExpires - DateTime.UtcNow;
				var timeRemaining40L = _time40LCacheExpires - DateTime.UtcNow;

				if (firstQuery || (timeRemaining <= TimeSpan.Zero && timeRemaining40L <= TimeSpan.Zero)
				) //_timeSinceQuery.Elapsed > TimeSpan.FromMinutes(5.1)
				{
					firstQuery = false;

					XConsole
						.WriteLine()
						.WriteLine("Querying API...", Swatch.Cyan)
						.WriteLine();

					var response = _client.FetchUserData(userName);

					_timeCacheExpires = response.CacheInfo.CachedUntil;


					var _40LinesResponse = _client.FetchStream(
						StreamID.UserRecent(StreamType._40Lines, response.Content.UserID));

					_time40LCacheExpires = _40LinesResponse.CacheInfo.CachedUntil;


					var stats = response.Content;
					var tl = stats.TetraLeagueStats;

					if (verbose)
					{
						XConsole
							.Outdent()
							.BeginJsonSession()
							.WriteCode("{", JsonCodeKind.Brace);

						XConsole.BeginJsonSession()
							.WriteProperty(stats, t => t.Username)
							.WriteProperty(stats, t => response.CacheInfo.CachedAt)
							.WriteProperty(stats, t => t.GamesPlayed)
							.WriteProperty(stats, t => t.GamesWon)
							.WriteProperty(stats, t => t.TotalGamePlayDuration)
							.WriteProperty(stats, t => t.XP)
							//.WriteProperty(stats, t => t.TimeStamp)
							.WriteCode("  ", JsonCodeKind.Brace)
							.WriteCode("TetraLeagueStats", JsonCodeKind.Attribute)
							.WriteCode(": {", JsonCodeKind.Brace);

						XConsole
							.Indent()
							.BeginJsonSession()
							.WriteProperty(tl, t => t.GamesPlayed)
							.WriteProperty(tl, t => t.GamesWon)
							.WriteProperty(tl, t => t.TetraLeagueRating)
							.WriteProperty(tl, t => t.GlickoRating)
							.WriteProperty(tl, t => t.GlickoRatingDeviation)
							.WriteProperty(tl, t => t.UserRank)
							.WriteProperty(tl, t => t.AverageRollingAPM)
							.WriteProperty(tl, t => t.AverageRollingPPS)
							.WriteProperty(tl, t => t.AverageRollingVSScore)
							.WriteProperty(tl, t => t.GlobalLeaderBoardsStanding)
							.WriteProperty(tl, t => t.LocalLeaderBoardsStanding)
							.WriteProperty(tl, t => t.Percentile)
							.WriteProperty(tl, t => t.PercentileRank);

						XConsole
							.Outdent()
							.BeginJsonSession()
							.WriteCode("  }\n}", JsonCodeKind.Brace);

						XConsole
							.WriteLine();

						var gameRecords = _40LinesResponse.Content
							.Select(t => t.ToGameRecord())
							.ToList();

						XConsole.WriteLine();

						XConsole
							.Write("GameRecords: ", Swatch.Amber)
							.WriteLine("Latest single player games", Swatch.Teal);

						XConsole.WriteLine();

						foreach (var gameRecord in gameRecords)
						{
							LogGameRecord(gameRecord);
						}

						XConsole.WriteLine();
					}


					var json = "{"
						+ $"\n  \"Username\": {response.Content.Username.Quote()}, "
						+ $"\n  \"TimeStamp\": \"{response.CacheInfo.CachedAt:O}\", "
						+ $"\n  \"GamesPlayed\": {response.Content.GamesPlayed}, "
						+ $"\n  \"GamesWon\": {response.Content.GamesWon}, "
						+ $"\n  \"TotalGamePlayDuration\": \"{response.Content.TotalGamePlayDuration:g}\", "
						+ $"\n  \"XP\": {response.Content.XP}, "
						//+ $"\n  \"TimeStamp\": \"{response.Response.Response.TimeStamp:O}\", "
						+ $"\n  \"TetraLeagueStats\": {{"
						+ $"\n    \"GamesPlayed\": {response.Content.TetraLeagueStats.GamesPlayed}, "
						+ $"\n    \"GamesWon\": {response.Content.TetraLeagueStats.GamesWon}, "
						+ $"\n    \"TetrisRating\": {response.Content.TetraLeagueStats.TetraLeagueRating}, "
						+ $"\n    \"GlickoRating\": {response.Content.TetraLeagueStats.GlickoRating}, "
						+ $"\n    \"GlickoRatingDeviation\": {response.Content.TetraLeagueStats.GlickoRatingDeviation}, "
						+ $"\n    \"UserRank\": \"{response.Content.TetraLeagueStats.UserRank}\", "
						+ $"\n    \"AverageRollingAPM\": {response.Content.TetraLeagueStats.AverageRollingAPM}, "
						+ $"\n    \"AverageRollingPPS\": {response.Content.TetraLeagueStats.AverageRollingPPS}, "
						+ $"\n    \"AverageRollingVSScore\": {response.Content.TetraLeagueStats.AverageRollingVSScore}, "
						+ $"\n    \"GlobalLeaderBoardsStanding\": {response.Content.TetraLeagueStats.GlobalLeaderBoardsStanding}, "
						+ $"\n    \"LocalLeaderBoardsStanding\": {response.Content.TetraLeagueStats.LocalLeaderBoardsStanding}, "
						+ $"\n    \"Percentile\": {response.Content.TetraLeagueStats.Percentile}, "
						+ $"\n    \"PercentileRank\": \"{response.Content.TetraLeagueStats.PercentileRank}\"" +
						"\n  }" +
						"\n},";


					var jsonFileInfo = new FileInfo(@"C:\Tetris\tetrio.statistics.json");

					using var jsonFileStream = jsonFileInfo.Exists
						? jsonFileInfo.AppendText()
						: jsonFileInfo.CreateText();

					jsonFileStream.WriteLine(json);
					jsonFileStream.Close();

					var fi = new FileInfo(@"C:\Tetris\tetrio.statistics.csv");

					using var fileStream = fi.Exists ? fi.AppendText() : fi.CreateText();

					var csv = $"{userName.Quote()},"
						+ $"\"{response.CacheInfo.CachedAt:O}\","
						+ $"{stats.GamesPlayed},"
						+ $"{stats.GamesWon},"
						+ $"\"{stats.TotalGamePlayDuration:g}\","
						+ $"{stats.XP},"
						//+ $"\"{stats.TimeStamp:O}\","
						+ $"{tl.GamesPlayed},"
						+ $"{tl.GamesWon},"
						+ $"{tl.TetraLeagueRating},"
						+ $"{tl.GlickoRating},"
						+ $"{tl.GlickoRatingDeviation},"
						+ $"{tl.UserRank.Quote()},"
						+ $"{tl.AverageRollingAPM},"
						+ $"{tl.AverageRollingPPS},"
						+ $"{tl.AverageRollingVSScore},"
						+ $"{tl.GlobalLeaderBoardsStanding},"
						+ $"{tl.LocalLeaderBoardsStanding},"
						+ $"{tl.Percentile},"
						+ $"{tl.PercentileRank.Quote()}";

					var recordCount = _coreContext.UserStatisticsEntries.Count();

					var lastRecords = _coreContext.UserStatisticsEntries
						.Where(t => t.UserStatisticsEntryID == recordCount);

					var lastRecord = lastRecords.SingleOrDefault();

					var user = _client.FetchUser(stats.Username);

					var statisticsEntry = new UserStatisticsEntry(
						user,
						response.CacheInfo.CachedAt,
						stats.XP,
						stats.GamesPlayed,
						stats.GamesWon,
						stats.TotalGamePlayDuration,
						tl.GamesPlayed,
						tl.GamesWon,
						tl.TetraLeagueRating,
						tl.GlickoRating,
						tl.GlickoRatingDeviation,
						tl.UserRank,
						tl.AverageRollingAPM,
						tl.AverageRollingPPS,
						tl.AverageRollingVSScore,
						tl.GlobalLeaderBoardsStanding,
						tl.LocalLeaderBoardsStanding,
						tl.Percentile,
						tl.PercentileRank);

					_coreContext.UserStatisticsEntries.Add(statisticsEntry);
					_coreContext.SaveChanges();

					fileStream.WriteLine(csv);
					fileStream.Close();

					XConsole
						.WriteLine("File write/Database insert complete.", Swatch.LightGreen);

					static void CompareStatistic<TValue>(
						UserStatisticsEntry lastEntry,
						UserStatisticsEntry currentEntry,
						Expression<Func<UserStatisticsEntry, TValue>> property,
						string label = "",
						double multiplier = 1,
						bool invert = false)
						where TValue : IComparable<TValue>
					{
						var propertyFunc = property.Compile();

						var lastValue = propertyFunc(lastEntry);
						var currentValue = propertyFunc(currentEntry);

						if (lastValue is double lastDouble)
						{
							lastValue = Math.Round(lastDouble * multiplier, 2)
								.As<TValue>();
							currentValue = Math.Round(currentValue.As<double>() * multiplier, 2)
								.As<TValue>();
						}

						if (lastValue is int lastInt)
						{
							lastValue = (lastInt * multiplier).As<TValue>();
							currentValue = (currentValue.As<double>() * multiplier).As<TValue>();
						}

						if (lastValue.CompareTo(currentValue) != 0)
						{
							var effectiveLabel = !label.IsNullOrEmptyEx()
								? label
								: property.GetMemberInfo()
									.Name;

							XConsole
								.Write(effectiveLabel, Swatch.Cyan)
								.Write($" : (", Color.Azure)
								.Write($"{lastValue}", Swatch.Cyan)
								.Write($" -> ", Color.Azure)
								.Write($"{currentValue}", Swatch.Cyan)
								.Write($") ", Color.Azure);

							var columns = 25 - XConsole.CursorLeft;
							var spacing = " ".Repeat(columns);

							XConsole.Write(spacing);

							var sign = lastValue.CompareTo(currentValue) < 0 ? "+" : "";

							var increaseSwatch = invert ? Swatch.Red : Swatch.Teal;
							var decreaseSwatch = invert ? Swatch.Teal : Swatch.Red;

							switch (currentValue)
							{
								case int @int:
									{
										var difference = @int - lastValue.As<int>();
										XConsole.WriteLine($"\t\tChange: {sign}{difference} {label}",
											difference > 0 ? increaseSwatch : decreaseSwatch);

										break;
									}
								case double @double:
									{
										var difference = Math.Round(@double - lastValue.As<double>(), 2);
										XConsole.WriteLine($"\t\tChange: {sign}{difference} {label}",
											difference > 0 ? increaseSwatch : decreaseSwatch);

										break;
									}
							}
						}
					}

					if (lastRecord != null)
					{
						XConsole.WriteLine();
						CompareStatistic(lastRecord, statisticsEntry, t => t.TetraLeagueRating, "TR ");
						CompareStatistic(lastRecord, statisticsEntry, t => t.AverageRollingVsScore, "VS ");
						CompareStatistic(lastRecord, statisticsEntry, t => t.AverageRollingAPM, "APM");
						CompareStatistic(lastRecord, statisticsEntry, t => t.AverageRollingPPS, "PPS");
						CompareStatistic(lastRecord, statisticsEntry, t => t.GlickoRating, "GR ");
						CompareStatistic(lastRecord, statisticsEntry, t => t.GlickoRatingDeviation, "RD ", 1, true);
						CompareStatistic(lastRecord, statisticsEntry, t => t.Percentile, "%  ", 100, true);
					}

					var currentRankStr = response
						.Content
						.TetraLeagueStats
						.UserRank
						.ToUpper();

					var nextRankStr = response
						.Content
						.TetraLeagueStats
						.NextRank
						.ToUpper();

					var currentUserRank = UserRankGrade.ParseFromJsonFormat(currentRankStr);
					var currentUserRankThresholds = _trRankingsThresholds[currentUserRank];
					var currentRankTRScoreMin = currentUserRankThresholds.Maximum;

					var nextUserRank = UserRankGrade.ParseFromJsonFormat(nextRankStr);
					var nextUserRankThresholds = _trRankingsThresholds[nextUserRank];
					var nextRankTRScore = nextUserRankThresholds.Maximum;


					var trUntilNextRank = nextRankTRScore - statisticsEntry.TetraLeagueRating;

					var trUntilPreviousRank = statisticsEntry.TetraLeagueRating - currentRankTRScoreMin;

					XConsole
						.WriteLine()
						.WriteLine(
							$"{Math.Round(trUntilNextRank)} TR until {nextRankStr} rank (~{Math.Round(trUntilNextRank / 250d)} match wins)",
							Swatch.Yellow);

					XConsole.WriteLine(
							$"{Math.Round(trUntilPreviousRank)} TR loss until losing {currentRankStr} rank (~{Math.Round(trUntilPreviousRank / 250d)} match wins)",
							Swatch.Yellow)
						.WriteLine()
						.WriteLine();

					var currentTR = statisticsEntry.TetraLeagueRating;

					var trThroughCurrentRank =
						currentTR - currentUserRankThresholds.Maximum;

					var rankSizeTR = currentUserRankThresholds.Minimum
						- currentUserRankThresholds.Maximum;

					var progressionThroughCurrentRank = trThroughCurrentRank / rankSizeTR;

					var outerSpacing = 4 * 2;

					var graphWidthColumns = XConsole.BufferWidth - outerSpacing;

					var currentProgressionColumn = graphWidthColumns * progressionThroughCurrentRank;
					var targetPositionColumn = (int)Math.Round(currentProgressionColumn);

					XConsole
						.AddFrame()
						.WriteLine("TL Rank Progression: ", Color.MediumSlateBlue)
						.WriteLine()
						.SetBold()
						.Write(currentUserRank.ToString()
								.PadRight(4),
							Swatch.LightBlue)
						.SetNormalIntensity()
						.SetBold()
						.Write("=".Repeat(targetPositionColumn - 2), Color.MediumSeaGreen);

					var markerPosition = XConsole.CursorLeft + 1;

					XConsole
						.Write("██", Swatch.Pink)
						.SetNormalIntensity()
						.SetFaint()
						.Write("▬".Repeat(XConsole.BufferWidth - XConsole.CursorLeft - 4), Color.DimGray)
						.SetBold()
						.WriteLine(nextUserRank.ToString()
								.PadLeft(4),
							Color.MediumSpringGreen)
						.SetNormalIntensity()
						.WriteLine();

					XConsole
						.Write(" ".Repeat(4), Swatch.Pink)
						.Write("| ", Color.DarkGray)
						.Write(currentUserRankThresholds.Maximum.Round() + " TR", Color.MediumTurquoise);

					var currentTRDisplayed = ((int)currentTR.Round());
					var centeringLength = (int)((currentTRDisplayed.ToString()
						.Length + 3) / 2d).Floor();

					XConsole.SetCursorPosition(
							markerPosition - centeringLength,
							XConsole.CursorTop)
						.Write(currentTRDisplayed + " TR", Color.MediumVioletRed);

					var nextRankTRDisplayed = (int)currentUserRankThresholds.Minimum.Round();

					XConsole.SetCursorPosition(
						XConsole.BufferWidth
						- 4 - 2 - 2
						- nextRankTRDisplayed.ToString()
							.Length,
						XConsole.CursorTop);

					XConsole
						.Write(nextRankTRDisplayed + " TR", Color.MediumSeaGreen)
						.Write(" |", Color.DarkGray)
						.RemoveFrame()
						.WriteLine();


					XConsole
						.WriteLine("Waiting until API caches expiration...", Swatch.Pink)
						.WriteLine();
				}

				else
				{
					Console.SetCursorPosition(0, Console.CursorTop - 1);

					var longestTimeRemaining = timeRemaining > timeRemaining40L ? timeRemaining : timeRemaining40L;

					XConsole
						.Write(
							$"{longestTimeRemaining.Minutes}:{longestTimeRemaining.Seconds:00} remaining",
							Swatch.Orange)
						.Write(
							$" | (US: ",
							Swatch.Cyan)
						.Write(
							$"{timeRemaining.Minutes}:{timeRemaining.Seconds:00}",
							Swatch.Pink)
						.Write(
							$", RGS: ",
							Swatch.Cyan)
						.Write(
							$"{timeRemaining.Minutes}:{timeRemaining.Seconds:00}",
							Swatch.Orange)
						.WriteLine(
							$")",
							Swatch.Cyan);
				}

				Thread.Sleep(1000);
			}
		}


		private static void LogGameRecord(
			GameRecord gameRecord)
		{
			var gr = gameRecord;

			XConsole
				.Outdent()
				.BeginJsonSession()
				.WriteCode("{", JsonCodeKind.Brace);

			XConsole
				.BeginJsonSession()
				.WriteProperty(gr, t => t.GameType)
				.WriteProperty(gr, t => t.TetrioGameRecordID)
				.WriteProperty(gr, t => t.StreamKey)
				.WriteProperty(gr, t => t.ReplayID)
				.WriteProperty(gr, t => t.UserID)
				.WriteProperty(gr, t => t.Username)
				.WriteProperty(gr, t => t.TimeStamp);

			switch (gameRecord.GameType)
			{
				case GameType.Single40Lines:
					{
						XConsole
							.BeginJsonSession()
							.WriteProperty(gr, t => t.FinalTime)
							.WriteProperty(gr, t => t.LinesCleared)
							.WriteProperty(gr, t => t.Inputs)
							.WriteProperty(gr, t => t.Holds)
							.WriteProperty(gr, t => t.Score)
							.WriteProperty(gr, t => t.Combo)
							.WriteProperty(gr, t => t.CurrentComboPower)
							.WriteProperty(gr, t => t.TopCombo)
							.WriteProperty(gr, t => t.BTB)
							.WriteProperty(gr, t => t.TopBTB)
							.WriteProperty(gr, t => t.TotalPiecesPlaced)

							.WriteProperty(gr, t => t.LineClearsSingles)
							.WriteProperty(gr, t => t.LineClearsDoubles)
							.WriteProperty(gr, t => t.LineClearsTriples)
							.WriteProperty(gr, t => t.LineClearsQuads)

							.WriteProperty(gr, t => t.TSpins)
							.WriteProperty(gr, t => t.LineClearsRealTSpins)
							.WriteProperty(gr, t => t.LineClearsMiniTSpins)
							.WriteProperty(gr, t => t.LineClearsMiniTSpinSingles)
							.WriteProperty(gr, t => t.LineClearsTSpinSingles)
							.WriteProperty(gr, t => t.LineClearsMiniTSpinDoubles)
							.WriteProperty(gr, t => t.LineClearsTSpinDoubles)
							.WriteProperty(gr, t => t.LineClearsTSpinTriples)
							.WriteProperty(gr, t => t.LineClearsTSpinQuads)

							.WriteProperty(gr, t => t.LineClearsAllClears)

							.WriteProperty(gr, t => t.FinesseCombo)
							.WriteProperty(gr, t => t.FinesseFaults)
							.WriteProperty(gr, t => t.FinessePerfectPieces);
						break;
					}

			}

			XConsole
				.BeginJsonSession()
				.WriteProperty(gr, t => t.GameType)
				.WriteProperty(gr, t => t.TetrioGameRecordID)
				.WriteProperty(gr, t => t.StreamKey)
				.WriteProperty(gr, t => t.ReplayID)
				.WriteProperty(gr, t => t.UserID)
				.WriteProperty(gr, t => t.Username)
				.WriteProperty(gr, t => t.TimeStamp)

				.WriteProperty(gr, t => t.IsMultiplayer)
				.WriteProperty(gr, t => t.FinalTime)
				.WriteProperty(gr, t => t.Kills)
				.WriteProperty(gr, t => t.LinesCleared)
				.WriteProperty(gr, t => t.LevelLines)
				.WriteProperty(gr, t => t.LevelLinesNeeded)
				.WriteProperty(gr, t => t.Inputs)
				.WriteProperty(gr, t => t.Holds)
				.WriteProperty(gr, t => t.Score)
				.WriteProperty(gr, t => t.ZenLevel)
				.WriteProperty(gr, t => t.ZenProgress)
				.WriteProperty(gr, t => t.Level)
				.WriteProperty(gr, t => t.Combo)
				.WriteProperty(gr, t => t.CurrentComboPower)
				.WriteProperty(gr, t => t.TopCombo)
				.WriteProperty(gr, t => t.BTB)
				.WriteProperty(gr, t => t.TopBTB)
				.WriteProperty(gr, t => t.TSpins)
				.WriteProperty(gr, t => t.TotalPiecesPlaced)

				.WriteProperty(gr, t => t.LineClearsSingles)
				.WriteProperty(gr, t => t.LineClearsDoubles)
				.WriteProperty(gr, t => t.LineClearsTriples)
				.WriteProperty(gr, t => t.LineClearsQuads)
				.WriteProperty(gr, t => t.LineClearsRealTSpins)
				.WriteProperty(gr, t => t.LineClearsMiniTSpins)
				.WriteProperty(gr, t => t.LineClearsMiniTSpinSingles)
				.WriteProperty(gr, t => t.LineClearsTSpinSingles)
				.WriteProperty(gr, t => t.LineClearsMiniTSpinDoubles)
				.WriteProperty(gr, t => t.LineClearsTSpinDoubles)
				.WriteProperty(gr, t => t.LineClearsTSpinTriples)
				.WriteProperty(gr, t => t.LineClearsTSpinQuads)
				.WriteProperty(gr, t => t.LineClearsAllClears)

				.WriteProperty(gr, t => t.GarbageTotalSent)
				.WriteProperty(gr, t => t.GarbageTotalReceived)
				.WriteProperty(gr, t => t.GarbageAttack)
				.WriteProperty(gr, t => t.GarbageTotalCleared)

				.WriteProperty(gr, t => t.FinesseCombo)
				.WriteProperty(gr, t => t.FinesseFaults)
				.WriteProperty(gr, t => t.FinessePerfectPieces);

			XConsole
				.Outdent()
				.BeginJsonSession()
				.WriteCode("  }\n}", JsonCodeKind.Brace);

			XConsole
				.WriteLine();
		}


		public override void Dispose()
		{
			_coreContext.Dispose();
			_client.Dispose();

			base.Dispose();
		}
	}
}
