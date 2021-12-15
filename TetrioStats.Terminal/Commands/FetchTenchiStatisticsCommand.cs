using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Ccr.Terminal.Application;
using Ccr.Terminal.Extensions.Fluent.Json;
using TetrioStats.Api;
using TetrioStats.Api.Domain.Json.TenchiStats;
using TetrioStats.Terminal.Extensions;
using static Ccr.Terminal.ExtendedConsole;

namespace TetrioStats.Terminal.Commands
{
	public class FetchTenchiStatisticsCommand
		: TerminalCommand<string>
	{
		private static readonly DirectoryInfo _tetrioStatsDirectory = new DirectoryInfo(
			@"C:\Tetris\Data\Tenchi\tetrio-stats\");

		private static readonly TetrioApiClient _client = new TetrioApiClient();
		private static readonly Regex _dateTimeRegex = new Regex(
			@"^(?<year>[\d]{4})-(?<month>[\d]{2})-(?<day>[\d]{2})T(?<hour>[\d]{2})(?<min>[\d]{2})(?<sec>[\d]{2})$");


		public override string CommandName => "tenchi-stats";

		public override string ShortCommandName => "ts";


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
				XConsole.WriteLine($" Cannot find path: \"{jsonFile.FullName}\"", Color.DeepPink);

				dateTime = null;
				jsonFileInfo = null;

				return false;
			}

			jsonFileInfo = jsonFile;

			return true;
		}


		public override void Execute(string args)
		{
			XConsole.Write(" Enter a username to fetch: ", Color.MediumTurquoise);

			var username = XConsole.ReadLine();
			username = username.ToLower();
			var userInfo = _client.FetchUser(username);

			var verbose = XConsole.PromptYesNo(" Verbose JSON output?");


			var userDataInfo = new Dictionary<DateTime, TenchiUserStatistics>();

			var userData = _client.FetchUserData(username);
			var userJoinDate = userData.Content.TimeStamp - TimeSpan.FromDays(2);

			//var directories = _tetrioStatsDirectory.GetDirectories();

			foreach (var subDirectory in _tetrioStatsDirectory.GetDirectories())
			{
				if (!_dateTimeRegex.IsMatch(subDirectory.Name))
				{
					XConsole.WriteLine(" Cannot Parse DateTime: \"" + subDirectory.Name + "\"", Color.DeepPink);
					continue;
				}

				var match = _dateTimeRegex.Match(subDirectory.Name);

				var year = int.Parse(match.Groups["year"].Value);
				var month = int.Parse(match.Groups["month"].Value);
				var day = int.Parse(match.Groups["day"].Value);
				var hour = int.Parse(match.Groups["hour"].Value);
				var min = int.Parse(match.Groups["min"].Value);
				var sec = int.Parse(match.Groups["sec"].Value);

				var dateTime = new DateTime(year, month, day, hour, min, sec);

				if (dateTime < userJoinDate)
				{
					continue;
				}

				var jsonFile = subDirectory.FullName + @"\league.json";
				var jsonFileInfo = new FileInfo(jsonFile);

				if (!jsonFileInfo.Exists)
				{
					XConsole.WriteLine(" Cannot find path: \"" + jsonFileInfo.FullName + "\"", Color.DeepPink);
					continue;
				}

				using var fileReader = jsonFileInfo.OpenText();

				var fileContent = fileReader.ReadToEnd();

				var statsFile = TenchiHistoricalStatisticsFile.FromJson(fileContent);

				if (!statsFile.Success)
				{
					XConsole.WriteLine(" statsFile.Success = false!", Color.DeepPink);
					continue;
				}

				var record = statsFile.UserStats.FirstOrDefault(
					t => t.UserID == userInfo.TetrioUserID);

				if (record != null)
				{
					userDataInfo.Add(dateTime, record);

					if (verbose)
					{
						var tl = record.League;

						XConsole
							.Outdent()
							.BeginJsonSession()
							.WriteCode("{", JsonCodeKind.Brace);

						XConsole.BeginJsonSession()
							.WriteProperty(dateTime, t => t, t => t.ToString("u"), "DateTime")
							.WriteProperty(record, t => t.Role)
							.WriteProperty(record, t => t.XP)
							//.WriteProperty(stats, t => t.TimeStamp)
							.WriteCode("  ", JsonCodeKind.Brace)
							.WriteCode("TetraLeagueStats", JsonCodeKind.Attribute)
							.WriteCode(": {", JsonCodeKind.Brace);

						XConsole
							.Indent()
							.BeginJsonSession()
							.WriteProperty(tl, t => t.GamesPlayed)
							.WriteProperty(tl, t => t.GamesWon)
							.WriteProperty(tl, t => t.TLRating) //, propertyName: "TR")
							.WriteProperty(tl, t => t.GlickoRating) //, propertyName: "Glicko")
							.WriteProperty(tl, t => t.GlickoRatingDeviation) //, propertyName: "RD")
							.WriteProperty(tl, t => t.UserRank) //, propertyName: "Rank")
							.WriteProperty(tl, t => t.AverageRollingAPM) //, propertyName: "APM")
							.WriteProperty(tl, t => t.AverageRollingPPS) //, propertyName: "PPS")
							.WriteProperty(tl, t => t.AverageRollingVSScore); //, propertyName: "VS");

						XConsole
							.Outdent()
							.BeginJsonSession()
							.WriteCode("  }\n}", JsonCodeKind.Brace);

						XConsole
							.WriteLine();
					}
				}
				else
				{
					XConsole.WriteLine($" No record for user \"{username}\" in {dateTime:u}", Color.MediumPurple);
				}
			}

			XConsole.WriteLine("Done.", Color.MediumSpringGreen);

			XConsole.WriteLine("-----------------------", Color.MediumSpringGreen);

			XConsole.WriteLine("Overall:", Color.MediumSpringGreen)
				.WriteLine();

			foreach (var userRecord in userDataInfo.OrderBy(t => t.Key))
			{
				var record = userRecord.Value;
				var tl = userRecord.Value.League;

				XConsole
					.Write($"{userRecord.Key}, ")
					.Write($"{record.Role}, ")
					.Write($"{record.XP}, ")
					.Write($"{tl.GamesPlayed}, ")
					.Write($"{tl.GamesWon}, ")
					.Write($"{tl.TLRating}, ")
					.Write($"{tl.GlickoRating}, ")
					.Write($"{tl.GlickoRatingDeviation}, ")
					.Write($"{tl.UserRank}, ")
					.Write($"{tl.AverageRollingAPM}, ")
					.Write($"{tl.AverageRollingPPS}, ")
					.Write($"{tl.AverageRollingVSScore}, ")
					.WriteLine()
					.WriteLine();
			}
		}

		public override void Dispose()
		{
			_client.Dispose();
			base.Dispose();
		}
	}
}