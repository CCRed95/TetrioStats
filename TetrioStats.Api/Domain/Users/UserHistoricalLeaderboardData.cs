using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using TetrioStats.Api.Infrastructure;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Domain.Users;

/// <summary>
/// Represents a user's leaderboard entry for a season.
/// </summary>
public class UserHistoricalLeaderboardEntry
{
	/// <summary>
	/// The user's internal ID.
	/// </summary>
	[JsonProperty("_id")]
	public string Id { get; set; }

	/// <summary>
	/// The season ID.
	/// </summary>
	[JsonProperty("season")]
	[CanBeNull]
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
	public string CountryCode { get; set; }

	/// <summary>
	/// This user's final position in the season's global leaderboards.
	/// </summary>
	[JsonProperty("placement")]
	public int Placement { get; set; }

	/// <summary>
	/// Indicates whether the user was ranked at the time of the season's end.
	/// </summary>
	[JsonProperty("ranked")]
	public bool IsRanked { get; set; }

	/// <summary>
	/// The amount of TETRA LEAGUE games played by this user.
	/// </summary>
	[JsonProperty("gamesplayed")]
	public int GamesPlayed { get; set; }

	/// <summary>
	/// The amount of TETRA LEAGUE games won by this user.
	/// </summary>
	[JsonProperty("gameswon")]
	public int GamesWon { get; set; }

	/// <summary>
	/// This user's final Glicko-2 rating.
	/// </summary>
	[JsonProperty("glicko")]
	public double Glicko { get; set; }

	/// <summary>
	/// This user's final Glicko-2 Rating Deviation.
	/// </summary>
	[JsonProperty("rd")]
	public double RatingDeviation { get; set; }

	/// <summary>
	/// This user's final TR (Tetra Rating).
	/// </summary>
	[JsonProperty("tr")]
	public double TetraRating { get; set; }

	/// <summary>
	/// This user's final GLIXARE score (a % chance of beating an average player).
	/// </summary>
	[JsonProperty("gxe")]
	public double Glixare { get; set; }

	/// <summary>
	/// This user's final letter rank.
	/// </summary>
	[JsonProperty("rank")]
	public UserRank Rank { get; set; }

	/// <summary>
	/// This user's highest achieved rank in the season.
	/// </summary>
	[JsonProperty("bestrank")]
	public UserRank? BestRank { get; set; }

	/// <summary>
	/// This user's average APM (attack per minute) over the last 10 games in the season.
	/// </summary>
	[JsonProperty("apm")]
	public double AverageAPM { get; set; }

	/// <summary>
	/// This user's average PPS (pieces per second) over the last 10 games in the season.
	/// </summary>
	[JsonProperty("pps")]
	public double AveragePPS { get; set; }

	/// <summary>
	/// This user's average VS (versus score) over the last 10 games in the season.
	/// </summary>
	[JsonProperty("vs")]
	public double AverageVS { get; set; }

	/// <summary>
	/// The Prisecter of this entry.
	/// </summary>
	[JsonProperty("p")]
	public Prisecter Prisecter { get; set; }
}


/// <summary>
/// Represents the leaderboard history for a user.
/// </summary>
public class UserHistoricalLeaderboardData
{
	/// <summary>
	/// The list of leaderboard entries for the user.
	/// </summary>
	[JsonProperty("entries")]
	public List<UserHistoricalLeaderboardEntry> Entries { get; set; }
}


/// <summary>
/// Represents a packet containing historical leaderboard data.
/// </summary>
public class UserHistoricalLeaderboardResponse : TetrioApiResponse<UserHistoricalLeaderboardData>;