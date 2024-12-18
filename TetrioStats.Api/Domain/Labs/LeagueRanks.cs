using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TetrioStats.Api.Infrastructure;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Domain.Labs;

/// <summary>
/// Represents the rank in the league leaderboard.
/// </summary>
public class LeagueRank
{
	/// <summary>
	/// The leaderboard position required to attain this rank.
	/// </summary>
	[JsonProperty("pos")]
	public int Position { get; set; }

	/// <summary>
	/// The percentile (0~1) this rank is for.
	/// </summary>
	[JsonProperty("percentile")]
	public double Percentile { get; set; }

	/// <summary>
	/// The TR required to obtain a leaderboard position that will award this rank.
	/// </summary>
	[JsonProperty("tr")]
	public double TR { get; set; }

	/// <summary>
	/// The TR this rank will gravitate toward (using de- and inflation zones).
	/// </summary>
	[JsonProperty("targettr")]
	public double TargetTR{ get; set; }

	/// <summary>
	/// The average APM across all players in this rank.
	/// </summary>
	[JsonProperty("apm")]
	public double? APM { get; set; }

	/// <summary>
	/// The average PPS across all players in this rank.
	/// </summary>
	[JsonProperty("pps")]
	public double? PPS { get; set; }

	/// <summary>
	/// The average Versus Score across all players in this rank.
	/// </summary>
	[JsonProperty("vs")]
	public double? VersusScore { get; set; }

	/// <summary>
	/// The amount of players with this rank.
	/// </summary>
	[JsonProperty("count")]
	public int TotalPlayersRanked { get; set; }
}


/// <summary>
/// Represents the data for league ranks.
/// </summary>
public class LeagueRanksData
{
	/// <summary>
	/// The total number of players in the league.
	/// </summary>
	[JsonProperty("total")]
	public int TotalPlayersRanked { get; set; }
	
	[JsonProperty("x+")]
	public LeagueRank XPlus { get; set; }

	[JsonProperty("x")]
	public LeagueRank X { get; set; }

	[JsonProperty("u")]
	public LeagueRank U { get; set; }

	[JsonProperty("ss")]
	public LeagueRank SS { get; set; }

	[JsonProperty("s+")]
	public LeagueRank SPlus { get; set; }

	[JsonProperty("s")]
	public LeagueRank S { get; set; }

	[JsonProperty("s-")]
	public LeagueRank SMinus { get; set; }

	[JsonProperty("a+")]
	public LeagueRank APlus { get; set; }

	[JsonProperty("a")]
	public LeagueRank A { get; set; }

	[JsonProperty("a-")]
	public LeagueRank AMinus { get; set; }

	[JsonProperty("b+")]
	public LeagueRank BPlus { get; set; }

	[JsonProperty("b")]
	public LeagueRank B { get; set; }

	[JsonProperty("b-")]
	public LeagueRank BMinus { get; set; }

	[JsonProperty("c+")]
	public LeagueRank CPlus { get; set; }

	[JsonProperty("c")]
	public LeagueRank C { get; set; }

	[JsonProperty("c-")]
	public LeagueRank CMinus { get; set; }

	[JsonProperty("d+")]
	public LeagueRank DPlus { get; set; }

	[JsonProperty("d")]
	public LeagueRank D { get; set; }

	[JsonIgnore]
	public IReadOnlyDictionary<UserRank, LeagueRank> UserRankMapping
	{
		get => new Dictionary<UserRank, LeagueRank>
		{
			[UserRank.D] = D,
			[UserRank.DPlus] = DPlus,
			[UserRank.CMinus] = CMinus,
			[UserRank.C] = C,
			[UserRank.CPlus] = CPlus,

			[UserRank.BMinus] = BMinus,
			[UserRank.B] = B,
			[UserRank.BPlus] = BPlus,
			[UserRank.AMinus] = AMinus,
			[UserRank.A] = A,

			[UserRank.APlus] = APlus,
			[UserRank.SMinus] = SMinus,
			[UserRank.S] = S,
			[UserRank.SPlus] = SPlus,
			[UserRank.SS] = SS,

			[UserRank.U] = U,
			[UserRank.X] = X,
			[UserRank.XPlus] = XPlus
		};
	}
}


/// <summary>
/// Represents the league ranks with additional metadata.
/// </summary>
public class LeagueRanks
{
	/// <summary>
	/// The internal ID of the Labs data point.
	/// </summary>
	[JsonProperty("_id")]
	public string Id { get; set; }

	/// <summary>
	/// The stream ID the Labs data point belongs to.
	/// </summary>
	[JsonProperty("s")]
	public string StreamId { get; set; }

	/// <summary>
	/// The time at which the data point was created.
	/// </summary>
	[JsonProperty("t")]
	public DateTime Timestamp { get; set; }

	/// <summary>
	/// The data point.
	/// </summary>
	[JsonProperty("data")]
	public LeagueRanksData Data { get; set; }
}


/// <summary>
/// Represents a packet containing league ranks data.
/// </summary>
public class LeagueRanksResponse : TetrioApiResponse<LeagueRanks>;