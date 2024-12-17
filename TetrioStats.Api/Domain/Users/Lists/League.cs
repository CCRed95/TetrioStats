using System.Collections.Generic;
using Newtonsoft.Json;
using TetrioStats.Api.Infrastructure;


// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Domain.Users.Lists;

/// <summary>
/// The user's current TETRA LEAGUE standing
/// </summary>
public class LeagueData
{
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
	/// This user's TR (Tetra Rating).
	/// </summary>
	[JsonProperty("tr")]
	public double TR { get; set; }

	/// <summary>
	/// This user's GLIXARE.
	/// </summary>
	[JsonProperty("gxe")]
	public double Glixare { get; set; }

	/// <summary>
	/// This user's letter rank.
	/// </summary>
	[JsonProperty("rank")]
	public UserRank UserRank { get; set; }

	/// <summary>
	/// This user's highest achieved rank this season.
	/// </summary>
	[JsonProperty("bestrank")]
	public UserRank BestRank { get; set; }

	/// <summary>
	/// This user's Glicko-2 rating.
	/// </summary>
	[JsonProperty("glicko")]
	public double Glicko { get; set; }

	/// <summary>
	/// This user's Glicko-2 Rating Deviation.
	/// </summary>
	[JsonProperty("rd")]
	public double RatingDeviation { get; set; }

	/// <summary>
	/// This user's average APM (attack per minute) over the last 10 games.
	/// </summary>
	[JsonProperty("apm")]
	public float APM { get; set; }

	/// <summary>
	/// This user's average PPS (pieces per second) over the last 10 games.
	/// </summary>
	[JsonProperty("pps")]
	public float PPS { get; set; }

	/// <summary>
	/// This user's average VS (versus score) over the last 10 games.
	/// </summary>
	[JsonProperty("vs")]
	public float VS { get; set; }

	/// <summary>
	/// Whether this user's RD is rising (has not played in the last week).
	/// </summary>
	[JsonProperty("decaying")]
	public bool Decaying { get; set; }
}


public class LeagueUser
{
	[JsonProperty("_id")]
	public string Id { get; set; }

	[JsonProperty("username")]
	public string Username { get; set; }

	[JsonProperty("role")]
	public UserRole Role { get; set; }

	[JsonProperty("xp")]
	public double XP { get; set; }

	[JsonProperty("country")]
	public string Country { get; set; }

	[JsonProperty("supporter")]
	public bool? Supporter { get; set; }

	[JsonProperty("verified")]
	public bool Verified { get; set; }

	[JsonProperty("league")]
	public LeagueData League { get; set; }
}


public class LeaguePacketData
{
	[JsonProperty("users")]
	public List<LeagueUser> Users { get; set; }
}


public class LeagueResponse : TetrioApiResponse<LeaguePacketData>;