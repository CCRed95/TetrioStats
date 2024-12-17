using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Users.Summaries;

/// <summary>
/// Represents a collection of all user summaries.
/// </summary>
public class AllSummaries
{
	/// <summary>
	/// User Summary: 40 LINES (Sprint).
	/// </summary>
	[JsonProperty("40l")]
	public SprintSummary Sprint { get; set; }

	/// <summary>
	/// User Summary: BLITZ.
	/// </summary>
	[JsonProperty("blitz")]
	public BlitzSummary Blitz { get; set; }

	/// <summary>
	/// User Summary: QUICK PLAY.
	/// </summary>
	[JsonProperty("zenith")]
	public ZenithSummary Zenith { get; set; }

	/// <summary>
	/// User Summary: EXPERT QUICK PLAY.
	/// </summary>
	[JsonProperty("zenithex")]
	public ZenithExSummary ZenithEx { get; set; }

	/// <summary>
	/// User Summary: TETRA LEAGUE.
	/// </summary>
	[JsonProperty("league")]
	public LeagueSummary League { get; set; }

	/// <summary>
	/// User Summary: ZEN.
	/// </summary>
	[JsonProperty("zen")]
	public ZenSummary Zen { get; set; }

	/// <summary>
	/// User Summary: Achievements.
	/// </summary>
	[JsonProperty("achievements")]
	public AchievementsSummary Achievements { get; set; }
}

/// <summary>
/// Represents a packet containing all user summaries.
/// </summary>
public class AllSummariesResponse : TetrioApiResponse<AllSummaries>;





