using System.ComponentModel;

namespace TetrioStats.Data.Enums;

public enum GameType
{
	[Description("40l")] Single40Lines,
	[Description("blitz")] Blitz,
	[Description("zen")] Zen
}

public enum GameMode
{
	[Description("40l")] FortyLines,
	[Description("blitz")] Blitz,
	[Description("zenith")] QuickPlay,
	[Description("zenithex")] ExpertQuickPlay,
	[Description("league")] TetraLeague
}

public enum Leaderboard
{
	[Description("top")] Top,
	[Description("recent")] Recent,
	[Description("progression")] Progression
}
