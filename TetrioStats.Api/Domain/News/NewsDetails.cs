using System.Collections.Generic;
using System.Text.Json;
using Newtonsoft.Json;
using TetrioStats.Api.Infrastructure;
using TetrioStats.Data.Enums;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Domain.News;

/// <summary>
/// Represents a single news item.
/// </summary>
public class News
{
	/// <summary>
	/// The unique identifier of the news item.
	/// </summary>
	[JsonProperty("_id")]
	public string Id { get; set; }

	/// <summary>
	/// The stream associated with the news item.
	/// </summary>
	[JsonProperty("stream")]
	public string Stream { get; set; }

	/// <summary>
	/// The type of the news item.
	/// </summary>
	[JsonProperty("type")]
	public string ItemType { get; set; }

	/// <summary>
	/// The data payload of the news item.
	/// </summary>
	[JsonProperty("data")]
	public JsonElement Data { get; set; }

	/// <summary>
	/// The timestamp of the news item.
	/// </summary>
	[JsonProperty("ts")]
	public string Timestamp { get; set; }
}


/// <summary>
/// When a user's new personal best enters a global leaderboard. Seen in the global stream only.
/// </summary>
public class NewsLeaderboardData
{
	/// <summary>
	/// The username of the person who got the leaderboard spot.
	/// </summary>
	[JsonProperty("username")]
	public string Username { get; set; }

	/// <summary>
	/// The game mode played.
	/// </summary>
	[JsonProperty("gametype")]
	public GameType GameType { get; set; }

	/// <summary>
	/// The global rank achieved.
	/// </summary>
	[JsonProperty("rank")]
	public int Rank { get; set; }

	/// <summary>
	/// The result (score or time) achieved.
	/// </summary>
	[JsonProperty("result")]
	public float Result { get; set; }

	/// <summary>
	/// The replay's shortID.
	/// </summary>
	[JsonProperty("replayid")]
	public string ReplayId { get; set; }
}


/// <summary>
/// When a user gets a personal best. Seen in user streams only.
/// </summary>
public class NewsPersonalBestData
{
	/// <summary>
	/// The username of the player.
	/// </summary>
	[JsonProperty("username")]
	public string Username { get; set; }

	/// <summary>
	/// The game mode played.
	/// </summary>
	[JsonProperty("gametype")]
	public GameType GameType { get; set; }

	/// <summary>
	/// The result (score or time) achieved.
	/// </summary>
	[JsonProperty("result")]
	public float Result { get; set; }

	/// <summary>
	/// The replay's shortID.
	/// </summary>
	[JsonProperty("replayid")]
	public string ReplayId { get; set; }
}

/// <summary>
/// When a user gets a badge. Seen in user streams only.
/// </summary>
public class NewsBadgeData
{
	/// <summary>
	/// The username of the player.
	/// </summary>
	[JsonProperty("username")]
	public string Username { get; set; }

	/// <summary>
	/// The badge's internal ID, and the filename of the badge icon (all PNGs within /res/badges/)
	/// </summary>
	[JsonProperty("type")]
	public string Type { get; set; }

	/// <summary>
	/// The badge's label.
	/// </summary>
	[JsonProperty("label")]
	public string Label { get; set; }
}


/// <summary>
/// When a user gets a badge. Seen in user streams only.
/// </summary>
public class NewsRankUpData
{
	/// <summary>
	/// The username of the player.
	/// </summary>
	[JsonProperty("username")]
	public string Username { get; set; }

	/// <summary>
	/// The new rank.
	/// </summary>
	[JsonProperty("type")]
	public UserRank Type { get; set; }
}

/// <summary>
/// When a user gets TETR.IO Supporter. Seen in user streams only.
/// </summary>
public class NewsSupporterData
{
	/// <summary>
	/// The username of the player.
	/// </summary>
	[JsonProperty("username")]
	public string Username { get; set; }
}


/// <summary>
/// When a user is gifted TETR.IO Supporter. Seen in user streams only.
/// </summary>
public class SupporterGiftData
{
	/// <summary>
	/// the username of the recipient.
	/// </summary>
	[JsonProperty("username")]
	public string Username { get; set; }
}


/// <summary>
/// Represents the data packet containing news items.
/// </summary>
public class NewsPacketData
{
	/// <summary>
	/// The list of news items.
	/// </summary>
	[JsonProperty("news")]
	public List<News> News { get; set; }
}


/// <summary>
/// Represents a packet containing the news data.
/// </summary>
public class NewsResponse : TetrioApiResponse<NewsPacketData>;