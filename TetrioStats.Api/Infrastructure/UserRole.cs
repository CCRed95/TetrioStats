using Newtonsoft.Json;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Infrastructure;

/// <summary>
/// Represents the role of a user in TETR.IO.
/// </summary>
public enum UserRole
{
	/// <summary>
	/// An anonymous user.
	/// </summary>`
	[JsonProperty("anon")]
	Anon,

	/// <summary>
	/// A registered user.
	/// </summary>
	[JsonProperty("user")]
	User,

	/// <summary>
	/// A bot account.
	/// </summary>
	[JsonProperty("bot")]
	Bot,

	/// <summary>
	/// A moderator account.
	/// </summary>
	[JsonProperty("mod")]
	Mod,

	/// <summary>
	/// An administrator account.
	/// </summary>
	[JsonProperty("admin")]
	Admin,

	/// <summary>
	/// A banned user.
	/// </summary>
	[JsonProperty("banned")]
	Banned,

	/// <summary>
	/// A half-moderator account.
	/// </summary>
	[JsonProperty("halfmod")]
	HalfMod,

	/// <summary>
	/// A system operator account.
	/// </summary>
	[JsonProperty("sysop")]
	SysOp,

	/// <summary>
	/// A hidden user account.
	/// </summary>
	[JsonProperty("hidden")]
	Hidden
}