using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Users.Summaries;

/// <summary>
/// Represents the summary of a user's Zen mode performance.
/// </summary>
public class ZenSummary
{
	/// <summary>
	/// The user's level in Zen mode.
	/// </summary>
	[JsonProperty("level")]
	public long Level { get; set; }

	/// <summary>
	/// The user's score in Zen mode.
	/// </summary>
	[JsonProperty("score")]
	public float Score { get; set; }
}


/// <summary>
/// Represents a packet containing Zen summary data.
/// </summary>
public class ZenSummaryResponse : TetrioApiResponse<ZenSummary>;