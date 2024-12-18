using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using TetrioStats.Api.Domain.Converters;
using TetrioStats.Api.Infrastructure;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Domain.Users;

/// <summary>
/// Represents detailed information about a user.
/// </summary>
public class UserInfo
{
	/// <summary>
	/// The unique identifier of the user.
	/// </summary>
	[JsonProperty("_id")]
	public string Id { get; set; }

	/// <summary>
	/// The username of the user.
	/// </summary>
	[JsonProperty("username")]
	public string Username { get; set; }

	/// <summary>
	/// The role of the user.
	/// </summary>
	[JsonProperty("role")]
	public UserRole Role { get; set; }

	/// <summary>
	/// The timestamp when the user was created, if available.
	/// </summary>
	[JsonProperty("ts")]
	[CanBeNull]
	public DateTime? Timestamp { get; set; }

	/// <summary>
	/// The bot master associated with the user, if any.
	/// </summary>
	[JsonProperty("botmaster")]
	[CanBeNull]
	public string BotMaster { get; set; }

	/// <summary>
	/// The list of badges associated with the user.
	/// </summary>
	[JsonProperty("badges")]
	public List<UserBadge> Badges { get; set; }

	/// <summary>
	/// The user's experience points.
	/// </summary>
	[JsonProperty("xp")]
	public double XP { get; set; }

	/// <summary>
	/// The total number of games played by the user.
	/// </summary>
	[JsonProperty("gamesplayed")]
	public int GamesPlayed { get; set; }

	/// <summary>
	/// The total number of games won by the user.
	/// </summary>
	[JsonProperty("gameswon")]
	public int GamesWon { get; set; }

	/// <summary>
	/// The total time spent playing games by the user, in seconds.
	/// </summary>
	[JsonProperty("gametime")]
	[JsonConverter(typeof(SecondsToTimeSpanConverter))]
	public TimeSpan GameTime { get; set; }

	/// <summary>
	/// The country the user is associated with, if available.
	/// </summary>
	[JsonProperty("country")]
	[CanBeNull]
	public string CountryCode { get; set; }

	/// <summary>
	/// Indicates whether the user is in bad standing, if applicable.
	/// </summary>
	[JsonProperty("badstanding")]
	public bool? BadStanding { get; set; }

	/// <summary>
	/// Indicates whether the user is a TETR.IO supporter.
	/// </summary>
	[JsonProperty("supporter")]
	public bool? IsSupporter { get; set; }

	/// <summary>
	/// The supporter's tier.
	/// </summary>
	[JsonProperty("supporter_tier")]
	public int SupporterTier { get; set; }

	/// <summary>
	/// The revision number of the user's avatar, if any.
	/// </summary>
	[JsonProperty("avatar_revision")]
	public long? AvatarRevision { get; set; }

	/// <summary>
	/// The revision number of the user's banner, if any.
	/// </summary>
	[JsonProperty("banner_revision")]
	public long? BannerRevision { get; set; }

	/// <summary>
	/// The biography of the user, if any.
	/// </summary>
	[JsonProperty("bio")]
	[CanBeNull]
	public string Bio { get; set; }

	/// <summary>
	/// The connections associated with the user.
	/// </summary>
	[JsonProperty("connections")]
	public UserConnections Connections { get; set; }

	/// <summary>
	/// The number of friends the user has, if available.
	/// </summary>
	[JsonProperty("friend_count")]
	public int? FriendCount { get; set; }

	/// <summary>
	/// The user's special distinguishing details, if any.
	/// </summary>
	[JsonProperty("distinguishment")]
	[CanBeNull]
	public UserDistinguishment Distinguishment { get; set; }

	/// <summary>
	/// The achievements associated with the user.
	/// </summary>
	[JsonProperty("achievements")]
	public List<int> Achievements { get; set; }

	/// <summary>
	/// The user's achievement rank.
	/// </summary>
	[JsonProperty("ar")]
	public int AchievementRank { get; set; }

	/// <summary>
	/// The user's achievement rank counts.
	/// </summary>
	[JsonProperty("ar_counts")]
	public UserArCounts AchievementRankCounts { get; set; }
}


/// <summary>
/// Represents a badge associated with a user.
/// </summary>
public class UserBadge
{
	/// <summary>
	/// The unique identifier of the badge.
	/// </summary>
	[JsonProperty("id")]
	public string Id { get; set; }

	/// <summary>
	/// The label or name of the badge.
	/// </summary>
	[JsonProperty("label")]
	public string Label { get; set; }

	/// <summary>
	/// The timestamp associated with the badge, if any.
	/// </summary>
	[JsonProperty("ts")]
	public DateTime? Timestamp { get; set; }

	/// <summary>
	/// The group or category the badge belongs to, if any.
	/// </summary>
	[JsonProperty("group")]
	[CanBeNull]
	public string Group { get; set; }
}


/// <summary>
/// Represents a user's special distinguishing details.
/// </summary>
public class UserDistinguishment
{
	/// <summary>
	/// The type of distinguishment.
	/// </summary>
	[JsonProperty("type")]
	public string DistinguishmentType { get; set; }

	/// <summary>
	/// Additional detail associated with the distinguishment, if any.
	/// </summary>
	[JsonProperty("detail")]
	[CanBeNull]
	public string Detail { get; set; }

	/// <summary>
	/// The header text associated with the distinguishment, if any.
	/// </summary>
	[JsonProperty("header")]
	[CanBeNull]
	public string Header { get; set; }

	/// <summary>
	/// The footer text associated with the distinguishment, if any.
	/// </summary>
	[JsonProperty("footer")]
	[CanBeNull]
	public string Footer { get; set; }
}


/// <summary>
/// Represents a Steam connection for a user.
/// </summary>
public class ExternalConnection
{
	/// <summary>
	/// The unique identifier of the connection.
	/// </summary>
	[JsonProperty("id")]
	public string Id { get; set; }

	/// <summary>
	/// The username of the account.
	/// </summary>
	[JsonProperty("username")]
	public string Username { get; set; }

	/// <summary>
	/// The display username of the account.
	/// </summary>
	[JsonProperty("display_username")]
	public string DisplayUsername { get; set; }
}


/// <summary>
/// Represents all external connections associated with a user.
/// </summary>
public class UserConnections
{
	/// <summary>
	/// The user's Discord connection, if any.
	/// </summary>
	[JsonProperty("discord")]
	[CanBeNull]
	public ExternalConnection Discord { get; set; }

	/// <summary>
	/// The user's Twitch connection, if any.
	/// </summary>
	[JsonProperty("twitch")]
	[CanBeNull]
	public ExternalConnection Twitch { get; set; }

	/// <summary>
	/// The user's Twitter connection, if any.
	/// </summary>
	[JsonProperty("twitter")]
	[CanBeNull]
	public ExternalConnection Twitter { get; set; }

	/// <summary>
	/// The user's Reddit connection, if any.
	/// </summary>
	[JsonProperty("reddit")]
	[CanBeNull]
	public ExternalConnection Reddit { get; set; }

	/// <summary>
	/// The user's YouTube connection, if any.
	/// </summary>
	[JsonProperty("youtube")]
	[CanBeNull]
	public ExternalConnection Youtube { get; set; }

	/// <summary>
	/// The user's Steam connection, if any.
	/// </summary>
	[JsonProperty("steam")]
	[CanBeNull]
	public ExternalConnection Steam { get; set; }
}


/// <summary>
/// Represents the achievement rank counts for a user.
/// </summary>
public class UserArCounts
{
	/// <summary>
	/// The count of Bronze achievements.
	/// </summary>
	[JsonProperty("1")]
	public int? Bronze { get; set; }

	/// <summary>
	/// The count of Silver achievements.
	/// </summary>
	[JsonProperty("2")]
	public int? Silver { get; set; }

	/// <summary>
	/// The count of Gold achievements.
	/// </summary>
	[JsonProperty("3")]
	public int? Gold { get; set; }

	/// <summary>
	/// The count of Platinum achievements.
	/// </summary>
	[JsonProperty("4")]
	public int? Platinum { get; set; }

	/// <summary>
	/// The count of Diamond achievements.
	/// </summary>
	[JsonProperty("5")]
	public int? Diamond { get; set; }

	/// <summary>
	/// The count of issued achievements.
	/// </summary>
	[JsonProperty("100")]
	public int? Issued { get; set; }

	/// <summary>
	/// The count of Top 100 rankings.
	/// </summary>
	[JsonProperty("t100")]
	public int? Top100 { get; set; }

	/// <summary>
	/// The count of Top 50 rankings.
	/// </summary>
	[JsonProperty("t50")]
	public int? Top50 { get; set; }

	/// <summary>
	/// The count of Top 25 rankings.
	/// </summary>
	[JsonProperty("t25")]
	public int? Top25 { get; set; }

	/// <summary>
	/// The count of Top 10 rankings.
	/// </summary>
	[JsonProperty("t10")]
	public int? Top10 { get; set; }

	/// <summary>
	/// The count of Top 5 rankings.
	/// </summary>
	[JsonProperty("t5")]
	public int? Top5 { get; set; }

	/// <summary>
	/// The count of Top 3 rankings.
	/// </summary>
	[JsonProperty("t3")]
	public int? Top3 { get; set; }
}


/// <summary>
/// Represents a packet containing user information.
/// </summary>
public class UserInfoResponse : TetrioApiResponse<UserInfo>;


public class UserMeResponse : TetrioApiResponse<object>;