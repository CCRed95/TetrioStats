using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using TetrioStats.Api.Domain.Converters;
using TetrioStats.Api.Infrastructure;


// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Domain.Users.Summaries;

/// <summary>
/// Represents the league summary of a user's performance during a specific season.
/// </summary>
public class LeagueSummaryPast
{
	/// <summary>
	/// The season ID.
	/// </summary>
	[JsonProperty("season")]
	public string Season { get; set; }

	/// <summary>
	/// The username the user had at the time.
	/// </summary>
	[JsonProperty("username")]
	public string Username { get; set; }

	/// <summary>
	/// The country the user represented at the time.
	/// </summary>
	[JsonProperty("country")]
    [CanBeNull]
    public string Country { get; set; }

	/// <summary>
	/// The user's final position in the season's global leaderboards.
	/// </summary>
	[JsonProperty("placement")]
	public long? Placement { get; set; }

	/// <summary>
	/// Indicates whether the user was ranked at the time of the season's end.
	/// </summary>
	[JsonProperty("ranked")]
	public bool Ranked { get; set; }

    /// <summary>
    /// The amount of TETRA LEAGUE games played by this user.
    /// </summary>
    [JsonProperty("gamesplayed")]
	public long? GamesPlayed { get; set; }

	/// <summary>
	/// The amount of TETRA LEAGUE games won by this user.
	/// </summary>
	[JsonProperty("gameswon")]
	public long? GamesWon { get; set; }

	/// <summary>
	/// The user's final Glicko-2 rating.
	/// </summary>
	[JsonProperty("glicko")]
	public float? Glicko { get; set; }

	/// <summary>
	/// The user's final Glicko-2 Rating Deviation.
	/// </summary>
	[JsonProperty("rd")]
	public float? RatingDeviation { get; set; }

	/// <summary>
	/// The user's final TR (Tetra Rating).
	/// </summary>
	[JsonProperty("tr")]
	public float? TR { get; set; }

	/// <summary>
	/// The user's final GLIXARE score (a % chance of beating an average player).
	/// </summary>
	[JsonProperty("gxe")]
	public float? GLIXARE { get; set; }

	/// <summary>
	/// The user's final letter rank. "z" is unranked.
	/// </summary>
	[JsonProperty("rank")]
	public string Rank { get; set; }

	/// <summary>
	/// The user's highest achieved rank in the season.
	/// </summary>
	[JsonProperty("bestrank")]
	public string? BestRank { get; set; }

	/// <summary>
	/// The user's average APM (attack per minute) over the last 10 games in the season.
	/// </summary>
	[JsonProperty("apm")]
	public float APM { get; set; }

	/// <summary>
	/// The user's average PPS (pieces per second) over the last 10 games in the season.
	/// </summary>
	[JsonProperty("pps")]
	public float PPS { get; set; }

	/// <summary>
	/// The user's average VS (versus score) over the last 10 games in the season.
	/// </summary>
	[JsonProperty("vs")]
	public float VS { get; set; }
}

/// <summary>
/// Represents the league summary for a user.
/// </summary>
public class LeagueSummary
{
	/// <summary>
    /// The amount of seconds this user spent playing, both on and offline. If the user has chosen
    /// to hide this statistic, it will be -1.
    /// </summary>
    [JsonProperty("gametime")]
    [JsonConverter(typeof(UnixTimeStampConverter))]
    public TimeSpan GamePlayTime { get; set; }

    /// <summary>
    /// The amount of TETRA LEAGUE games played by this user.
    /// </summary>
    [JsonProperty("gamesplayed")]
	public long? GamesPlayed { get; set; }

	/// <summary>
	/// The amount of TETRA LEAGUE games won by this user.
	/// </summary>
	[JsonProperty("gameswon")]
	public long? GamesWon { get; set; }

	/// <summary>
	/// The user's Glicko-2 rating, or -1 if less than 10 games were played.
	/// </summary>
	[JsonProperty("glicko")]
	public float? Glicko { get; set; }

	/// <summary>
	/// The user's Glicko-2 Rating Deviation, or -1 if less than 10 games were played.
	/// If over 100, this user is unranked.
	/// </summary>
	[JsonProperty("rd")]
	public float? RatingDeviation { get; set; }

	/// <summary>
	/// Indicates whether the user's RD is rising (has not played in the last week).
	/// </summary>
	[JsonProperty("decaying")]
	public bool? Decaying { get; set; }

	/// <summary>
	/// The user's TR (Tetra Rating), or -1 if less than 10 games were played.
	/// </summary>
	[JsonProperty("tr")]
	public float? TR { get; set; }

	/// <summary>
	/// The user's GLIXARE score (a % chance of beating an average player),
	/// or -1 if less than 10 games were played.
	/// </summary>
	[JsonProperty("gxe")]
	public float? GLIXARE { get; set; }

	/// <summary>
	/// The user's letter rank. "z" is unranked.
	/// </summary>
	[JsonProperty("rank")]
	public UserRank? Rank { get; set; }

	/// <summary>
	/// The user's highest achieved rank this season.
	/// </summary>
	[JsonProperty("bestrank")]
	public UserRank? BestRank { get; set; }

	/// <summary>
	/// The user's average APM (attack per minute) over the last 10 games.
	/// </summary>
	[JsonProperty("apm")]
	public float? APM { get; set; }

	/// <summary>
	/// The user's average PPS (pieces per second) over the last 10 games.
	/// </summary>
	[JsonProperty("pps")]
	public float? PPS { get; set; }

	/// <summary>
	/// The user's average VS (versus score) over the last 10 games.
	/// </summary>
	[JsonProperty("vs")]
	public float? VS { get; set; }

	/// <summary>
	/// The user's position in global leaderboards, or -1 if not applicable.
	/// </summary>
	[JsonProperty("standing")]
	public long? Standing { get; set; }

	/// <summary>
	/// The user's position in local leaderboards, or -1 if not applicable.
	/// </summary>
	[JsonProperty("standing_local")]
	public long? LocalStanding { get; set; }

	/// <summary>
	/// The user's percentile position (0 is best, 1 is worst).
	/// </summary>
	[JsonProperty("percentile")]
	public float? Percentile { get; set; }

	/// <summary>
	/// The user's percentile rank, or "z" if not applicable.
	/// </summary>
	[JsonProperty("percentile_rank")]
	public UserRank? PercentileRank { get; set; }

	/// <summary>
	/// The next rank the user can achieve by winning more games, or null if unranked
	/// (or already at the best rank).
	/// </summary>
	[JsonProperty("next_rank")]
	public UserRank? NextRank { get; set; }

	/// <summary>
	/// The previous rank the user can achieve by losing more games, or null if unranked
	/// (or already at the worst rank).
	/// </summary>
	[JsonProperty("prev_rank")]
	public UserRank? PreviousRank { get; set; }

	/// <summary>
	/// The position of the best player in the user's current rank. Surpassing this position
	/// advances the user to the next rank. -1 if unranked (or already at the best rank).
	/// </summary>
	[JsonProperty("next_at")]
	public long? NextAt { get; set; }

	/// <summary>
	/// The position of the worst player in the user's current rank. Dropping below this position
	/// demotes the user to the previous rank. -1 if unranked (or already at the worst rank).
	/// </summary>
	[JsonProperty("prev_at")]
	public long? PreviousAt { get; set; }

	/// <summary>
	/// The past league summaries by season.
	/// </summary>
	[JsonProperty("past")]
	[CanBeNull]
	public Dictionary<string, LeagueSummaryPast> Past { get; set; }
}


/// <summary>
/// Represents a packet containing League summary data.
/// </summary>
public class LeagueSummaryResponse : TetrioApiResponse<LeagueSummary>;