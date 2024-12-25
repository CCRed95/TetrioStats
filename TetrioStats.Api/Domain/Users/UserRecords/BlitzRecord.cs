using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Domain.Users.UserRecords;

/// <summary>
/// Represents aggregate statistics for a Blitz game.
/// </summary>
public class BlitzAggregateStats
{
	[JsonProperty("apm")]
	public double APM { get; set; }

	[JsonProperty("pps")]
	public double PPS { get; set; }

	[JsonProperty("vsscore")]
	public double VSScore { get; set; }
}


/// <summary>
/// Represents time-related information for a Blitz game.
/// </summary>
public class BlitzTime
{
	[JsonProperty("start")]
	public long Start { get; set; }

	[JsonProperty("zero")]
	public bool Zero { get; set; }

	[JsonProperty("locked")]
	public bool Locked { get; set; }

	[JsonProperty("prev")]
	public long Previous { get; set; }

	[JsonProperty("frameoffset")]
	public long? FrameOffset { get; set; }
}


/// <summary>
/// Represents clears during a Blitz game.
/// </summary>
public class BlitzClears
{
	[JsonProperty("singles")]
	public int Singles { get; set; }

	[JsonProperty("doubles")]
	public int Doubles { get; set; }

	[JsonProperty("triples")]
	public int Triples { get; set; }

	[JsonProperty("quads")]
	public int Quads { get; set; }

	[JsonProperty("pentas")]
	public int? Pentas { get; set; }

	[JsonProperty("realtspins")]
	public int RealTSpins { get; set; }

	[JsonProperty("minitspins")]
	public int MiniTSpins { get; set; }

	[JsonProperty("minitspinsingles")]
	public int MiniTSpinSingles { get; set; }

	[JsonProperty("tspinsingles")]
	public int TSpinSingles { get; set; }

	[JsonProperty("minitspindoubles")]
	public int MiniTSpinDoubles { get; set; }

	[JsonProperty("tspindoubles")]
	public int TSpinDoubles { get; set; }

	[JsonProperty("minitspintriples")]
	public int? MiniTSpinTriples { get; set; }

	[JsonProperty("tspintriples")]
	public int TSpinTriples { get; set; }

	[JsonProperty("minitspinquads")]
	public int? MiniTSpinQuads { get; set; }

	[JsonProperty("tspinquads")]
	public int TSpinQuads { get; set; }

	[JsonProperty("tspinpentas")]
	public int? TSpinPentas { get; set; }

	[JsonProperty("allclear")]
	public int AllClear { get; set; }
}


/// <summary>
/// Represents garbage statistics during a Blitz game.
/// </summary>
public class BlitzGarbage
{
	[JsonProperty("sent")]
	public int Sent { get; set; }

	[JsonProperty("sent_nomult")]
	public int? SentNoMulti { get; set; }

	[JsonProperty("maxspike")]
	public int? MaxSpike { get; set; }

	[JsonProperty("maxspike_nomult")]
	public int? MaxSpikeNoMulti { get; set; }

	[JsonProperty("received")]
	public int Received { get; set; }

	[JsonProperty("attack")]
	public int? Attack { get; set; }

	[JsonProperty("cleared")]
	public int? Cleared { get; set; }
}


/// <summary>
/// Represents finesse data for a Blitz game.
/// </summary>
public class BlitzFinesse
{
	[JsonProperty("combo")]
	public int Combo { get; set; }

	[JsonProperty("faults")]
	public int Faults { get; set; }

	[JsonProperty("perfectpieces")]
	public int PerfectPieces { get; set; }
}


/// <summary>
/// Represents Zenith-specific statistics for a Blitz game.
/// </summary>
public class BlitzZenith
{
	[JsonProperty("altitude")]
	public long Altitude { get; set; }

	[JsonProperty("rank")]
	public long Rank { get; set; }

	[JsonProperty("peakrank")]
	public long PeakRank { get; set; }

	[JsonProperty("avgrankpts")]
	public long AverageRankPoints { get; set; }

	[JsonProperty("floor")]
	public long Floor { get; set; }

	[JsonProperty("targetingfactor")]
	public long TargetingFactor { get; set; }

	[JsonProperty("targetinggrace")]
	public long TargetingGrace { get; set; }

	[JsonProperty("totalbonus")]
	public long TotalBonus { get; set; }

	[JsonProperty("revives")]
	public long Revives { get; set; }

	[JsonProperty("revivesTotal")]
	public long RevivesTotal { get; set; }

	[JsonProperty("speedrun")]
	public bool IsSpeedRun { get; set; }

	[JsonProperty("speedrun_seen")]
	public bool WasSpeedRunSeen { get; set; }

	[JsonProperty("splits")]
	public List<long> Splits { get; set; }
}


/// <summary>
/// Represents the overall statistics for a Blitz game.
/// </summary>
public class BlitzStats
{
	[JsonProperty("clears")]
	public BlitzClears Clears { get; set; }

	[JsonProperty("garbage")]
	public BlitzGarbage Garbage { get; set; }

	[JsonProperty("finesse")]
	[CanBeNull]
	public BlitzFinesse Finesse { get; set; }

	[JsonProperty("zenith")]
	[CanBeNull]
	public BlitzZenith Zenith { get; set; }
}


/// <summary>
/// Represents the results of a Blitz game.
/// </summary>
public class BlitzResults
{
	/// <summary>
	/// Aggregate stats of the game played.
	/// </summary>
	[JsonProperty("aggregatestats")]
	public BlitzAggregateStats AggregateStats { get; set; }

	/// <summary>
	/// The final stats of the game played.
	/// </summary>
	[JsonProperty("stats")]
	public BlitzStats Stats { get; set; }

	/// <summary>
	/// The reason the game has ended.
	/// </summary>
	[JsonProperty("gameoverreason")]
	public string GameOverReason { get; set; }
}


/// <summary>
/// Represents a user in a Blitz game record.
/// </summary>
public class BlitzRecordUser
{
	/// <summary>
	/// The player's User ID.
	/// </summary>
	[JsonProperty("id")]
	public string Id { get; set; }

	/// <summary>
	/// The player's username.
	/// </summary>
	[JsonProperty("username")]
	public string Username { get; set; }
}

public abstract class PersonalUserRecord
{

}

/// <summary>
/// Represents a Blitz game record.
/// </summary>
public class BlitzRecord : PersonalUserRecord
{
	[JsonProperty("_id")]
	public string Id { get; set; }

	[JsonProperty("replayid")]
	public string ReplayId { get; set; }
}


/// <summary>
/// Represents a packet containing personal blitz records.
/// </summary>
public class PersonalBlitzRecordResponse : TetrioApiResponse<PersonalUserRecord<BlitzRecord>>;