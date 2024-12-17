using System.Collections.Generic;
using Newtonsoft.Json;
using TetrioStats.Api.Infrastructure;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Domain.Users.Lists;

/// <summary>
/// This user's current TETRA LEAGUE standing.
/// </summary>
public class LeagueFullData
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
	/// This user's TR (Tetra Rating), or -1 if less than 10 games were played.
	/// </summary>
	[JsonProperty("rating")]
	public double Rating { get; set; }

	/// <summary>
	/// This user's letter rank. Z is unranked.
	/// </summary>
	[JsonProperty("rank")]
	public UserRank Rank { get; set; }

	/// <summary>
	/// This user's highest achieved rank this season.
	/// </summary>
	[JsonProperty("bestrank")]
	public UserRank? BestRank { get; set; }

	/// <summary>
	/// This user's Glicko-2 rating.
	/// </summary>
	[JsonProperty("glicko")]
	public double? Glicko { get; set; }

	/// <summary>
	/// This user's Glicko-2 Rating Deviation. If over 100, this user is unranked.
	/// </summary>
	[JsonProperty("rd")]
	public double? Rd { get; set; }

	/// <summary>
	/// This user's average APM (attack per minute) over the last 10 games.
	/// </summary>
	[JsonProperty("apm")]
	public double? APM { get; set; }

	/// <summary>
	/// This user's average PPS (pieces per second) over the last 10 games.
	/// </summary>
	[JsonProperty("pps")]
	public double? PPS { get; set; }

	/// <summary>
	/// This user's average VS (versus score) over the last 10 games.
	/// </summary>
	[JsonProperty("vs")]
	public double? VS { get; set; }

	/// <summary>
	/// Whether this user's RD is rising (has not played in the last week).
	/// </summary>
	[JsonProperty("decaying")]
	public bool Decaying { get; set; }
}


/// <summary>
/// The matched user's data.
/// </summary>
public class LeagueFullUser
{
	/// <summary>
	/// The user's internal ID.
	/// </summary>
	[JsonProperty("_id")]
	public string Id { get; set; }

	/// <summary>
	/// The user's username.
	/// </summary>
	[JsonProperty("username")]
	public string Username { get; set; }

	/// <summary>
	/// The user's role (one of "anon", "user", "bot", "halfmod", "mod", "admin", "sysop").
	/// </summary>
	[JsonProperty("role")]
	public UserRole Role { get; set; }

	/// <summary>
	/// The user's XP in points.
	/// </summary>
	[JsonProperty("xp")]
	public double XP { get; set; }

	/// <summary>
	/// The user's ISO 3166-1 country code, or null if hidden/unknown. Some vanity flags exist.
	/// </summary>
	[JsonProperty("country")]
	public string Country { get; set; }

	/// <summary>
	/// Whether this user is currently supporting TETR.IO
	/// </summary>
	[JsonProperty("supporter")]
	public bool? Supporter { get; set; }

	/// <summary>
	/// Whether this user is a verified account.
	/// </summary>
	[JsonProperty("verified")]
	public bool Verified { get; set; }

	/// <summary>
	/// This user's current TETRA LEAGUE standing.
	/// </summary>
	[JsonProperty("league")]
	public LeagueFullData League { get; set; }
}


/// <summary>
/// Represents the data for a packet containing a list of users and their TETRA LEAGUE standings.
/// </summary>
public class LeagueFullPacketData
{
	/// <summary>
	/// The matched users.
	/// </summary>
	[JsonProperty("users")]
	public List<LeagueFullUser> Users { get; set; }
}


/// <summary>
/// A packet containing a list of users and their TETRA LEAGUE standings.
/// </summary>
public class LeagueFullResponse : TetrioApiResponse<LeagueFullPacketData>;