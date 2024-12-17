using Newtonsoft.Json;

namespace TetrioStats.Api.Infrastructure;

/// <summary>
/// Represents the game mode.
/// </summary>
public enum GameMode
{
	/// <summary>
	/// Sprint mode (40 lines).
	/// </summary>
	[JsonProperty("40l")]
	Sprint,

	/// <summary>
	/// Blitz mode.
	/// </summary>
	[JsonProperty("blitz")]
	Blitz,

	/// <summary>
	/// Zenith mode.
	/// </summary>
	[JsonProperty("zenith")]
	Zenith,

	/// <summary>
	/// Zenith EX mode.
	/// </summary>
	[JsonProperty("zenithex")]
	ZenithEX,

	/// <summary>
	/// League mode.
	/// </summary>
	[JsonProperty("league")]
	League
}