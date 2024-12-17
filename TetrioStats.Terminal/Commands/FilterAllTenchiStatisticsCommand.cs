//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Text.RegularExpressions;
//using Ccr.Colorization.Mappings;
//using Ccr.Std.Core.Extensions;
//using Ccr.Terminal.Application;
//using TetrioStats.Api;
//using TetrioStats.Api.Domain.Json.TenchiStats;
//using TetrioStats.Data.Context;
//using TetrioStats.Data.Domain;
//using static Ccr.Terminal.ExtendedConsole;

//namespace TetrioStats.Terminal.Commands
//{
//	public class TenchiHistoricalStatsAggregator
//	{
//		private const int RecordCacheSize = 4000;


//		private readonly Dictionary<
//			string,
//			Dictionary<DateTime, TLStatsEntry>> _results;


//		public TenchiHistoricalStatsAggregator()
//		{
//			_results = new();
//		}


//		public void AddRecord(
//			TLStatsEntry record)
//		{
//			var userStatsGroup = _results
//				.SingleOrDefault(t => t.Key == record.UserID);

//			if (userStatsGroup.Value == default)
//			{
//				_results.Add(
//					record.UserID,
//					new());

//				userStatsGroup = _results
//					.SingleOrDefault(t => t.Key == record.UserID);
//			}

//			userStatsGroup.Value.Add(
//				record.DateTimeUtc,
//				record);
//		}
//	}

//	public class FilterAllTenchiStatisticsCommand
//		: TerminalCommand<string>
//	{
//		private static readonly DirectoryInfo _tetrioStatsDirectory = new(
//			@"Y:\tetrio-stats\");

//		private static readonly TetrioApiClient _client = new();
//		private static readonly CoreContext _coreContext = new();
//		private static readonly Regex _dateTimeRegex = new(
//			@"^(?<year>[\d]{4})-(?<month>[\d]{2})-(?<day>[\d]{2})T(?<hour>[\d]{2})(?<min>[\d]{2})(?<sec>[\d]{2})$");

//		private static readonly Dictionary<string, TLStatsEntry> _cachedLastUserRecords = new();

//		public override string CommandName => "tenchi-filter-all-stats";

//		public override string ShortCommandName => "tfas";


//		private static readonly IReadOnlyList<Color> _csvColors
//			= new List<Color>
//			{
//				FromHex("#745e73"),
//				FromHex("#6f4c6e"),
//				//FromHex("#5b3668"),
//				FromHex("#542064"),
//				//FromHex("#421b60"),
//				//FromHex("#4c3dab"),
//				FromHex("#3b4c9f"),
//				FromHex("#3f709c"),
//				FromHex("#2e9379"),
//				FromHex("#369145"),
//				FromHex("#3a9d2f"),
//				FromHex("#9e8925"),
//				FromHex("#d19e26"),
//				FromHex("#cea125"),
//				FromHex("#df9020"),
//				FromHex("#ae5128"),
//				FromHex("#aa41b1"),
//				FromHex("#737373"),
//			};

//		private static Color FromHex(string hex)
//		{
//			return ColorTranslator.FromHtml(hex).Lighten(0.6);
//		}


//		public override void Execute(string args)
//		{
//			var colorIndex = -1;

//			Color nextColor()
//			{
//				colorIndex++;
//				return _csvColors[colorIndex];
//			}


//			XConsole.Write(" Analyzing Json File: ", Color.MediumTurquoise);

//			//XConsole
//			//	.Write($"Username, ", nextColor())
//			//	.Write($"Datetime, ", nextColor())
//			//	//.Write($"XP, ", nextColor())
//			//	.Write($"GP, ", nextColor())
//			//	.Write($"GW, ", nextColor())
//			//	.Write($"TR, ", nextColor())
//			//	.Write($"Glicko, ", nextColor())
//			//	.Write($"RD, ", nextColor())
//			//	.Write($"Rank, ", nextColor())
//			//	.Write($"APM, ", nextColor())
//			//	.Write($"PPS, ", nextColor())
//			//	.Write($"VS", nextColor())
//			//	.WriteLine();

//			colorIndex = -1;

//			var stopwatch = new Stopwatch();
//			stopwatch.Start();

//			var singleFileStopwatch = new Stopwatch();
//			singleFileStopwatch.Start();

//			var directories = _tetrioStatsDirectory.GetDirectories();
//			var startIndex = 0;
//			var index = 0;
//			var foundRecords = 0;
//			var skipFirstInsert = false;

//			foreach (var subDirectory in directories)
//			//.SkipWhile(t => t.Name != "2021-09-16T170001"))
//			{
//				//Console.SetCursorPosition(0, Console.CursorTop);



//				XConsole.Write(" Analyzing Json File: ", Color.MediumTurquoise);

//				XConsole.WriteLine($"{subDirectory.Name}", Swatch.Cyan);


//				index++;

//				if (!TryGetDateTime(subDirectory, out var dateTime, out var jsonFileInfo))
//				{
//					continue;
//				}

//				try
//				{

//					using var fileReader = jsonFileInfo.OpenText();
//					var fileContent = fileReader.ReadToEnd();

//					var statsFile = TenchiHistoricalStatisticsFile.FromJson(fileContent);

//					if (statsFile == null || statsFile.Success == false)
//					{
//					}
//					else
//					{
//						foreach (var s in statsFile.UserStats)
//						{
//							var convertedStatsEntry = new TLStatsEntry(
//								s.UserID,
//								s.Username,
//								dateTime.Value,
//								s.XP,
//								s.Country,
//								s.League.GamesPlayed,
//								s.League.GamesWon,
//								s.League.TLRating,
//								s.League.GlickoRating,
//								s.League.GlickoRatingDeviation,
//								s.League.UserRank,
//								s.League.AverageRollingAPM,
//								s.League.AverageRollingPPS,
//								s.League.AverageRollingVSScore);

//							if (convertedStatsEntry.Country == null)
//							{
//								convertedStatsEntry.Country = "XX";
//							}

//							if (_cachedLastUserRecords.TryGetValue(convertedStatsEntry.UserID, out var lastRecord))
//							{
//								if (lastRecord.GP != convertedStatsEntry.GP)
//								{
//									if (skipFirstInsert)
//									{
//										_coreContext.TLStatsEntries.Add(convertedStatsEntry);
//									}

//									foundRecords++;
//								}
//							}
//							else
//							{
//								if (skipFirstInsert)
//								{
//									_coreContext.TLStatsEntries.Add(convertedStatsEntry);
//								}

//								_cachedLastUserRecords.Add(
//									convertedStatsEntry.UserID,
//									convertedStatsEntry);

//								foundRecords++;
//							}

//							_cachedLastUserRecords[convertedStatsEntry.UserID] = convertedStatsEntry;
//						}

//						var affected = _coreContext.SaveChanges();

//						var percentage = ((double)index - startIndex + 1)
//							/ ((double)directories.Length - startIndex)
//							* 100d;

//						XConsole
//							.WriteLine($"{percentage:F3}%", Color.MediumTurquoise)
//							.Write(" | ", Color.Azure)
//							.Write($" {(index - startIndex + 1)} / {(directories.Length - startIndex)}", Color.DeepPink)
//							.Write(" | ", Color.Azure)
//							.Write($"{foundRecords}", Color.MediumPurple)
//							.Write(" uniques from ", Color.MediumSpringGreen)
//							.Write($"{_cachedLastUserRecords.Count}", Color.Yellow)
//							.Write(" users.", Color.MediumSpringGreen)
//							.Write(" | ", Color.Azure)
//							.Write(" SQL Insert: ", Color.MediumTurquoise)
//							.Write($"{affected}", Color.Orange)
//							.Write(" rows affected.", Color.MediumTurquoise);
//					}

//					XConsole
//						.WriteLine()
//						.WriteLine();

//					XConsole.WriteLine($" File Duration:  {singleFileStopwatch.Elapsed:c}", Color.MediumPurple);
//					XConsole.WriteLine($" Total Duration: {stopwatch.Elapsed:c}", Color.Yellow);

//					singleFileStopwatch.Restart();

//					skipFirstInsert = true;

//				}
//				catch (Exception ex)
//				{
//					XConsole.WriteLine(" EXCEPTION OCCURRED READING FILE: " + ex, Color.Red);
//				}
//			}


//			XConsole.WriteLine(" Done.", Color.MediumSpringGreen);

//			XConsole.WriteLine(" -----------------------", Color.MediumPurple);

//			XConsole.WriteLine($" Total Duration: {stopwatch.Elapsed:c}", Color.Yellow);

//			singleFileStopwatch.Stop();
//			stopwatch.Stop();
		
//	}



//		private static bool TryGetDateTime(
//				DirectoryInfo directory,
//				out DateTime? dateTime,
//				out FileInfo jsonFileInfo)
//		{
//			if (!_dateTimeRegex.IsMatch(directory.Name))
//			{
//				XConsole.WriteLine($" Cannot Parse DateTime: \"{directory.Name}\"", Color.DeepPink);

//				dateTime = null;
//				jsonFileInfo = null;

//				return false;
//			}

//			var match = _dateTimeRegex.Match(directory.Name);

//			var year = int.Parse(match.Groups["year"].Value);
//			var month = int.Parse(match.Groups["month"].Value);
//			var day = int.Parse(match.Groups["day"].Value);
//			var hour = int.Parse(match.Groups["hour"].Value);
//			var min = int.Parse(match.Groups["min"].Value);
//			var sec = int.Parse(match.Groups["sec"].Value);

//			dateTime = new DateTime(year, month, day, hour, min, sec);

//			var jsonFile = new FileInfo($@"{directory.FullName}\league.json");

//			if (!jsonFile.Exists)
//			{
//				//XConsole.WriteLine($" Cannot find path: \"{jsonFile.FullName}\"", Color.DeepPink);

//				dateTime = null;
//				jsonFileInfo = null;

//				return false;
//			}

//			jsonFileInfo = jsonFile;
//			return true;
//		}


//		public override void Dispose()
//		{
//			_client.Dispose();
//			_coreContext.Dispose();

//			base.Dispose();
//		}
//	}
//}