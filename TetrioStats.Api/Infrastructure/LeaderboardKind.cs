using System.ComponentModel;

namespace TetrioStats.Api.Infrastructure;

/// <summary>
/// Represents the kind of leaderboard.
/// </summary>
public enum LeaderboardKind
{
	/// <summary>
	/// TETRA League Leaderboard.
	/// </summary>
	[Description("league")]
	League,

	/// <summary>
	/// XP leaderboard.
	/// </summary>
	[Description("xp")]
	Xp,

	/// <summary>
	/// Achievement rating leaderboard.
	/// </summary>
	[Description("ar")]
	Ar
}