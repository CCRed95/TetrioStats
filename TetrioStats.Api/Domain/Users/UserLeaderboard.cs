using System.Collections.Generic;
using Newtonsoft.Json;
using TetrioStats.Api.Infrastructure;


// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Domain.Users;

/// <summary>
/// Represents a user's league statistics in the leaderboard.
/// </summary>
public class LeaderboardUserLeague
{
	/// <summary>
	/// The amount of TETRA LEAGUE games played by this user.
	/// </summary>
	[JsonProperty("gamesplayed")]
	public long GamesPlayed { get; set; }

	/// <summary>
	/// The amount of TETRA LEAGUE games won by this user.
	/// </summary>
	[JsonProperty("gameswon")]
	public long GamesWon { get; set; }

	/// <summary>
	/// This user's TR (Tetra Rating).
	/// </summary>
	[JsonProperty("tr")]
	public float TetraRating { get; set; }

	/// <summary>
	/// This user's GLIXARE.
	/// </summary>
	[JsonProperty("gxe")]
	public float Glixare { get; set; }

	/// <summary>
	/// This user's letter rank.
	/// </summary>
	[JsonProperty("rank")]
	public UserRank? Rank { get; set; }

	/// <summary>
	/// This user's highest achieved rank this season.
	/// </summary>
	[JsonProperty("bestrank")]
	public UserRank? BestRank { get; set; }

	/// <summary>
	/// This user's Glicko-2 rating.
	/// </summary>
	[JsonProperty("glicko")]
	public float GlickoRating { get; set; }

	/// <summary>
	/// This user's Glicko-2 Rating Deviation.
	/// </summary>
	[JsonProperty("rd")]
	public float RatingDeviation { get; set; }

	/// <summary>
	/// This user's average APM (attack per minute) over the last 10 games.
	/// </summary>
	[JsonProperty("apm")]
	public float AverageAPM { get; set; }

	/// <summary>
	/// This user's average PPS (pieces per second) over the last 10 games.
	/// </summary>
	[JsonProperty("pps")]
	public float AveragePPS { get; set; }

	/// <summary>
	/// This user's average VS (versus score) over the last 10 games.
	/// </summary>
	[JsonProperty("vs")]
	public float AverageVS { get; set; }

	/// <summary>
	/// Indicates whether this user's RD is rising (has not played in the last week).
	/// </summary>
	[JsonProperty("decaying")]
	public bool IsDecaying { get; set; }
}


///// <summary>
///// Represents a user entry in the leaderboard.
///// </summary>
//public class LeaderboardUser
//{
//	/// <summary>
//	/// The unique ID of the user.
//	/// </summary>
//	[JsonProperty("_id")]
//	public string Id { get; set; }

//	/// <summary>
//	/// The username of the user.
//	/// </summary>
//	[JsonProperty("username")]
//	public string Username { get; set; }

//	/// <summary>
//	/// The role of the user.
//	/// </summary>
//	[JsonProperty("role")]
//	public UserRole Role { get; set; }

//	/// <summary>
//	/// The timestamp when the user was created.
//	/// </summary>
//	[JsonProperty("ts")]
//	public string? Timestamp { get; set; }

//	/// <summary>
//	/// The user's experience points.
//	/// </summary>
//	[JsonProperty("xp")]
//	public float XP { get; set; }

//	/// <summary>
//	/// The user's ISO 3166-1 country code, or null if hidden or unknown.
//	/// </summary>
//	[JsonProperty("country")]
//	public string? Country { get; set; }

//	/// <summary>
//	/// Indicates whether the user is a TETR.IO supporter.
//	/// </summary>
//	[JsonProperty("supporter")]
//	public bool? IsSupporter { get; set; }

//	/// <summary>
//	/// The user's league statistics.
//	/// </summary>
//	[JsonProperty("league")]
//	public LeaderboardUserLeague League { get; set; }

//	/// <summary>
//	/// The total number of games played by the user.
//	/// </summary>
//	[JsonProperty("gamesplayed")]
//	public long GamesPlayed { get; set; }

//	/// <summary>
//	/// The total number of games won by the user.
//	/// </summary>
//	[JsonProperty("gameswon")]
//	public long GamesWon { get; set; }

//	/// <summary>
//	/// The total amount of time the user has played, in seconds.
//	/// </summary>
//	[JsonProperty("gametime")]
//	public float GameTime { get; set; }

//	/// <summary>
//	/// The number of friends the user has.
//	/// </summary>
//	[JsonProperty("friend_count")]
//	public long? FriendCount { get; set; }

//	/// <summary>
//	/// The user's achievement rank.
//	/// </summary>
//	[JsonProperty("ar")]
//	public long AchievementRank { get; set; }

//	/// <summary>
//	/// The user's achievement rank counts.
//	/// </summary>
//	[JsonProperty("ar_counts")]
//	public UserArCounts AchievementRankCounts { get; set; }

//	/// <summary>
//	/// The Prisecter associated with the user.
//	/// </summary>
//	[JsonProperty("p")]
//	public Prisecter Prisecter { get; set; }
//}


/// <summary>
/// Represents the leaderboard object containing user entries.
/// </summary>
public class LeaderboardResponseData
{
	/// <summary>
	/// The list of leaderboard user entries.
	/// </summary>
	[JsonProperty("entries")]
	public List<UserHistoricalLeaderboardEntry> Entries { get; set; }
}


/// <summary>
/// Represents a packet containing leaderboard data.
/// </summary>
public class LeaderboardResponse : TetrioApiResponse<LeaderboardResponseData>;