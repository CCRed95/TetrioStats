using JetBrains.Annotations;
using Newtonsoft.Json;
using TetrioStats.Api.Domain.Users.UserRecords;

namespace TetrioStats.Api.Domain.Users.Summaries;

/// <summary>
/// Represents the user's career best in Zenith mode.
/// </summary>
public class ZenithCareerBest
{
	/// <summary>
	/// The user's Zenith record.
	/// </summary>
	[JsonProperty("record")]
	[CanBeNull]
	public ZenithRecord Record { get; set; }

	/// <summary>
	/// The user's rank.
	/// </summary>
	[JsonProperty("rank")]
	public long Rank { get; set; }
}


/// <summary>
/// Represents the summary of a user's Zenith mode performance.
/// </summary>
public class ZenithSummary
{
	/// <summary>
	/// The user's Zenith record.
	/// </summary>
	[JsonProperty("record")]
	[CanBeNull]
	public ZenithRecord Record { get; set; }

	/// <summary>
	/// The user's rank in Zenith mode.
	/// </summary>
	[JsonProperty("rank")]
	public long Rank { get; set; }

	/// <summary>
	/// The user's local rank in Zenith mode.
	/// </summary>
	[JsonProperty("rank_local")]
	public long RankLocal { get; set; }

	/// <summary>
	/// The user's career best in Zenith mode.
	/// </summary>
	[JsonProperty("best")]
	public ZenithCareerBest Best { get; set; }
}


/// <summary>
/// Represents a packet containing Zenith summary data.
/// </summary>
public class ZenithSummaryResponse : TetrioApiResponse<ZenithSummary>;