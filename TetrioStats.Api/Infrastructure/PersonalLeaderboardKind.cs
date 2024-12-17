using Newtonsoft.Json;

namespace TetrioStats.Api.Infrastructure;

/// <summary>
/// Represents the personal leaderboard kind.
/// </summary>
public enum PersonalLeaderboardKind
{
	/// <summary>
	/// Top leaderboard.
	/// </summary>
	[JsonProperty("top")]
	Top,

	/// <summary>
	/// Recent leaderboard.
	/// </summary
	[JsonProperty("recent")]
	Recent,

	/// <summary>
	/// Progression leaderboard.
	/// </summary
	[JsonProperty("progression")]
	Progression
}