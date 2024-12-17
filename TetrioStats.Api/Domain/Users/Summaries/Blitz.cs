using JetBrains.Annotations;
using Newtonsoft.Json;
using TetrioStats.Api.Domain.Users.UserRecords;

namespace TetrioStats.Api.Domain.Users.Summaries;

/// <summary>
/// Represents the Blitz summary for a user.
/// </summary>
public class BlitzSummary
{
	/// <summary>
	/// The Blitz record associated with this summary.
	/// </summary>
	[JsonProperty("record")]
	[CanBeNull]
	public BlitzRecord Record { get; set; }

	/// <summary>
	/// The global rank of the user for Blitz.
	/// </summary>
	[JsonProperty("rank")]
	public long Rank { get; set; }

	/// <summary>
	/// The local rank of the user for Blitz.
	/// </summary>
	[JsonProperty("rank_local")]
	public long LocalRank { get; set; }
}


/// <summary>
/// Represents a packet containing Blitz summary data.
/// </summary>
public class BlitzSummaryResponse : TetrioApiResponse<BlitzSummary>;