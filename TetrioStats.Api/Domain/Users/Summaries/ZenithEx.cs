using JetBrains.Annotations;
using Newtonsoft.Json;
using TetrioStats.Api.Domain.Users.UserRecords;

namespace TetrioStats.Api.Domain.Users.Summaries;

/// <summary>
/// Represents the user's career best in Zenith Expert mode.
/// </summary>
public class ZenithExCareerBest
{
	/// <summary>
	/// The user's Zenith Expert record.
	/// </summary>
	[JsonProperty("record")]
	[CanBeNull]
	public ZenithExRecord Record { get; set; }

	/// <summary>
	/// The user's rank.
	/// </summary>
	[JsonProperty("rank")]
	public long Rank { get; set; }
}


/// <summary>
/// Represents the summary of a user's Zenith Expert mode performance.
/// </summary>
public class ZenithExSummary
{
	/// <summary>
	/// The user's Zenith Expert record.
	/// </summary>
	[JsonProperty("record")]
	[CanBeNull]
	public ZenithExRecord Record { get; set; }

	/// <summary>
	/// The user's rank in Zenith Expert mode.
	/// </summary>
	[JsonProperty("rank")]
	public long Rank { get; set; }

	/// <summary>
	/// The user's local rank in Zenith Expert mode.
	/// </summary>
	[JsonProperty("rank_local")]
	public long RankLocal { get; set; }

	/// <summary>
	/// The user's career best in Zenith Expert mode.
	/// </summary>
	[JsonProperty("best")]
	public ZenithExCareerBest Best { get; set; }
}


/// <summary>
/// Represents a packet containing Zenith EX summary data.
/// </summary>
public class ZenithExSummaryResponse : TetrioApiResponse<ZenithExSummary>;