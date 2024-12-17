//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading;
//using Ccr.Colorization.Mappings;
//using Ccr.Std.Core.Extensions;
//using Ccr.Std.Core.Numerics.Ranges;
//using Ccr.Terminal.Application;
//using Ccr.Terminal.Extensions;
//using TetrioStats.Api;
//using TetrioStats.Api.Infrastructure;
//using TetrioStats.Data.Context;
//using TetrioStats.Data.Domain;
//using TetrioStats.Api.Extensions;
//using static Ccr.Terminal.ExtendedConsole;
//using EnumExtensions = TetrioStats.Api.Extensions.EnumExtensions;

//namespace TetrioStats.Terminal.Commands
//{
//	public class MonitorCommand
//		: TerminalCommand<string>
//	{
//		private static readonly TetrioApiClient _client = new();
//		private static readonly LocalCoreContext _coreContext = new();

//		private static DateTime _timeCacheExpires;
//		private static DateTime _time40LCacheExpires;

//		private UserStatisticsEntry _firstStatisticsEntryInSession;

//		private IReadOnlyDictionary<UserRank, DoubleRange> _trRankingsThresholds;
//		private static readonly IReadOnlyDictionary<string, Color> _rankToStringMap
//			= new Dictionary<string, Color>
//			{
//				["D"] = FromHex("#745e73"),
//				["D+"] = FromHex("#6f4c6e"),
//				["C-"] = FromHex("#5b3668"),
//				["C"] = FromHex("#542064"),
//				["C+"] = FromHex("#421b60"),
//				["B-"] = FromHex("#4c3dab"),
//				["B"] = FromHex("#3b4c9f"),
//				["B+"] = FromHex("#3f709c"),
//				["A-"] = FromHex("#2e9379"),
//				["A"] = FromHex("#369145"),
//				["A+"] = FromHex("#3a9d2f"),
//				["S-"] = FromHex("#9e8925"),
//				["S"] = FromHex("#d19e26"),
//				["S+"] = FromHex("#cea125"),
//				["SS"] = FromHex("#df9020"),
//				["U"] = FromHex("#ae5128"),
//				["X"] = FromHex("#aa41b1"),
//                ["X+"] = FromHex("#aa41b1"),
//                ["Z"] = FromHex("#737373"),
//			};


//		private static Color FromHex(string hex)
//		{
//			return ColorTranslator.FromHtml(hex);
//		}

//		public override string CommandName => "monitor-user";

//		public override string ShortCommandName => "mu";


//		public override void Execute(string args)
//		{
//			XConsole.Write(" Enter a username or userID to track: ", Color.MediumTurquoise);

//			var userName = XConsole.ReadLine();
//			userName = userName.ToLower();

//			var userInfo = _client.Users.FetchUserInfoAsync(userName).Result;

//			XConsole.Write(" Fetching TR Ranking Thresholds... ", Color.MediumTurquoise);

//			_trRankingsThresholds = UserRankExtensions.CalculateRankGradeTRThresholds();

//			XConsole.WriteLine("Done.", Color.MediumSpringGreen);


//			var reversedTrRankingsThresholds = _trRankingsThresholds
//                .Reverse()
//				.ToArray();

//			UserRank lastThreshold = null;

//			foreach (var (key, value) in reversedTrRankingsThresholds)
//			{
//				var currentGradeStr = key.ToString();
//				var color = _rankToStringMap[currentGradeStr];

//				var darkened = color.Darken(0.7);
//				var lightened = color.Lighten(0.8);

//				var rankPercentile = lastThreshold == null
//					? key.RankPercentile
//					: key.RankPercentile - lastThreshold.RankPercentile;

//				XConsole
//					.ScopedBold(c1 =>
//					{
//						c1.ScopedBackground(darkened,
//							c2 =>
//							{
//								c2.Write($"     {key,-2}    |    ", lightened)
//									.Write($"{value.Maximum.Round().ToString().PadLeft(5)} TR    ", color.Lighten(0.97))
//									.Write($"|    ({rankPercentile} % of player base)", lightened)
//									.WriteLine("-".Repeat(Console.WindowWidth - Console.CursorLeft), darkened);
//							});
//					});

//				lastThreshold = key;
//			}

//			XConsole
//				.WriteLine()
//				.Write(" Monitoring Account: ", Swatch.Cyan)
//				.Write(userInfo.Data.Username, Swatch.Amber)
//				.Write(" UserID: ", Swatch.Cyan)
//				.WriteLine(userInfo.Data.Id, Swatch.Teal);

//			var firstQuery = true;

//			_timeCacheExpires = DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(1));
//			_time40LCacheExpires = DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(1));

//			while (true)
//			{
//				var timeRemaining = _timeCacheExpires - DateTime.UtcNow;
//				var timeRemaining40L = _time40LCacheExpires - DateTime.UtcNow;

//				if (firstQuery || (timeRemaining <= TimeSpan.Zero
//						&& timeRemaining40L <= TimeSpan.Zero)
//				) //_timeSinceQuery.Elapsed > TimeSpan.FromMinutes(5.1)
//				{
//					firstQuery = false;

//					XConsole
//						.WriteLine()
//						.WriteLine(" Querying API...  ", Swatch.Cyan)
//						.WriteLine();

//					var response = _client.Users.Summaries.FetchLeagueSummaryAsync(userInfo.Data.Id).Result;

//                    _timeCacheExpires = response.CacheInfo.CachedUntil;


//					var _40LinesResponse = _client.Users.Summaries.FetchSprintSummaryAsync(userInfo.Data.Id).Result;

//					_time40LCacheExpires = _40LinesResponse.CacheInfo.CachedUntil;


//					var stats = response.Content;
//					var tl = stats.TetraLeagueStats;

//					var recordCount = _coreContext.UserStatisticsEntries.Count();

//					var lastRecords = _coreContext.UserStatisticsEntries
//						.Where(t => t.UserStatisticsEntryID == recordCount);

//					var lastRecord = lastRecords.SingleOrDefault();


//					var user = _client.FetchUser(stats.Username);

//					var statisticsEntry = new UserStatisticsEntry(
//						user,
//						response.CacheInfo.CachedAt,
//						stats.XP,
//						stats.GamesPlayed,
//						stats.GamesWon,
//						stats.TotalGamePlayDuration,
//						tl.GamesPlayed,
//						tl.GamesWon,
//						tl.TetraLeagueRating,
//						tl.GlickoRating,
//						tl.GlickoRatingDeviation,
//						tl.UserRank,
//						tl.AverageRollingAPM,
//						tl.AverageRollingPPS,
//						tl.AverageRollingVSScore,
//						tl.GlobalLeaderBoardsStanding,
//						tl.LocalLeaderBoardsStanding,
//						tl.Percentile,
//						tl.PercentileRank);

//					if (_firstStatisticsEntryInSession == null)
//					{
//						_firstStatisticsEntryInSession = statisticsEntry;
//					}

//					if (lastRecord != null && lastRecord.Equals(statisticsEntry))
//					{
//						XConsole
//							.WriteLine(" Data has not changed.", Swatch.Cyan)
//							.WriteLine();

//						continue;
//					}

//                    var tlSummary = _client.Users.Summaries.FetchLeagueSummaryAsync(userInfo.Data.Username).Result;

//					var json = "{"
//						+ $"\n  \"Username\": {userInfo.Data.Username}, "
//						+ $"\n  \"TimeStamp\": \"{response.CacheInfo.CachedAt:O}\", "
//						+ $"\n  \"GamesPlayed\": {response.Data.GamesPlayed}, "
//						+ $"\n  \"GamesWon\": {response.Data.GamesWon}, "
//						+ $"\n  \"TotalGamePlayDuration\": \"{response.Data.GamePlayTime:g}\", "
//						+ $"\n  \"XP\": {userInfo.Data.XP}, "
//						//+ $"\n  \"TimeStamp\": \"{response.Response.Response.TimeStamp:O}\", "
//						+ $"\n  \"TetraLeagueStats\": {{"
//						+ $"\n    \"GamesPlayed\": {tlSummary.Data.GamesPlayed}, "
//						+ $"\n    \"GamesWon\": {tlSummary.Data.GamesWon}, "
//						+ $"\n    \"TetrisRating\": {tlSummary.Data.TR}, "
//						+ $"\n    \"GlickoRating\": {tlSummary.Data.Glicko}, "
//						+ $"\n    \"GlickoRatingDeviation\": {tlSummary.Data.RatingDeviation}, "
//						+ $"\n    \"UserRank\": \"{tlSummary.Data.Rank}\", "
//						+ $"\n    \"AverageRollingAPM\": {tlSummary.Data.APM}, "
//						+ $"\n    \"AverageRollingPPS\": {tlSummary.Data.PPS}, "
//						+ $"\n    \"AverageRollingVSScore\": {tlSummary.Data.PPS}, "
//						+ $"\n    \"GlobalLeaderBoardsStanding\": {tlSummary.Data.Standing}, "
//						+ $"\n    \"LocalLeaderBoardsStanding\": {tlSummary.Data.LocalStanding}, "
//						+ $"\n    \"Percentile\": {tlSummary.Data.Percentile}, "
//						+ $"\n    \"PercentileRank\": \"{tlSummary.Data.PercentileRank}\"" +
//						"\n  }" +
//						"\n},";


//					var jsonFileInfo = new FileInfo(@"C:\Tetris\tetrio.statistics.json");

//					using var jsonFileStream = jsonFileInfo.Exists
//						? jsonFileInfo.AppendText()
//						: jsonFileInfo.CreateText();

//					jsonFileStream.WriteLine(json);
//					jsonFileStream.Close();

//					var fi = new FileInfo(@"C:\Tetris\tetrio.statistics.csv");

//					using var fileStream = fi.Exists ? fi.AppendText() : fi.CreateText();

//					var csv = $"{userName.Quote()},"
//						+ $"\"{response.CacheInfo.CachedAt:O}\","
//						+ $"{response.Data.GamesPlayed},"
//						+ $"{response.Data.GamesWon},"
//						+ $"\"{response.Data.GamePlayTime:g}\","
//						+ $"{userInfo.Data.XP},"
//						//+ $"\"{stats.TimeStamp:O}\","
//						+ $"{tlSummary.Data.GamesPlayed},"
//						+ $"{tlSummary.Data.GamesWon},"
//						+ $"{tlSummary.Data.TR},"
//						+ $"{tlSummary.Data.Glicko},"
//						+ $"{tlSummary.Data.RatingDeviation},"
//						+ $"{tlSummary.Data.Rank.ToString().Quote()},"
//						+ $"{tlSummary.Data.APM},"
//						+ $"{tlSummary.Data.PPS},"
//						+ $"{tlSummary.Data.VS},"
//						+ $"{tlSummary.Data.Standing},"
//						+ $"{tlSummary.Data.LocalStanding},"
//						+ $"{tlSummary.Data.Percentile},"
//						+ $"{tlSummary.Data.PercentileRank.ToString().Quote()}";

//					_coreContext.UserStatisticsEntries.Add(statisticsEntry);
//					_coreContext.SaveChanges();

//					fileStream.WriteLine(csv);
//					fileStream.Close();

//					XConsole
//						.WriteLine(" File write/Database insert complete.", Swatch.LightGreen);

//					static void CompareStatistic<TValue>(
//						UserStatisticsEntry lastEntry,
//						UserStatisticsEntry currentEntry,
//						Expression<Func<UserStatisticsEntry, TValue>> property,
//						string label = "",
//						double multiplier = 1,
//						bool invert = false)
//						where TValue : IComparable<TValue>
//					{
//						var propertyFunc = property.Compile();

//						var lastValue = propertyFunc(lastEntry);
//						var currentValue = propertyFunc(currentEntry);

//						if (lastValue is double lastDouble)
//						{
//							lastValue = Math.Round(lastDouble * multiplier, 2).As<TValue>();
//							currentValue = Math.Round(currentValue.As<double>() * multiplier, 2).As<TValue>();
//						}

//						if (lastValue is int lastInt)
//						{
//							lastValue = (lastInt * multiplier).As<TValue>();
//							currentValue = (currentValue.As<double>() * multiplier).As<TValue>();
//						}

//						if (lastValue.CompareTo(currentValue) != 0)
//						{
//							var effectiveLabel = !label.IsNullOrEmptyEx()
//								? label
//								: property.GetMemberInfo()
//									.Name;

//							XConsole
//								.Write(effectiveLabel, Swatch.Cyan)
//								.Write($" : (", Color.Azure)
//								.Write($"{lastValue}", Swatch.Cyan)
//								.Write($" -> ", Color.Azure)
//								.Write($"{currentValue}", Swatch.Cyan)
//								.Write($") ", Color.Azure);

//							var columns = 25 - XConsole.CursorLeft;
//							var spacing = " ".Repeat(columns);

//							XConsole.Write(spacing);

//							var sign = lastValue.CompareTo(currentValue) < 0 ? "+" : "";

//							var increaseSwatch = invert ? Swatch.Red : Swatch.Teal;
//							var decreaseSwatch = invert ? Swatch.Teal : Swatch.Red;

//							switch (currentValue)
//							{
//								case int @int:
//									{
//										var difference = @int - lastValue.As<int>();
//										XConsole.WriteLine($"\t\tChange: {sign}{difference} {label}",
//											difference > 0 ? increaseSwatch : decreaseSwatch);

//										break;
//									}
//								case double @double:
//									{
//										var difference = Math.Round(@double - lastValue.As<double>(), 2);
//										XConsole.WriteLine($"\t\tChange: {sign}{difference} {label}",
//											difference > 0 ? increaseSwatch : decreaseSwatch);

//										break;
//									}
//							}
//						}
//					}

//					if (lastRecord != null)
//					{
//						XConsole.WriteLine();
//						CompareStatistic(lastRecord, statisticsEntry, t => t.TetraLeagueRating, "TR ");
//						CompareStatistic(lastRecord, statisticsEntry, t => t.AverageRollingVsScore, "VS ");
//						CompareStatistic(lastRecord, statisticsEntry, t => t.AverageRollingAPM, "APM");
//						CompareStatistic(lastRecord, statisticsEntry, t => t.AverageRollingPPS, "PPS");
//						CompareStatistic(lastRecord, statisticsEntry, t => t.GlickoRating, "GR ");
//						CompareStatistic(lastRecord, statisticsEntry, t => t.GlickoRatingDeviation, "RD ", 1, true);
//						CompareStatistic(lastRecord, statisticsEntry, t => t.Percentile, "%  ", 100, true);

//						CompareStatistic(
//							_firstStatisticsEntryInSession,
//							statisticsEntry,
//							t => t.TetraLeagueRating,
//							"Session TR ");
//					}

//					var currentRankStr = tlSummary.Data.Rank.ToString();
//					var nextRankStr = response.Data.NextRank.ToString();

//					var currentUserRank = EnumExtensions.ParseFromDescription<UserRank>(currentRankStr);
//					var currentUserRankThresholds = _trRankingsThresholds[currentUserRank];
//					var currentRankTRScoreMin = currentUserRankThresholds.Maximum;

//					var nextUserRank = EnumExtensions.ParseFromDescription<UserRank>(nextRankStr);
//                    var nextUserRankThresholds = _trRankingsThresholds[nextUserRank];
//					var nextRankTRScore = nextUserRankThresholds.Maximum;


//					var trUntilNextRank = nextRankTRScore - statisticsEntry.TetraLeagueRating;

//					var trUntilPreviousRank = statisticsEntry.TetraLeagueRating - currentRankTRScoreMin;

//					XConsole
//						.WriteLine()
//						.WriteLine(
//							$" {Math.Round(trUntilNextRank)} TR until {nextRankStr} rank (~{Math.Round(trUntilNextRank / 250d, 1)} match wins)",
//							Swatch.Yellow);

//					XConsole.WriteLine(
//							$" {Math.Round(trUntilPreviousRank, 1)} TR loss until losing {currentRankStr} rank (~{Math.Round(trUntilPreviousRank / 250d, 1)} match losses)",
//							Swatch.Yellow)
//						.WriteLine()
//						.WriteLine();

//					var currentTR = statisticsEntry.TetraLeagueRating;

//					var trThroughCurrentRank =
//						currentTR - currentUserRankThresholds.Maximum;

//					var rankSizeTR = currentUserRankThresholds.Minimum
//						- currentUserRankThresholds.Maximum;

//					var progressionThroughCurrentRank = trThroughCurrentRank / rankSizeTR;

//					var outerSpacing = 4 * 2;

//					var graphWidthColumns = XConsole.WindowWidth - outerSpacing;

//					var currentProgressionColumn = graphWidthColumns * progressionThroughCurrentRank;
//					var targetPositionColumn = (int)Math.Round(currentProgressionColumn);

//					XConsole
//						.WriteLine(" TL Rank Progression: ", Color.MediumSlateBlue.Lighten(0.5))
//						.WriteLine()
//						.ScopedBackground(Color.MediumSeaGreen.Darken(0.85), c1 =>
//						{
//							c1.ScopedBold(c2 =>
//							{
//								c2.Write(currentUserRank.ToString().CenterAlign(4), Swatch.LightBlue);
//							}).ScopedBackground(Color.MediumSeaGreen.Darken(0.75), c3 =>
//							{
//								c3.Write("=".Repeat(targetPositionColumn - 2), Color.MediumSeaGreen);
//							});
//						});

//					var markerPosition = XConsole.CursorLeft + 1;

//					XConsole
//						.Write("██", Swatch.Pink)
//						.SetFaint()
//						.Write("▬".Repeat(XConsole.WindowWidth - XConsole.CursorLeft - 4), Color.DimGray.Darken(0.7))
//						.ScopedBackground(Color.MediumSeaGreen.Darken(0.85), c1 =>
//						{
//							c1.ScopedBold(c2 =>
//							{
//								c2.WriteLine(nextUserRank.ToString().CenterAlign(4), Swatch.LightBlue);
//							});
//						});

//					XConsole
//						.Write(" ".Repeat(4), Swatch.Pink)
//						.Write("| ", Color.DarkGray)
//						.Write(currentUserRankThresholds.Maximum.Round() + " TR", Color.MediumTurquoise);

//					var currentTRDisplayed = (int)currentTR.Round();
//					var centeringLength = (int)((currentTRDisplayed.ToString()
//						.Length + 3) / 2d).Floor();

//					XConsole.SetCursorPosition(
//							markerPosition - centeringLength,
//							XConsole.CursorTop)
//						.ScopedBackground(Color.MediumVioletRed.Darken(0.8), c1 =>
//						{
//							c1.Write(currentTRDisplayed + " TR", Color.MediumVioletRed);
//						}).SetBackgroundColor(Color.Black);

//					var nextRankTRDisplayed = (int)currentUserRankThresholds.Minimum.Round();

//					XConsole.SetCursorPosition(
//						XConsole.WindowWidth
//						- 4 - 2 - 2
//						- nextRankTRDisplayed.ToString()
//							.Length,
//						XConsole.CursorTop);

//					XConsole
//						.Write(nextRankTRDisplayed + " TR", Color.MediumSeaGreen)
//						.Write("|", Color.DarkGray)
//						.WriteLine()
//						.WriteLine();

//					XConsole
//						.WriteLine(" Waiting until API caches expiration...", Swatch.Pink)
//						.WriteLine();
//				}

//				else
//				{
//					Console.SetCursorPosition(0, Console.CursorTop - 1);

//					var longestTimeRemaining = timeRemaining > timeRemaining40L ? timeRemaining : timeRemaining40L;

//					XConsole
//						.Write(
//							$" {longestTimeRemaining.Minutes}:{longestTimeRemaining.Seconds:00} remaining",
//							Swatch.Orange)
//						.Write($" | (US: ", Swatch.Cyan)
//						.Write($"{timeRemaining.Minutes}:{timeRemaining.Seconds:00}", Swatch.Pink)
//						.Write($", RGS: ", Swatch.Cyan)
//						.Write($"{timeRemaining.Minutes}:{timeRemaining.Seconds:00}", Swatch.Orange)
//						.WriteLine($")", Swatch.Cyan);
//				}

//				Thread.Sleep(1000);
//			}
//		}


//		public override void Dispose()
//		{
//			_coreContext.Dispose();

//			base.Dispose();
//		}
//	}
//}