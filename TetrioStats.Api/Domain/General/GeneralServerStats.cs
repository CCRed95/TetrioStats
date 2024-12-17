using Newtonsoft.Json;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Domain.General;

public class GeneralServerStats
{
	/// <summary>
	/// The amount of users on the server, including anonymous accounts.
	/// </summary>
	[JsonProperty("usercount")]
	public int UserCount { get; set; }

	/// <summary>
	/// The amount of users created a second (through the last minute).
	/// </summary>
	[JsonProperty("usercount_delta")]
	public float UserCountDelta { get; set; }

	/// <summary>
	/// The amount of anonymous accounts on the server.
	/// </summary>
	[JsonProperty("anoncount")]
	public int AnonCount { get; set; }

	/// <summary>
	/// The total amount of accounts ever created (including pruned anons etc.).
	/// </summary>
	[JsonProperty("totalaccounts")]
	public int TotalAccounts { get; set; }

	/// <summary>
	/// The amount of ranked (visible in TETRA LEAGUE leaderboard) accounts on the server.
	/// </summary>
	[JsonProperty("rankedcount")]
	public int RankedCount { get; set; }

	/// <summary>
	/// The amount of game records stored on the server.
	/// </summary>
	[JsonProperty("recordcount")]
	public int RecordCount { get; set; }

	/// <summary>
	/// The amount of games played across all users, including both off- and online modes.
	/// </summary>
	[JsonProperty("gamesplayed")]
	public int GamesPlayed { get; set; }

	/// <summary>
	/// The amount of games played a second (through the last minute).
	/// </summary>
	[JsonProperty("gamesplayed_delta")]
	public float GamesPlayedDelta { get; set; }

	/// <summary>
	/// The amount of games played across all users, including both off- and online modes, excluding games that were not completed (e.g. retries)
	/// </summary>
	[JsonProperty("gamesfinished")]
	public int GamesFinished { get; set; }

	/// <summary>
	/// The amount of seconds spent playing across all users, including both off- and online modes.
	/// </summary>
	[JsonProperty("gametime")]
	public float GameTime { get; set; }

	/// <summary>
	/// The amount of keys pressed across all users, including both off- and online modes.
	/// </summary>
	[JsonProperty("inputs")]
	public int Inputs { get; set; }

	/// <summary>
	/// The amount of pieces placed across all users, including both off- and online modes.
	/// </summary>
	[JsonProperty("piecesplaced")]
	public int PiecesPlaced { get; set; }
}


public class GeneralServerStatsResponse : TetrioApiResponse<GeneralServerStats>;