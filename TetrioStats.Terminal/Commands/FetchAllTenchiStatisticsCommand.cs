using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Ccr.Colorization.Mappings;
using Ccr.Std.Core.Extensions;
using Ccr.Std.Core.Extensions.NumericExtensions;
using Ccr.Terminal.Application;
using TetrioStats.Api;
using TetrioStats.Api.Domain.Json.TenchiStats;
using TetrioStats.Data.Domain;
using static Ccr.Terminal.ExtendedConsole;
using DPoint = System.Drawing.Point;

namespace TetrioStats.Terminal.Commands
{
	public class FetchAllTenchiStatisticsCommand
		: TerminalCommand<string>
	{
		private static readonly DirectoryInfo _tetrioStatsDirectory = new DirectoryInfo(
			@"Y:\tetrio-stats\");

		private static readonly TetrioApiClient _client = new TetrioApiClient();
		private static readonly Regex _dateTimeRegex = new Regex(
			@"^(?<year>[\d]{4})-(?<month>[\d]{2})-(?<day>[\d]{2})T(?<hour>[\d]{2})(?<min>[\d]{2})(?<sec>[\d]{2})$");

		private static readonly IReadOnlyList<Color> _csvColors
			= new List<Color>
			{
				FromHex("#745e73"),
				FromHex("#6f4c6e"),
				//FromHex("#5b3668"),
				FromHex("#542064"),
				//FromHex("#421b60"),
				//FromHex("#4c3dab"),
				FromHex("#3b4c9f"),
				FromHex("#3f709c"),
				FromHex("#2e9379"),
				FromHex("#369145"),
				FromHex("#3a9d2f"),
				FromHex("#9e8925"),
				FromHex("#d19e26"),
				FromHex("#cea125"),
				FromHex("#df9020"),
				FromHex("#ae5128"),
				FromHex("#aa41b1"),
				FromHex("#737373"),
			};

		private static Color FromHex(string hex)
		{
			return ColorTranslator.FromHtml(hex).Lighten(0.6);
		}


		public override string CommandName => "tenchi-all-stats";

		public override string ShortCommandName => "tas";


		private FileInfo EfficientSeekFirstFile(
			User userInfo,
			DateTime userJoinDate)
		{
			const int maxSkip = 100;

			var directories = _tetrioStatsDirectory.GetDirectories();

			var sequentialMisses = 0;

			for (var index = 0; index < directories.Length; index++)
			{
				var subDirectory = directories[index];
				Console.SetCursorPosition(0, Console.CursorTop);

				XConsole.Write($"{subDirectory.Name}", Swatch.Pink)
					.Write($" | ", Color.Azure)
					.Write($"{index} / {(directories.Length)}", Swatch.Cyan)
					.Write($" | ", Color.Azure)
					.Write($"{sequentialMisses} Seq. Misses", Swatch.Teal);

				if (!TryGetDateTime(subDirectory, out var dateTime, out var jsonFileInfo))
				{
					continue;
				}

				if (dateTime < userJoinDate)
				{
					continue;
				}

				var record = TenchiHistoricalStatisticsFile.FromJsonHighEfficiency(
					jsonFileInfo.FullName,
					userInfo.TetrioUserID);

				if (record == null)
				{
					sequentialMisses++;

					if (sequentialMisses >= 3)
					{
						var skipAmount = sequentialMisses * 2;
						skipAmount = skipAmount.Smallest(maxSkip);
						index += skipAmount;
					}

					continue;
				}
				else
				{
					if (sequentialMisses >= 0)
					{
						XConsole.WriteLine("Backing up... ");

						var reverseIndex = 1;

						while (true)
						{
							var reverseSeekSubDirectory = directories[index - reverseIndex];

							if (!TryGetDateTime(
								reverseSeekSubDirectory,
								out var reverseSeekDateTime,
								out var reverseSeekJsonFileInfo))
							{
								reverseIndex++;
								continue;
							}

							var reverseSeekRecord = TenchiHistoricalStatisticsFile.FromJsonHighEfficiency(
								jsonFileInfo.FullName,
								userInfo.TetrioUserID);

							if (reverseSeekRecord == null)
							{
								if (!TryGetDateTime(
									directories[index - reverseIndex + 1],
									out var reverseSeekDateTime2,
									out var reverseSeekJsonFileInfo2))
								{
									reverseIndex++;
									continue;
								}
								XConsole.WriteLine("found file ");
								return reverseSeekJsonFileInfo2;
							}
							reverseIndex++;
						}
					}
					else
					{
						XConsole.WriteLine("found file ");
						return jsonFileInfo;
					}
				}
				sequentialMisses = 0;
			}
			return null;
		}


		public override void Execute(string args)
		{
			XConsole.Write(" Enter a username/id to fetch: ", Color.MediumTurquoise);

			var username = XConsole.ReadLine();
			username = username.ToLower();
			var userInfo = _client.FetchUser(username);

			var userData = _client.FetchUserData(username);
			var userJoinDate = userData.Content.TimeStamp - TimeSpan.FromHours(6.5);

			XConsole.WriteLine($" Processing data for {userInfo.Username.Quote()}... ", Color.MediumTurquoise);

			TenchiHistoricalStatisticsFile lastUserStatisticsEntry = null;
			TenchiUserStatistics lastUserStatistics = null;

			var colorIndex = -1;

			Color nextColor()
			{
				colorIndex++;
				return _csvColors[colorIndex];
			}


			var stopwatch = new Stopwatch();
			stopwatch.Start();

			var singleFileStopwatch = new Stopwatch();
			singleFileStopwatch.Start();

			var isDoneSeeking = false;
			var hasEncounteredRecord = false;

			var directories = _tetrioStatsDirectory.GetDirectories();
			var startIndex = 0;
			var index = 0;
			var foundRecords = 0;

			var results = new Dictionary<DateTime, TenchiUserStatistics>();

			//var startFile = EfficientSeekFirstFile(userInfo, userJoinDate);

			foreach (var subDirectory in directories)
			{
				Console.SetCursorPosition(0, Console.CursorTop);

				XConsole.Write($"Current: {subDirectory.Name} | {singleFileStopwatch.ElapsedMilliseconds} ms.", Swatch.Cyan);
				singleFileStopwatch.Restart();

				index++;

				if (!TryGetDateTime(subDirectory, out var dateTime, out var jsonFileInfo))
				{
					continue;
				}

				if (dateTime < userJoinDate)
				{
					continue;
				}

				//if (dateTime < startFile.LastWriteTimeUtc - TimeSpan.FromHours(3.1))
				//{
				//	continue;
				//}

				if (!isDoneSeeking)
				{
					isDoneSeeking = true;
					XConsole.WriteLine($" Begin Start Seek Time: {stopwatch.Elapsed:c}", Color.Yellow);
				}

				//using var fileReader = jsonFileInfo.OpenText();

				//var fileContent = fileReader.ReadToEnd();

				//var statsFile = TenchiHistoricalStatisticsFile.FromJson(fileContent);
				var record = TenchiHistoricalStatisticsFile.FromJsonHighEfficiency(
					jsonFileInfo.FullName,
					userInfo.TetrioUserID);

				if (record == null)// || !statsFile.Success)
				{
					continue;
				}

				if (!hasEncounteredRecord)
				{
					startIndex = index;
					hasEncounteredRecord = true;

					XConsole.WriteLine($" Begin Read Record Time: {stopwatch.Elapsed:c}", Color.Yellow);
				}

				var percentage = ((double)index - startIndex + 1) / ((double)directories.Length - startIndex) * 100d;
				//percentage =((double)index / directories.Length)* 100;
				//Console.SetCursorPosition(0, Console.CursorTop - 1);

				XConsole.Write($" | {percentage:F3} %", Swatch.Pink)
					.Write($" | ", Color.Azure)
					.Write($"{(index - startIndex + 1)} / {(directories.Length - startIndex)}", Swatch.Cyan)
					.Write($" | ", Color.Azure)
					.Write($"{foundRecords} Unique   ", Swatch.Teal);

				//var records = statsFile;//.UserStats;

				//var record = records.FirstOrDefault(
				//	t => t.UserID.ToLower() == userInfo.TetrioUserID.ToLower());


				//if (record == null)
				//	continue;


				colorIndex = -1;

				if (lastUserStatistics == null
					|| record.League.GamesPlayed != lastUserStatistics.League.GamesPlayed)
				{
					results.Add(jsonFileInfo.LastWriteTimeUtc, record);
					foundRecords++;
				}
				lastUserStatistics = record;
			}

			XConsole
				.WriteLine()
				.WriteLine();

			XConsole
				.Write($"Datetime, ", nextColor())
				//.Write($"Username, ", nextColor())
				//.Write($"XP, ", nextColor())
				.Write($"GP, ", nextColor())
				.Write($"GW, ", nextColor())
				.Write($"TR, ", nextColor())
				.Write($"Glicko, ", nextColor())
				.Write($"RD, ", nextColor())
				.Write($"Rank, ", nextColor())
				.Write($"APM, ", nextColor())
				.Write($"PPS, ", nextColor())
				.Write($"VS", nextColor())
				.WriteLine();

			colorIndex = -1;

			foreach (var record in results)
			{
				var tl = record.Value.League;

				XConsole
					.Write($"{record.Key:u}, ", nextColor())
					//.Write($"{record.Value.Username}, ", nextColor())
					//.Write($"{record.Value.XP ?? -1}, ", nextColor())
					.Write($"{tl.GamesPlayed ?? -1}, ", nextColor())
					.Write($"{tl.GamesWon ?? -1}, ", nextColor())
					.Write($"{tl.TLRating ?? -1}, ", nextColor())
					.Write($"{tl.GlickoRating ?? -1}, ", nextColor())
					.Write($"{tl.GlickoRatingDeviation ?? -1}, ", nextColor())
					.Write($"{tl.UserRank ?? "z"}, ", nextColor())
					.Write($"{tl.AverageRollingAPM ?? -1}, ", nextColor())
					.Write($"{tl.AverageRollingPPS ?? -1}, ", nextColor())
					.Write($"{tl.AverageRollingVSScore ?? -1}", nextColor())
					.WriteLine();

				colorIndex = -1;
			}

			XConsole
				.WriteLine()
				.WriteLine();

			XConsole.WriteLine(" Done.", Color.MediumSpringGreen);

			XConsole.WriteLine(" -----------------------", Color.MediumSpringGreen);

			stopwatch.Stop();

			XConsole.WriteLine($" Process Duration: {stopwatch.Elapsed:c}", Color.Yellow);
			//XConsole.WriteLine("Overall:.", Color.MediumSpringGreen)
			//	.WriteLine();

			//foreach (var userRecord in userDataInfo.OrderBy(t => t.Key))
			//{
			//	var record = userRecord.Value;
			//	var tl = userRecord.Value.League;

			//	XConsole
			//		.Write($"{userRecord.Key}, ")
			//		.Write($"{record.Role}, ")
			//		.Write($"{record.XP}, ")
			//		.Write($"{tl.GamesPlayed}, ")
			//		.Write($"{tl.GamesWon}, ")
			//		.Write($"{tl.TetrisRating}, ")
			//		.Write($"{tl.GlickoRating}, ")
			//		.Write($"{tl.GlickoRatingDeviation}, ")
			//		.Write($"{tl.UserRank}, ")
			//		.Write($"{tl.AverageRollingAPM}, ")
			//		.Write($"{tl.AverageRollingPPS}, ")
			//		.Write($"{tl.AverageRollingVSScore}, ")
			//		.WriteLine()
			//		.WriteLine();
			//}
		}


		private static bool TryGetDateTime(
			DirectoryInfo directory,
			out DateTime? dateTime,
			out FileInfo jsonFileInfo)
		{
			if (!_dateTimeRegex.IsMatch(directory.Name))
			{
				XConsole.WriteLine($" Cannot Parse DateTime: \"{directory.Name}\"", Color.DeepPink);

				dateTime = null;
				jsonFileInfo = null;

				return false;
			}

			var match = _dateTimeRegex.Match(directory.Name);

			var year = int.Parse(match.Groups["year"].Value);
			var month = int.Parse(match.Groups["month"].Value);
			var day = int.Parse(match.Groups["day"].Value);
			var hour = int.Parse(match.Groups["hour"].Value);
			var min = int.Parse(match.Groups["min"].Value);
			var sec = int.Parse(match.Groups["sec"].Value);

			dateTime = new DateTime(year, month, day, hour, min, sec);

			var jsonFile = new FileInfo($@"{directory.FullName}\league.json");

			if (!jsonFile.Exists)
			{
				//XConsole.WriteLine($" Cannot find path: \"{jsonFile.FullName}\"", Color.DeepPink);

				dateTime = null;
				jsonFileInfo = null;

				return false;
			}

			jsonFileInfo = jsonFile;
			return true;
		}


		public override void Dispose()
		{
			_client.Dispose();
			base.Dispose();
		}
	}
}
