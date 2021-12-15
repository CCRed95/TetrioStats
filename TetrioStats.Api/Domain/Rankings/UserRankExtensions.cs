using System;
using System.Collections.Generic;
using System.Linq;
using Ccr.Std.Core.Collections;
using Ccr.Std.Core.Extensions;
using Ccr.Std.Core.Numerics.Ranges;

namespace TetrioStats.Api.Domain.Rankings
{
	public static class UserRankExtensions
	{
		private static readonly IReadOnlyDictionary<UserRank, double> _rankPercentileThreshold
			= new BijectiveIsomorphicMap<UserRank, double>
			{
				[UserRank.X] = 1.0,
				[UserRank.U] = 5.0,
				[UserRank.SS] = 11.0,
				[UserRank.SPlus] = 17.0,
				[UserRank.S] = 23.0,
				[UserRank.SMinus] = 30.0,
				[UserRank.APlus] = 38.0,
				[UserRank.A] = 46.0,
				[UserRank.AMinus] = 54.0,
				[UserRank.BPlus] = 62.0,
				[UserRank.B] = 70.0,
				[UserRank.BMinus] = 78.0,
				[UserRank.CPlus] = 84.0,
				[UserRank.C] = 90.0,
				[UserRank.CMinus] = 95.0,
				[UserRank.DPlus] = 97.5,
				[UserRank.D] = 100.0,
			};


		public static DoubleRange GetPercentileRange(
			this UserRank @this)
		{
			if (!_rankPercentileThreshold.TryGetValue(@this, out var maxPercentileInRank))
				throw new NotSupportedException(
					$"Cannot find range for UserRank {@this}.");

			if (@this == UserRank.X)
				return (0.0, maxPercentileInRank);


			var nextRankIntegralValue = (int)@this + 1;
			var nextRank = Enum.ToObject(typeof(UserRank), nextRankIntegralValue)
				.As<UserRank>();

			if (!_rankPercentileThreshold.TryGetValue(nextRank, out var minPercentileInRank))
				throw new NotSupportedException(
					$"Cannot find range for UserRank {nextRank}.");

			return (minPercentileInRank, maxPercentileInRank);
		}

		public static IReadOnlyDictionary<UserRank, DoubleRange> CalculateRankTRThresholds()
		{
			var globalLeaderboardRankings = TetrioApiClient.FetchGlobalLeaderboardRankings()
				.ToList();

			var results = new Dictionary<UserRank, DoubleRange>();

			var userRanks = Enum
				.GetValues(typeof(UserRank))
				.Cast<UserRank>()
				.SkipWhile(t => t == UserRank.Unranked)
				.ToArray();

			foreach (var userRank in userRanks)
			{
				var userRankRange = userRank.GetPercentileRange();

				var index = (int)Math.Round(userRankRange.Minimum / 100d * globalLeaderboardRankings.Count());
				var upperIndex = (int)Math.Round(userRankRange.Maximum / 100d * globalLeaderboardRankings.Count() - 1);

				var thresholdTRForRank = globalLeaderboardRankings[index].League.TLRating;
				var upperThresholdTRForRank = globalLeaderboardRankings[upperIndex].League.TLRating;

				results.Add(userRank, (thresholdTRForRank, upperThresholdTRForRank));
			}

			return results;
		}

		public static DoubleRange GetPercentileRange(
			this UserRankGrade @this)
		{
			var userRanks = ValueEnum.ToArray<UserRankGrade>().ToList();
			
			var minPercentileInRank = @this.RankPercentile;

			if (ReferenceEquals(@this, UserRankGrade.X))
				return (0.0, minPercentileInRank);

			var nextRankIndex = userRanks.IndexOf(@this) + 1;
			var nextRank = userRanks[nextRankIndex];
			var minPercentileInNextRank = nextRank.Value.Item3;

			return (minPercentileInNextRank, minPercentileInRank);
		}

		public static IReadOnlyDictionary<UserRankGrade, DoubleRange> CalculateRankGradeTRThresholds()
		{
			var globalLeaderboardRankings = TetrioApiClient
				.FetchGlobalLeaderboardRankings()
				.ToList();

			var results = new Dictionary<UserRankGrade, DoubleRange>();

			var userRanks = ValueEnum.ToArray<UserRankGrade>();

			foreach (var userRank in userRanks)
			{
				var userRankRange = userRank.GetPercentileRange();

				var minimumIndex = (int)Math.Round(
					userRankRange.Minimum / 100d * globalLeaderboardRankings.Count);

				var upperIndex = (int)Math.Round(
					userRankRange.Maximum / 100d * globalLeaderboardRankings.Count - 1);

				var thresholdTRForRank = globalLeaderboardRankings[minimumIndex].League.TLRating;
				var upperThresholdTRForRank = globalLeaderboardRankings[upperIndex].League.TLRating;

				results.Add(userRank, (thresholdTRForRank, upperThresholdTRForRank));
			}

			return results;
		}
	}
}