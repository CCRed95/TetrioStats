using JetBrains.Annotations;
using Newtonsoft.Json;
using TetrioStats.Api.Domain.Users.UserRecords;

namespace TetrioStats.Api.Domain.Users.Summaries;

/// <summary>
/// Represents the Sprint summary for a user.
/// </summary>
public class SprintSummary
{
	/// <summary>
	/// The Sprint record associated with this summary.
	/// </summary>
	[JsonProperty("record")]
	[CanBeNull]
	public SprintRecord Record { get; set; }

	/// <summary>
	/// The global rank of the user for Sprint.
	/// </summary>
	[JsonProperty("rank")]
	public long Rank { get; set; }

	/// <summary>
	/// The local rank of the user for Sprint.
	/// </summary>
	[JsonProperty("rank_local")]
	public long LocalRank { get; set; }
}


/// <summary>
/// Represents a packet containing Sprint summary data.
/// </summary>
public class SprintSummaryResponse : TetrioApiResponse<SprintSummary>;