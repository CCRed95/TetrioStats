using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Domain.Users.UserRecords;

/// <summary>
/// Represents aggregate statistics for a Zenith game.
/// </summary>
public class ZenithAggregateStats
{
	[JsonProperty("apm")]
	public float APM { get; set; }

	[JsonProperty("pps")]
	public float PPS { get; set; }

	[JsonProperty("vsscore")]
	public float VSScore { get; set; }
}


/// <summary>
/// Represents time-related data for a Zenith game.
/// </summary>
public class ZenithTime
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
/// Represents line clears achieved during a Zenith game.
/// </summary>
public class ZenithClears
{
	[JsonProperty("singles")]
	public short Singles { get; set; }

	[JsonProperty("doubles")]
	public short Doubles { get; set; }

	[JsonProperty("triples")]
	public short Triples { get; set; }

	[JsonProperty("quads")]
	public short Quads { get; set; }

	[JsonProperty("pentas")]
	public short? Pentas { get; set; }

	[JsonProperty("realtspins")]
	public short RealTSpins { get; set; }

	[JsonProperty("minitspins")]
	public short MiniTSpins { get; set; }

	[JsonProperty("minitspinsingles")]
	public short MiniTSpinSingles { get; set; }

	[JsonProperty("tspinsingles")]
	public short TSpinSingles { get; set; }

	[JsonProperty("minitspindoubles")]
	public short MiniTSpinDoubles { get; set; }

	[JsonProperty("tspindoubles")]
	public short TSpinDoubles { get; set; }

	[JsonProperty("minitspintriples")]
	public short? MiniTSpinTriples { get; set; }

	[JsonProperty("tspintriples")]
	public short TSpinTriples { get; set; }

	[JsonProperty("minitspinquads")]
	public short? MiniTSpinQuads { get; set; }

	[JsonProperty("tspinquads")]
	public short TSpinQuads { get; set; }

	[JsonProperty("tspinpentas")]
	public short? TSpinPentas { get; set; }

	[JsonProperty("allclear")]
	public short AllClear { get; set; }
}


/// <summary>
/// Represents garbage-related statistics for a Zenith game.
/// </summary>
public class ZenithGarbage
{
	[JsonProperty("sent")]
	public short Sent { get; set; }

	[JsonProperty("sent_nomult")]
	public short? SentNoMulti { get; set; }

	[JsonProperty("maxspike")]
	public short? MaxSpike { get; set; }

	[JsonProperty("maxspike_nomult")]
	public short? MaxSpikeNoMulti { get; set; }

	[JsonProperty("received")]
	public short Received { get; set; }

	[JsonProperty("attack")]
	public short? Attack { get; set; }

	[JsonProperty("cleared")]
	public short? Cleared { get; set; }
}


/// <summary>
/// Represents finesse-related statistics for a Zenith game.
/// </summary>
public class ZenithFinesse
{
	[JsonProperty("combo")]
	public short Combo { get; set; }

	[JsonProperty("faults")]
	public short Faults { get; set; }

	[JsonProperty("perfectpieces")]
	public short PerfectPieces { get; set; }
}


/// <summary>
/// Represents Zenith-specific statistics for a Zenith game.
/// </summary>
public class ZenithZenith
{
	[JsonProperty("altitude")]
	public float Altitude { get; set; }

	[JsonProperty("rank")]
	public float Rank { get; set; }

	[JsonProperty("peakrank")]
	public float PeakRank { get; set; }

	[JsonProperty("avgrankpts")]
	public float AverageRankPoints { get; set; }

	[JsonProperty("floor")]
	public long Floor { get; set; }

	[JsonProperty("targetingfactor")]
	public float TargetingFactor { get; set; }

	[JsonProperty("targetinggrace")]
	public float TargetingGrace { get; set; }

	[JsonProperty("totalbonus")]
	public float TotalBonus { get; set; }

	[JsonProperty("revives")]
	public long Revives { get; set; }

	[JsonProperty("revivesTotal")]
	public long RevivesTotal { get; set; }

	[JsonProperty("speedrun")]
	public bool SpeedRun { get; set; }

	[JsonProperty("speedrun_seen")]
	public bool SpeedRunSeen { get; set; }

	[JsonProperty("splits")]
	public List<long> Splits { get; set; }
}


/// <summary>
/// Represents the overall statistics for a Zenith game.
/// </summary>
public class ZenithStats
{
	[JsonProperty("clears")]
	public ZenithClears Clears { get; set; }

	[JsonProperty("garbage")]
	public ZenithGarbage Garbage { get; set; }

	[JsonProperty("finesse")]
	[CanBeNull]
	public ZenithFinesse Finesse { get; set; }

	[JsonProperty("zenith")]
	[CanBeNull]
	public ZenithZenith Zenith { get; set; }
}


/// <summary>
/// Represents the results of a Zenith game.
/// </summary>
public class ZenithResults
{
	[JsonProperty("aggregatestats")]
	public ZenithAggregateStats AggregateStats { get; set; }

	[JsonProperty("stats")]
	public ZenithStats Stats { get; set; }

	[JsonProperty("gameoverreason")]
	public string GameOverReason { get; set; }
}


/// <summary>
/// Represents a user associated with a Zenith record.
/// </summary>
public class ZenithRecordUser
{
	[JsonProperty("id")]
	public string Id { get; set; }

	[JsonProperty("username")]
	public string Username { get; set; }
}


/// <summary>
/// Represents extras specific to Zenith.
/// </summary>
public class ZenithExtras
{
	[JsonProperty("zenith")]
	public ZenithZenith Zenith { get; set; }
}


/// <summary>
/// Represents a record for a Zenith game.
/// </summary>
public class ZenithRecord : PersonalUserRecord
{
	[JsonProperty("_id")]
	public string Id { get; set; }

	[JsonProperty("results")]
	public ZenithResults Results { get; set; }
}


/// <summary>
/// Represents a packet containing personal Zenith records.
/// </summary>
public class PersonalZenithRecordResponse : TetrioApiResponse<PersonalUserRecord<ZenithRecord>>;