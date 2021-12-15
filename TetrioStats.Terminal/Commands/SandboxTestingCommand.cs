using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Ccr.Std.Core.Extensions;
using Ccr.Terminal.Application;
using Ccr.Terminal.Extensions;
using TetrioStats.Api;
using TetrioStats.Api.Domain.Rankings;
using static Ccr.Terminal.ExtendedConsole;

namespace TetrioStats.Terminal.Commands
{
	public class SandboxTestingCommand
		: TerminalCommand<string>
	{
		private static readonly TetrioApiClient _client = new TetrioApiClient();


		public override string CommandName => "sandbox-testing";

		public override string ShortCommandName => "test";


		//private IReadOnlyDictionary<UserRankGrade, DoubleRange> _trRankingsThresholds;
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

		public override void Execute(string args)
		{
			XConsole.Write(" Running Sandbox Tests...", Color.MediumTurquoise);

			XConsole.Write("Fetching TR Ranking Thresholds... ", Color.MediumTurquoise);

			var _trRankingsThresholds = UserRankExtensions
				.CalculateRankGradeTRThresholds();

			var reversedTrRankingsThresholds = _trRankingsThresholds.Reverse()
				.ToArray();

			UserRankGrade lastThreshold = null;

			foreach (var (key, value) in reversedTrRankingsThresholds)
			{
				var currentGradeStr = key.ToString();
				var color = _rankToStringMap[currentGradeStr];

				var darkened = color.Darken(0.7);
				var lightened = color.Lighten(0.8);

				var rankPercentile = lastThreshold == null
					? key.RankPercentile
					: key.RankPercentile - lastThreshold.RankPercentile;

				XConsole
					.ScopedBold(c1 =>
					{
						c1.ScopedBackground(darkened,
							c2 =>
							{
								c2.Write($"     {key.ToString().PadRight(2)}    |    ", lightened)
									.Write($"{value.Maximum.Round().ToString().PadLeft(5)} TR    ", color.Lighten(0.97))
									.Write($"|    ({rankPercentile} % of player base)", lightened)
									.WriteLine("-".Repeat(Console.WindowWidth - Console.CursorLeft), darkened);
							});
					});

				lastThreshold = key;
			}
		}


		public override void Dispose()
		{
			_client.Dispose();
			base.Dispose();
		}
	}
}
