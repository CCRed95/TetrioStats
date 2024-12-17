using Newtonsoft.Json;

namespace TetrioStats.Api.Infrastructure;

/// <summary>
/// Represents the rank of a user in TETR.IO.
/// </summary>
public enum UserRank
{
	/// <summary>
	/// Rank X+.
	/// </summary>
	[JsonProperty("x+")]
	XPlus,

	/// <summary>
	/// Rank X.
	/// </summary>
	[JsonProperty("x")]
	X,

	/// <summary>
	/// Rank U.
	/// </summary>
	[JsonProperty("u")]
	U,

	/// <summary>
	/// Rank SS.
	/// </summary>
	[JsonProperty("ss")]
	SS,

	/// <summary>
	/// Rank S+.
	/// </summary>
	[JsonProperty("s+")]
	SPlus,

	/// <summary>
	/// Rank S.
	/// </summary>
	[JsonProperty("s")]
	S,

	/// <summary>
	/// Rank S-.
	/// </summary>
	[JsonProperty("s-")]
	SMinus,

	/// <summary>
	/// Rank A+.
	/// </summary>
	[JsonProperty("a+")]
	APlus,

	/// <summary>
	/// Rank A.
	/// </summary>
	[JsonProperty("a")]
	A,

	/// <summary>
	/// Rank A-.
	/// </summary>
	[JsonProperty("a-")]
	AMinus,

	/// <summary>
	/// Rank B+.
	/// </summary>
	[JsonProperty("b+")]
	BPlus,

	/// <summary>
	/// Rank B.
	/// </summary>
	[JsonProperty("b")]
	B,

	/// <summary>
	/// Rank B-.
	/// </summary>
	[JsonProperty("b-")]
	BMinus,

	/// <summary>
	/// Rank C+.
	/// </summary>
	[JsonProperty("c+")]
	CPlus,

	/// <summary>
	/// Rank C.
	/// </summary>
	[JsonProperty("c")]
	C,

	/// <summary>
	/// Rank C-.
	/// </summary>
	[JsonProperty("c-")]
	CMinus,

	/// <summary>
	/// Rank D+.
	/// </summary>
	[JsonProperty("d+")]
	DPlus,

	/// <summary>
	/// Rank D.
	/// </summary>
	[JsonProperty("d")]
	D,

	/// <summary>
	/// Rank Z.
	/// </summary>
	[JsonProperty("z")]
	Z
}