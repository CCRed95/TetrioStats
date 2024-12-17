using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Domain.Users.Summaries;

/// <summary>
/// Represents an achievement in the system.
/// </summary>
public class Achievement
{
	/// <summary>
	/// The unique identifier of the achievement.
	/// </summary>
	[JsonProperty("_id")]
	[CanBeNull]
	public string Id { get; set; }

	/// <summary>
	/// The Achievement ID for every type of achievement.
	/// </summary>
	[JsonProperty("k")]
	public long AchievementId { get; set; }

	/// <summary>
	/// The category of the achievement.
	/// </summary>
	[JsonProperty("category")]
	public string Category { get; set; }

	/// <summary>
	/// The primary name of the achievement.
	/// </summary>
	[JsonProperty("name")]
	public string Name { get; set; }

	/// <summary>
	/// The objective of the achievement.
	/// </summary>
	[JsonProperty("object")]
	public string Objective { get; set; }

	/// <summary>
	/// The flavor text of the achievement.
	/// </summary>
	[JsonProperty("desc")]
	public string Description { get; set; }

	/// <summary>
	/// The order of this achievement in its category.
	/// </summary>
	[JsonProperty("o")]
	public long? Order { get; set; }

	/// <summary>
	/// The rank type of this achievement.
	/// </summary>
	[JsonProperty("rt")]
	public byte RankType { get; set; }

	/// <summary>
	/// The achieved score for this achievement (replaces V).
	/// </summary>
	[JsonProperty("vt")]
	public byte AchievedScore { get; set; }

	/// <summary>
	/// The AR type of this achievement.
	/// </summary>
	[JsonProperty("art")]
	public byte AchievementRankType { get; set; }

	/// <summary>
	/// The minimum score required to obtain the achievement.
	/// </summary>
	[JsonProperty("min")]
	public long MinimumScore { get; set; }

	/// <summary>
	/// The number of decimal places to show.
	/// </summary>
	[JsonProperty("deci")]
	public long DecimalPlaces { get; set; }

	/// <summary>
	/// Indicates whether this achievement is usually not shown.
	/// </summary>
	[JsonProperty("hidden")]
	public bool IsHidden { get; set; }

	/// <summary>
	/// The optional achieved value.
	/// </summary>
	[JsonProperty("v")]
	public float? AchievedValue { get; set; }

	/// <summary>
	/// Additional data related to the achievement.
	/// </summary>
	[JsonProperty("a")]
	public float? AdditionalData { get; set; }

	/// <summary>
	/// The time the achievement was last updated.
	/// </summary>
	[JsonProperty("t")]
	[CanBeNull]
	public string UpdatedTime { get; set; }

	/// <summary>
	/// The zero-indexed position in the achievement's leaderboards.
	/// </summary>
	[JsonProperty("pos")]
	public long? LeaderboardPosition { get; set; }

	/// <summary>
	/// The total number of players who have achieved this milestone.
	/// </summary>
	[JsonProperty("total")]
	public long? TotalAchievers { get; set; }

	/// <summary>
	/// The rank of the achievement.
	/// </summary>
	[JsonProperty("rank")]
	public byte? Rank { get; set; }

	/// <summary>
	/// Additional optional metadata.
	/// </summary>
	[JsonProperty("n")]
	[CanBeNull]
	public string Metadata { get; set; }

	/// <summary>
	/// Indicates whether the achievement has no leaderboards.
	/// </summary>
	[JsonProperty("nolb")]
	public bool NoLeaderboard { get; set; }

	/// <summary>
	/// Indicates whether the achievement is a placeholder.
	/// </summary>
	[JsonProperty("stub")]
	public bool? IsStub { get; set; }
}


/// <summary>
/// Represents a collection of achievements.
/// </summary>
public class AchievementsSummary : List<Achievement>;


/// <summary>
/// Represents a packet containing Achievements summary data.
/// </summary>
public class AchievementsSummaryResponse : TetrioApiResponse<AchievementsSummary>;