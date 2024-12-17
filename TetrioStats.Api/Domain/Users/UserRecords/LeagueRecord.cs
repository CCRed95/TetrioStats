using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using TetrioStats.Api.Infrastructure;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Domain.Users.UserRecords;

/// <summary>
/// Represents the aggregate stats for a player's league leaderboard.
/// </summary>
public class LeagueLeaderboardStats
{
	[JsonProperty("apm")]
	public float? APM { get; set; }

	[JsonProperty("pps")]
	public float? PPS { get; set; }

	[JsonProperty("vsscore")]
	public float? VSScore { get; set; }

	[JsonProperty("garbagesent")]
	public float GarbageSent { get; set; }

	[JsonProperty("garbagereceived")]
	public float GarbageReceived { get; set; }

	[JsonProperty("kills")]
	public float Kills { get; set; }

	[JsonProperty("altitude")]
	public float Altitude { get; set; }

	[JsonProperty("rank")]
	public float Rank { get; set; }

	[JsonProperty("targetingfactor")]
	public float TargetingFactor { get; set; }

	[JsonProperty("targetinggrace")]
	public float TargetingGrace { get; set; }
}


/// <summary>
/// Represents a shadowed player.
/// </summary>
public class ShadowedBy
{
}


/// <summary>
/// Represents shadows cast by a player.
/// </summary>
public class Shadows
{
}


/// <summary>
/// Represents the leaderboard for a league match.
/// </summary>
public class LeagueLeaderboard
{
	/// <summary>
	/// The player's User ID.
	/// </summary>
	[JsonProperty("id")]
	public string Id { get; set; }

	/// <summary>
	/// The player's username.
	/// </summary>
	[JsonProperty("username")]
	public string Username { get; set; }

	/// <summary>
	/// Indicates if the player is still in the game.
	/// </summary>
	[JsonProperty("active")]
	public bool Active { get; set; }

	/// <summary>
	/// The number of rounds won by the player.
	/// </summary>
	[JsonProperty("wins")]
	public long Wins { get; set; }

	/// <summary>
	/// The aggregate stats for the player.
	/// </summary>
	[JsonProperty("stats")]
	public LeagueLeaderboardStats Stats { get; set; }

	/// <summary>
	/// The player's natural order.
	/// </summary>
	[JsonProperty("naturalorder")]
	public short? NaturalOrder { get; set; }

	/// <summary>
	/// List of players shadowing this player.
	/// </summary>
	[JsonProperty("shadowedBy")]
	[ItemCanBeNull]
	public List<ShadowedBy> ShadowedBy { get; set; }

	/// <summary>
	/// List of shadows cast by this player.
	/// </summary>
	[JsonProperty("shadows")]
	[ItemCanBeNull]
	public List<Shadows> Shadows { get; set; }
}


/// <summary>
/// Represents the stats for a player in a specific league round.
/// </summary>
public class LeagueRoundStats
{
	[JsonProperty("apm")]
	public float APM { get; set; }

	[JsonProperty("pps")]
	public float PPS { get; set; }

	[JsonProperty("vsscore")]
	public float VSScore { get; set; }

	[JsonProperty("garbagesent")]
	public float GarbageSent { get; set; }

	[JsonProperty("garbagereceived")]
	public float GarbageReceived { get; set; }

	[JsonProperty("kills")]
	public float Kills { get; set; }

	[JsonProperty("altitude")]
	public float Altitude { get; set; }

	[JsonProperty("rank")]
	public float Rank { get; set; }

	[JsonProperty("targetingfactor")]
	public float TargetingFactor { get; set; }

	[JsonProperty("targetinggrace")]
	public float TargetingGrace { get; set; }
}


/// <summary>
/// Represents a player's stats for a specific round.
/// </summary>
public class LeagueRound
{
	[JsonProperty("id")]
	public string Id { get; set; }

	[JsonProperty("username")]
	public string Username { get; set; }

	[JsonProperty("active")]
	public bool Active { get; set; }

	[JsonProperty("alive")]
	public bool Alive { get; set; }

	[JsonProperty("lifetime")]
	public long Lifetime { get; set; }

	[JsonProperty("stats")]
	public LeagueRoundStats Stats { get; set; }

	[JsonProperty("naturalorder")]
	public short? NaturalOrder { get; set; }

	[JsonProperty("shadowedBy")]
	[ItemCanBeNull]
	public List<ShadowedBy> ShadowedBy { get; set; }

	[JsonProperty("shadows")]
	[ItemCanBeNull]
	public List<Shadows> Shadows { get; set; }
}


/// <summary>
/// Represents the results of a league match.
/// </summary>
public class LeagueResults
{
	[JsonProperty("leaderboard")]
	public List<LeagueLeaderboard> Leaderboard { get; set; }

	[JsonProperty("rounds")]
	public List<List<LeagueRound>> Rounds { get; set; }
}


/// <summary>
/// Represents a user in a league record.
/// </summary>
public class LeagueRecordUser
{
	[JsonProperty("id")]
	public string Id { get; set; }

	[JsonProperty("username")]
	public string Username { get; set; }

	[JsonProperty("avatar_revision")]
	public long? AvatarRevision { get; set; }

	[JsonProperty("banner_revision")]
	public long? BannerRevision { get; set; }

	[JsonProperty("country")]
	[CanBeNull]
	public string Country { get; set; }

	[JsonProperty("supporter")]
	public bool Supporter { get; set; }
}


/// <summary>
/// Represents additional data for a league record.
/// </summary>
public class LeagueExtrasData
{
	[JsonProperty("glicko")]
	public float Glicko { get; set; }

	[JsonProperty("placement")]
	public float? Placement { get; set; }

	[JsonProperty("rank")]
	public UserRank Rank { get; set; }

	[JsonProperty("rd")]
	public float RatingDeviation { get; set; }

	[JsonProperty("tr")]
	public float TetraRating { get; set; }
}


/// <summary>
/// Represents the extras for a league record.
/// </summary>
public class LeagueExtras
{
	[JsonProperty("league")]
	public Dictionary<string, List<LeagueExtrasData?>> League { get; set; }

	[JsonProperty("result")]
	public string Result { get; set; }
}


/// <summary>
/// Represents a league record.
/// </summary>
public class LeagueRecord : PersonalUserRecord
{
	[JsonProperty("_id")]
	public string Id { get; set; }

	[JsonProperty("replayid")]
	public string ReplayId { get; set; }

	[JsonProperty("stub")]
	public bool Stub { get; set; }

	[JsonProperty("gamemode")]
	public GameMode GameMode { get; set; }

	[JsonProperty("pb")]
	public bool PersonalBest { get; set; }

	[JsonProperty("oncepb")]
	public bool OncePersonalBest { get; set; }

	[JsonProperty("ts")]
	public string Timestamp { get; set; }

	[JsonProperty("revolution")]
	[CanBeNull]
	public string Revolution { get; set; }

	[JsonProperty("user")]
	[CanBeNull]
	public LeagueRecordUser User { get; set; }

	[JsonProperty("otherusers")]
	public List<LeagueRecordUser> OtherUsers { get; set; }

	[JsonProperty("leaderboards")]
	public List<string> Leaderboards { get; set; }

	[JsonProperty("results")]
	public LeagueResults Results { get; set; }

	[JsonProperty("extras")]
	public LeagueExtras Extras { get; set; }

	[JsonProperty("disputed")]
	public bool Disputed { get; set; }

	[JsonProperty("p")]
	public Prisecter Prisecter { get; set; }
}


/// <summary>
/// Represents a packet containing personal league records.
/// </summary>
public class PersonalLeagueRecordResponse : TetrioApiResponse<PersonalUserRecord<LeagueRecord>>;