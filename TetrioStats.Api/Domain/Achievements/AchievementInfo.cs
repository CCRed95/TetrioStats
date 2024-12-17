using System.Collections.Generic;
using Newtonsoft.Json;
using TetrioStats.Api.Domain.Users.Summaries;
using TetrioStats.Api.Infrastructure;

namespace TetrioStats.Api.Domain.Achievements;

/// <summary>
/// Represents a user in the achievement leaderboard
/// </summary>
public class UserSummaryAchievements
{
	/// <summary>
	/// The user's internal ID.
	/// </summary>
	[JsonProperty("_id")]
	public string Id { get; set; }

	/// <summary>
	/// The user's username.
	/// </summary>
	[JsonProperty("username")]
	public string Username { get; set; }

	/// <summary>
	/// The user's role.
	/// </summary>
	[JsonProperty("role")]
	public UserRole Role { get; set; }

	/// <summary>
	/// Whether the user is supporting TETRIO.
	/// </summary>
	[JsonProperty("supporter")]
	public bool IsSupporter { get; set; }

	/// <summary>
	/// The user's country, if public.
	/// </summary>
	[JsonProperty("country")]
	public string CountryCode { get; set; }
}


/// <summary>
/// Represents an entry in the User Summary Achievements
/// </summary>
public class AchievementLeaderboard
{
	/// <summary>
	/// The user owning the achievement
	/// </summary>
	[JsonProperty("u")]
	public UserSummaryAchievements UserAchievements { get; set; }

	/// <summary>
	/// The achieved score in the achievement.
	/// </summary>
	[JsonProperty("v")]
	public float Value { get; set; }

	/// <summary>
	/// Additional score for the achievement.
	/// </summary>
	[JsonProperty("a")]
	public float? AdditionalScore { get; set; }

	/// <summary>
	/// The time the achievement was last updated.
	/// </summary>
	[JsonProperty("t")]
	public string Timestamp { get; set; }
}


/// <summary>
/// Represents the cutoff scores for different achievement ranks
/// </summary>
public class AchievementCutoffs
{
	/// <summary>
	/// The total amount of users with this achievement.
	/// </summary>
	[JsonProperty("total")]
	public int Total { get; set; }

	/// <summary>
	/// If applicable, the score required to obtain a Diamond rank.
	/// If null, any score is allowed; if not given, this rank is not available.
	/// </summary>
	[JsonProperty("diamond")]
	public float? Diamond { get; set; }

	/// <summary>
	/// If applicable, the score required to obtain a Platinum rank.
	/// If null, any score is allowed; if not given, this rank is not available.
	/// </summary>
	[JsonProperty("platinum")]
	public float? Platinum { get; set; }

	/// <summary>
	/// If applicable, the score required to obtain a Gold rank.
	/// If null, any score is allowed; if not given, this rank is not available.
	/// </summary>
	[JsonProperty("gold")]
	public float? Gold { get; set; }

	/// <summary>
	/// If applicable, the score required to obtain a Silver rank.
	/// If null, any score is allowed; if not given, this rank is not available.
	/// </summary>
	[JsonProperty("silver")]
	public float? Silver { get; set; }

	/// <summary>
	/// If applicable, the score required to obtain a Bronze rank.
	/// If null, any score is allowed; if not given, this rank is not available.
	/// </summary>
	[JsonProperty("bronze")]
	public float? Bronze { get; set; }
}


/// <summary>
/// Contains complete information about an achievement
/// </summary>
public class AchievementInfo
{
	[JsonProperty("achievement")]
	public Achievement Achievement { get; set; }

	[JsonProperty("leaderboard")]
	public List<AchievementLeaderboard> Leaderboard { get; set; }

	[JsonProperty("cutoffs")]
	public AchievementCutoffs Cutoffs { get; set; }
}


/// <summary>
/// Type alias for a packet containing achievement information
/// </summary>
public class AchievementInfoResponse : TetrioApiResponse<AchievementInfo>;