using System.ComponentModel;
using Newtonsoft.Json;

namespace TetrioStats.Api.Infrastructure;

/// <summary>
/// Represents the rank of a user in TETR.IO.
/// </summary>
public enum UserRank
{
	/// <summary>
	/// Rank D.
	/// </summary>
	[Description("d")]
	D,

	/// <summary>
	/// Rank D+.
	/// </summary>
	[Description("d+")]
	DPlus,

	/// <summary>
	/// Rank C-.
	/// </summary>
	[Description("c-")]
	CMinus,

	/// <summary>
	/// Rank C.
	/// </summary>
	[Description("c")]
	C,

	/// <summary>
	/// Rank C+.
	/// </summary>
	[Description("c+")]
	CPlus,

	/// <summary>
	/// Rank B-.
	/// </summary>
	[Description("b-")]
	BMinus,

	/// <summary>
	/// Rank B.
	/// </summary>
	[Description("b")]
	B,

	/// <summary>
	/// Rank B+.
	/// </summary>
	[Description("b+")]
	BPlus,

	/// <summary>
	/// Rank A-.
	/// </summary>
	[Description("a-")]
	AMinus,

	/// <summary>
	/// Rank A.
	/// </summary>
	[Description("a")]
	A,

	/// <summary>
	/// Rank A+.
	/// </summary>
	[Description("a+")]
	APlus,

	/// <summary>
	/// Rank S-.
	/// </summary>
	[Description("s-")]
	SMinus,

	/// <summary>
	/// Rank S.
	/// </summary>
	[Description("s")]
	S,

	/// <summary>
	/// Rank S+.
	/// </summary>
	[Description("s+")]
	SPlus,

	/// <summary>
	/// Rank SS.
	/// </summary>
	[Description("ss")]
	SS,

	/// <summary>
	/// Rank U.
	/// </summary>
	[Description("u")]
	U,

	/// <summary>
	/// Rank X.
	/// </summary>
	[Description("x")]
	X,

	/// <summary>
	/// Rank X+.
	/// </summary>
	[Description("x+")]
	XPlus,

	/// <summary>
	/// Rank Z.
	/// </summary>
	[Description("z")]
	Z
}