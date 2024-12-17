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
	public long Position { get; set; }

	/// <summary>
	/// The percentile (0~1) this rank is for.
	/// </summary>
	[JsonProperty("percentile")]
	public float Percentile { get; set; }

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
	public long Count { get; set; }
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
	public long Total { get; set; }

	/// <summary>
	/// A dictionary of ranks mapped by UserRank.
	/// </summary>
	[JsonProperty("ranks")]
	public Dictionary<UserRank, LeagueRank> Ranks { get; set; }
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
	public string Timestamp { get; set; }

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