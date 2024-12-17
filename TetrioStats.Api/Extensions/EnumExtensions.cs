using Ccr.Std.Core.Collections;
using Ccr.Std.Core.Numerics.Ranges;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Ccr.Std.Core.Extensions;
using TetrioStats.Api.Infrastructure;

namespace TetrioStats.Api.Extensions;

public static class UserRankExtensions
{
    private static readonly IReadOnlyDictionary<UserRank, double> _rankPercentileThreshold
        = new BijectiveIsomorphicMap<UserRank, double>
        {
            [UserRank.XPlus] = 0.2,
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
        var client = new TetrioApiClient();

        var leagueRankings = client.Labs.FetchLeagueRanks().Result;

        var results = new Dictionary<UserRank, DoubleRange>();

        var userRanks = Enum
            .GetValues(typeof(UserRank))
            .Cast<UserRank>()
            .SkipWhile(t => t == UserRank.Z)
            .ToArray();

        foreach (var userRank in userRanks)
        {
            var userRankRange = userRank.GetPercentileRange();

            var index = (int)Math.Round(userRankRange.Minimum / 100d * leagueRankings.Data.Data.Ranks.Count);
            var upperIndex = (int)Math.Round(userRankRange.Maximum / 100d * leagueRankings.Data.Data.Ranks.Count - 1);

            var thresholdTRForRank = leagueRankings.Data.Data.Ranks.ElementAt(index).Value.TR;
            var upperThresholdTRForRank = leagueRankings.Data.Data.Ranks.ElementAt(upperIndex).Value.TR;

            results.Add(userRank, (thresholdTRForRank, upperThresholdTRForRank));
        }

        return results;
    }

    //public static DoubleRange GetPercentileRange(
    //    this UserRank @this)
    //{
    //    var userRanks = ValueEnum.ToArray<UserRank>().ToList();

    //    var minPercentileInRank = @this.RankPercentile;

    //    if (ReferenceEquals(@this, UserRank.XPlus))
    //        return (0.0, minPercentileInRank);

    //    var nextRankIndex = userRanks.IndexOf(@this) + 1;
    //    var nextRank = userRanks[nextRankIndex];
    //    var minPercentileInNextRank = nextRank.Value.Item3;

    //    return (minPercentileInNextRank, minPercentileInRank);
    //}

    public static IReadOnlyDictionary<UserRank, DoubleRange> CalculateRankGradeTRThresholds()
    {
        var client = new TetrioApiClient();

        var leagueRankings = client.Labs.FetchLeagueRanks().Result;

        var results = new Dictionary<UserRank, DoubleRange>();

        var userRanks = _rankPercentileThreshold.Keys.ToArray();

        foreach (var userRank in userRanks)
        {
            var userRankRange = userRank.GetPercentileRange();

            var index = (int)Math.Round(userRankRange.Minimum / 100d * leagueRankings.Data.Data.Ranks.Count);
            var upperIndex = (int)Math.Round(userRankRange.Maximum / 100d * leagueRankings.Data.Data.Ranks.Count - 1);

            var thresholdTRForRank = leagueRankings.Data.Data.Ranks.ElementAt(index).Value.TR;
            var upperThresholdTRForRank = leagueRankings.Data.Data.Ranks.ElementAt(upperIndex).Value.TR;

            results.Add(userRank, (thresholdTRForRank, upperThresholdTRForRank));
        }

        return results;
    }
}

public static class EnumExtensions
{
	public static string GetDescription<TEnum>(
		this TEnum @this)
			where TEnum : Enum
	{
		var fieldInfo = @this.GetType().GetField(@this.ToString());

		if (fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false)
			    is DescriptionAttribute[] attributes && attributes.Any())
		{
			return attributes.First().Description;
		}

		return @this.ToString();
	}


    public static TEnum ParseFromDescription<TEnum>(string @this)
        where TEnum : Enum
    {
        var fieldInfos = @this.GetType().GetFields(BindingFlags.Public);
        
        foreach (var fieldInfo in fieldInfos)
        {
            if (fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    is DescriptionAttribute[] attributes && attributes.Any())
            {
                if (string.Equals(attributes.First().Description, @this, StringComparison.CurrentCultureIgnoreCase))
                {
                    var rawValue = fieldInfo.GetValue(null);
                    return (TEnum)rawValue;
                }
            }
        }

        throw new NotSupportedException();
    }
}