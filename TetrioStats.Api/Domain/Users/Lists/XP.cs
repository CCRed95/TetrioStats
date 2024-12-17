using System.Collections.Generic;
using Newtonsoft.Json;
using TetrioStats.Api.Infrastructure;


// ReSharper disable StringLiteralTypo

//! [XP Leaderboard](https://tetr.io/about/api/#userlistsxp) models

namespace TetrioStats.Api.Domain.Users.Lists;

/// <summary>
/// Represents a user in the XP leaderboard.
/// </summary>
public class XPUser
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
	/// The user's role (e.g., "anon", "user", "bot", "halfmod", "mod", "admin", "sysop").
	/// </summary>
	[JsonProperty("role")]
	public UserRole Role { get; set; }

	/// <summary>
	/// When the user account was created. If not set, this account was created before join dates were recorded.
	/// </summary>
	[JsonProperty("ts")]
	public string Timestamp { get; set; }

	/// <summary>
	/// The user's ISO 3166-1 country code, or null if hidden/unknown. Some vanity flags exist.
	/// </summary>
	[JsonProperty("country")]
	public string Country { get; set; }

	/// <summary>
	/// Whether this user is currently supporting TETR.IO <3.
	/// </summary>
	[JsonProperty("supporter")]
	public bool? Supporter { get; set; }

	/// <summary>
	/// Whether this user is a verified account.
	/// </summary>
	[JsonProperty("verified")]
	public bool Verified { get; set; }

	/// <summary>
	/// The user's XP in points.
	/// </summary>
	[JsonProperty("xp")]
	public double Xp { get; set; }

	/// <summary>
	/// The number of online games played by this user. If the user has chosen to hide this statistic, it will be -1.
	/// </summary>
	[JsonProperty("gamesplayed")]
	public long GamesPlayed { get; set; }

	/// <summary>
	/// The number of online games won by this user. If the user has chosen to hide this statistic, it will be -1.
	/// </summary>
	[JsonProperty("gameswon")]
	public long GamesWon { get; set; }

	/// <summary>
	/// The number of seconds this user spent playing, both online and offline. If the user has chosen to hide this statistic, it will be -1.
	/// </summary>
	[JsonProperty("gametime")]
	public double GameTime { get; set; }
}


/// <summary>
/// Represents the XP leaderboard data.
/// </summary>
public class XPResponseData
{
	/// <summary>
	/// The matched users.
	/// </summary>
	[JsonProperty("users")]
	public List<XPUser> Users { get; set; }
}


/// <summary>
/// Represents a packet containing XP leaderboard data.
/// </summary>
public class XPResponse : TetrioApiResponse<XPResponseData>;