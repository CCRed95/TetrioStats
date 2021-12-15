using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Replays
{
	public class TetrioMatchReplay
	{
		[JsonProperty("ismulti")]
		public bool IsMultiPlayer { get; set; }

		[JsonProperty("data")]
		public List<Datum> Data { get; set; }

		[JsonProperty("endcontext")]
		public List<EndContext> EndContext { get; set; }

		[JsonProperty("ts")]
		public DateTimeOffset TimeStamp { get; set; }
	}

	public class Datum
	{
		[JsonProperty("board")]
		public List<Board> Board { get; set; }

		[JsonProperty("replays")]
		public List<Replay> Replays { get; set; }
	}

	public class Board
	{
		[JsonProperty("user")]
		public UserInfo User { get; set; }

		[JsonProperty("active")]
		public bool IsActive { get; set; }

		[JsonProperty("success")]
		public bool IsSuccess { get; set; }

		[JsonProperty("winning")]
		public long Winning { get; set; }
	}

	public class UserInfo
	{
		[JsonProperty("_id")]
		public string Id { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }
	}

	public class Replay
	{
		[JsonProperty("frames")]
		public long TotalFrames { get; set; }

		[JsonProperty("events")]
		public List<Event> Events { get; set; }
	}

	public class Event
	{
		[JsonProperty("frame")]
		public long Frame { get; set; }

		[JsonProperty("type")]
		public EventKind Type { get; set; }

		[JsonProperty("data")]
		public EventData Data { get; set; }
	}

	public class EventData
	{
		[JsonProperty("successful", NullValueHandling = NullValueHandling.Ignore)]
		public bool? Successful { get; set; }

		[JsonProperty("gameoverreason")]
		public object GameOverReason { get; set; }

		[JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
		public Options Options { get; set; }

		[JsonProperty("stats", NullValueHandling = NullValueHandling.Ignore)]
		public Stats Stats { get; set; }

		[JsonProperty("targets", NullValueHandling = NullValueHandling.Ignore)]
		public List<object> Targets { get; set; }
		
		[JsonProperty("fire", NullValueHandling = NullValueHandling.Ignore)]
		public long? Fire { get; set; }

		[JsonProperty("game", NullValueHandling = NullValueHandling.Ignore)]
		public GameData Game { get; set; }

		[JsonProperty("killer", NullValueHandling = NullValueHandling.Ignore)]
		public Killer Killer { get; set; }
		
		[JsonProperty("aggregatestats", NullValueHandling = NullValueHandling.Ignore)]
		public Aggregatestats AggregateStats { get; set; }

		[JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
		public Id? Id { get; set; }

		[JsonProperty("frame", NullValueHandling = NullValueHandling.Ignore)]
		public long? Frame { get; set; }

		[JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
		public EventKind? Type { get; set; }

		[JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
		public DataUnion? Data { get; set; }

		[JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
		public MoveKind? Key { get; set; }

		[JsonProperty("hoisted", NullValueHandling = NullValueHandling.Ignore)]
		public bool? Hoisted { get; set; }

		[JsonProperty("subframe", NullValueHandling = NullValueHandling.Ignore)]
		public double? SubFrame { get; set; }

		[JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
		public string Reason { get; set; }

		[JsonProperty("export", NullValueHandling = NullValueHandling.Ignore)]
		public Export Export { get; set; }
	}

	public class Aggregatestats
	{
		[JsonProperty("apm")]
		public double APM { get; set; }

		[JsonProperty("pps")]
		public double PPS { get; set; }

		[JsonProperty("vsscore")]
		public double VSScore { get; set; }
	}

	public class DataDataClass
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("victim", NullValueHandling = NullValueHandling.Ignore)]
		public string Victim { get; set; }

		[JsonProperty("killer", NullValueHandling = NullValueHandling.Ignore)]
		public Killer Killer { get; set; }

		[JsonProperty("fire", NullValueHandling = NullValueHandling.Ignore)]
		public long? Fire { get; set; }

		[JsonProperty("lines", NullValueHandling = NullValueHandling.Ignore)]
		public long? Lines { get; set; }

		[JsonProperty("column", NullValueHandling = NullValueHandling.Ignore)]
		public long? Column { get; set; }

		[JsonProperty("sender", NullValueHandling = NullValueHandling.Ignore)]
		public string Sender { get; set; }

		[JsonProperty("sent_frame", NullValueHandling = NullValueHandling.Ignore)]
		public long? SentFrame { get; set; }
	}

	public class Killer
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }
	}

	public class Export
	{
		[JsonProperty("successful")]
		public bool Successful { get; set; }

		[JsonProperty("gameoverreason")]
		public string GameOverReason { get; set; }
		
		[JsonProperty("options")]
		public Options Options { get; set; }

		[JsonProperty("stats")]
		public Stats Stats { get; set; }

		[JsonProperty("targets")]
		public List<string> Targets { get; set; }

		[JsonProperty("fire")]
		public long Fire { get; set; }

		[JsonProperty("game")]
		public ExportGame Game { get; set; }

		[JsonProperty("killer")]
		public Killer Killer { get; set; }

		[JsonProperty("aggregatestats")]
		public Aggregatestats AggregateStats { get; set; }
	}

	public class ExportGame
	{
		[JsonProperty("board")]
		public List<List<MinoKind?>> Board { get; set; }

		[JsonProperty("bag")]
		public List<MinoKind> Bag { get; set; }

		[JsonProperty("hold")]
		public Hold Hold { get; set; }

		[JsonProperty("g")]
		public double Gravity { get; set; }

		[JsonProperty("controlling")]
		public ControllingConfig ControllingConfig { get; set; }

		[JsonProperty("handling")]
		public Handling Handling { get; set; }

		[JsonProperty("playing")]
		public bool IsPlaying { get; set; }
	}

	public class ControllingConfig
	{
		[JsonProperty("ldas")]
		public long LDas { get; set; }

		[JsonProperty("ldasiter")]
		public double LDasiter { get; set; }

		[JsonProperty("lshift")]
		public bool LShift { get; set; }

		[JsonProperty("rdas")]
		public double RDas { get; set; }

		[JsonProperty("rdasiter")]
		public double RDasiter { get; set; }

		[JsonProperty("rshift")]
		public bool RShift { get; set; }

		[JsonProperty("lastshift")]
		public long LastShift { get; set; }

		[JsonProperty("softdrop")]
		public bool SoftDrop { get; set; }
	}

	public class Handling
	{
		[JsonProperty("arr")]
		public long ARR { get; set; }

		[JsonProperty("das")]
		public long DAS { get; set; }

		[JsonProperty("dcd")]
		public long DCD { get; set; }

		[JsonProperty("sdf")]
		public long SDF { get; set; }

		[JsonProperty("safelock")]
		public bool SafeLock { get; set; }

		[JsonProperty("cancel")]
		public bool Cancel { get; set; }
	}

	public class Hold
	{
		[JsonProperty("piece")]
		public MinoKind? Piece { get; set; }

		[JsonProperty("locked")]
		public bool Locked { get; set; }
	}

	public class Options
	{
		[JsonProperty("version")]
		public long Version { get; set; }

		[JsonProperty("seed_random")]
		public bool SeedRandom { get; set; }

		[JsonProperty("seed")]
		public long Seed { get; set; }

		[JsonProperty("g")]
		public double Gravity { get; set; }

		[JsonProperty("stock")]
		public long Stock { get; set; }

		[JsonProperty("countdown")]
		public bool Countdown { get; set; }

		[JsonProperty("countdown_count")]
		public long CountdownCount { get; set; }

		[JsonProperty("countdown_interval")]
		public long CountdownInterval { get; set; }

		[JsonProperty("precountdown")]
		public long PreCountdown { get; set; }

		[JsonProperty("prestart")]
		public long PreStart { get; set; }

		[JsonProperty("mission")]
		public string Mission { get; set; }

		[JsonProperty("mission_type")]
		public string MissionType { get; set; }

		[JsonProperty("zoominto")]
		public string ZoomInto { get; set; }

		[JsonProperty("display_lines")]
		public bool DisplayLines { get; set; }

		[JsonProperty("display_attack")]
		public bool DisplayAttack { get; set; }

		[JsonProperty("display_pieces")]
		public bool DisplayPieces { get; set; }

		[JsonProperty("display_impending")]
		public bool DisplayImpending { get; set; }

		[JsonProperty("display_kills")]
		public bool DisplayKills { get; set; }

		[JsonProperty("display_placement")]
		public bool DisplayPlacement { get; set; }

		[JsonProperty("display_fire")]
		public bool DisplayFire { get; set; }

		[JsonProperty("display_username")]
		public bool DisplayUsername { get; set; }

		[JsonProperty("hasgarbage")]
		public bool HasGarbage { get; set; }

		[JsonProperty("neverstopbgm")]
		public bool NeverStopBGM { get; set; }

		[JsonProperty("display_next")]
		public bool DisplayNext { get; set; }

		[JsonProperty("display_hold")]
		public bool DisplayHold { get; set; }

		[JsonProperty("gmargin")]
		public long GravityMargin { get; set; }

		[JsonProperty("gincrease")]
		public double GravityIncrease { get; set; }

		[JsonProperty("garbagemultiplier")]
		public long GarbageMultiplier { get; set; }

		[JsonProperty("garbagemargin")]
		public long GarbageMargin { get; set; }

		[JsonProperty("garbageincrease")]
		public double GarbageIncrease { get; set; }

		[JsonProperty("garbagecap")]
		public long GarbageCap { get; set; }

		[JsonProperty("garbagecapincrease")]
		public long GarbageCapIncrease { get; set; }

		[JsonProperty("garbagecapmax")]
		public long GarbageCapMax { get; set; }

		[JsonProperty("bagtype")]
		public string BagType { get; set; }

		[JsonProperty("spinbonuses")]
		public string SpinBonuses { get; set; }

		[JsonProperty("kickset")]
		public string KickSet { get; set; }

		[JsonProperty("nextcount")]
		public long NextCount { get; set; }

		[JsonProperty("allow_harddrop")]
		public bool AllowHardDrop { get; set; }

		[JsonProperty("display_shadow")]
		public bool DisplayShadow { get; set; }

		[JsonProperty("locktime")]
		public long LockTime { get; set; }

		[JsonProperty("garbagespeed")]
		public long GarbageSpeed { get; set; }

		[JsonProperty("forfeit_time")]
		public long ForfeitTime { get; set; }

		[JsonProperty("are")]
		public long EntryDelay { get; set; }

		[JsonProperty("lineclear_are")]
		public long LineClearAre { get; set; }

		[JsonProperty("infinitemovement")]
		public bool InfiniteMovement { get; set; }

		[JsonProperty("lockresets")]
		public long LockResets { get; set; }

		[JsonProperty("allow180")]
		public bool Allow180 { get; set; }

		[JsonProperty("objective")]
		public Objective Objective { get; set; }

		[JsonProperty("room_handling")]
		public bool RoomHandling { get; set; }

		[JsonProperty("room_handling_arr")]
		public long RoomHandlingArr { get; set; }

		[JsonProperty("room_handling_das")]
		public long RoomHandlingDas { get; set; }

		[JsonProperty("room_handling_sdf")]
		public long RoomHandlingSdf { get; set; }

		[JsonProperty("manual_allowed")]
		public bool ManualAllowed { get; set; }

		[JsonProperty("b2bchaining")]
		public bool B2BChaining { get; set; }

		[JsonProperty("clutch")]
		public bool Clutch { get; set; }

		[JsonProperty("display_stopwatch")]
		public bool DisplayStopwatch { get; set; }

		[JsonProperty("display_vs")]
		public bool DisplayVS { get; set; }

		[JsonProperty("fulloffset", NullValueHandling = NullValueHandling.Ignore)]
		public long? FullOffset { get; set; }

		[JsonProperty("fullinterval", NullValueHandling = NullValueHandling.Ignore)]
		public long? FullInterval { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("physical")]
		public bool Physical { get; set; }

		[JsonProperty("handling", NullValueHandling = NullValueHandling.Ignore)]
		public Handling Handling { get; set; }

		[JsonProperty("noscope", NullValueHandling = NullValueHandling.Ignore)]
		public bool? NoScope { get; set; }
	}

	public class Objective
	{
		[JsonProperty("type")]
		public string Type { get; set; }
	}

	public class Stats
	{
		[JsonProperty("seed")]
		public long Seed { get; set; }

		[JsonProperty("lines")]
		public long Lines { get; set; }

		[JsonProperty("level_lines")]
		public long LevelLines { get; set; }

		[JsonProperty("level_lines_needed")]
		public long LevelLinesNeeded { get; set; }

		[JsonProperty("inputs")]
		public long Inputs { get; set; }

		[JsonProperty("time")]
		public Time Time { get; set; }

		[JsonProperty("score")]
		public long Score { get; set; }

		[JsonProperty("zenlevel")]
		public long ZenLevel { get; set; }

		[JsonProperty("zenprogress")]
		public long ZenProgress { get; set; }

		[JsonProperty("level")]
		public long Level { get; set; }

		[JsonProperty("combo")]
		public long Combo { get; set; }

		[JsonProperty("currentcombopower")]
		public long CurrentComboPower { get; set; }

		[JsonProperty("topcombo")]
		public long TopCombo { get; set; }

		[JsonProperty("btb")]
		public long BTB { get; set; }

		[JsonProperty("topbtb")]
		public long TopBTB { get; set; }

		[JsonProperty("tspins")]
		public long TSpins { get; set; }

		[JsonProperty("piecesplaced")]
		public long PiecesPlaced { get; set; }

		[JsonProperty("clears")]
		public Dictionary<string, long> Clears { get; set; }

		[JsonProperty("garbage")]
		public Garbage Garbage { get; set; }

		[JsonProperty("kills")]
		public long Kills { get; set; }

		[JsonProperty("finesse")]
		public Finesse Finesse { get; set; }
	}

	public class Finesse
	{
		[JsonProperty("combo")]
		public long Combo { get; set; }

		[JsonProperty("faults")]
		public long Faults { get; set; }

		[JsonProperty("perfectpieces")]
		public long PerfectPieces { get; set; }
	}

	public class Garbage
	{
		[JsonProperty("sent")]
		public long Sent { get; set; }

		[JsonProperty("received")]
		public long Received { get; set; }

		[JsonProperty("attack")]
		public long Attack { get; set; }

		[JsonProperty("cleared")]
		public long Cleared { get; set; }
	}

	public class Time
	{
		[JsonProperty("start")]
		public double Start { get; set; }

		[JsonProperty("zero")]
		public bool IsZero { get; set; }

		[JsonProperty("locked")]
		public bool IsLocked { get; set; }

		[JsonProperty("prev")]
		public long Prev { get; set; }

		[JsonProperty("frameoffset")]
		public long FrameOffset { get; set; }
	}

	public class GameData
	{
		[JsonProperty("board")]
		public List<List<object>> Board { get; set; }

		[JsonProperty("bag")]
		public List<MinoKind> Bag { get; set; }

		[JsonProperty("hold")]
		public Hold Hold { get; set; }

		[JsonProperty("g")]
		public double G { get; set; }

		[JsonProperty("controlling")]
		public ControllingConfig ControllingConfig { get; set; }

		[JsonProperty("handling")]
		public Handling Handling { get; set; }

		[JsonProperty("playing")]
		public bool Playing { get; set; }
	}

	public class EndContext
	{
		[JsonProperty("naturalorder")]
		public long NaturalOrder { get; set; }

		[JsonProperty("user")]
		public UserInfo User { get; set; }

		[JsonProperty("active")]
		public bool Active { get; set; }

		[JsonProperty("wins")]
		public long Wins { get; set; }

		[JsonProperty("points")]
		public PointsInfo Points { get; set; }

		[JsonProperty("inputs")]
		public long Inputs { get; set; }

		[JsonProperty("piecesplaced")]
		public long PiecesPlaced { get; set; }
	}

	public class PointsInfo
	{
		[JsonProperty("primary")]
		public long Primary { get; set; }

		[JsonProperty("secondary")]
		public long Secondary { get; set; }

		[JsonProperty("tertiary")]
		public long Tertiary { get; set; }
	}

	public enum MinoKind
	{
		Garbage,
		I,
		J,
		L,
		O,
		S,
		T,
		Z
	};

	public enum MoveKind
	{
		HardDrop,
		Hold,
		MoveLeft,
		MoveRight,
		RotateCCW,
		RotateCW,
		SoftDrop
	};

	public enum EventKind
	{
		End,
		Full,
		InGameEvent,
		KeyDown,
		KeyUp,
		Start,
		Targets
	};

	public struct DataUnion
	{
		public DataDataClass DataDataClass;

		public List<string> StringArray;


		public static implicit operator DataUnion(
			DataDataClass DataDataClass)
		{
			return new DataUnion
			{
				DataDataClass = DataDataClass
			};
		}

		public static implicit operator DataUnion(
			List<string> StringArray)
		{
			return new DataUnion
			{
				StringArray = StringArray
			};
		}
	}

	public struct Id
	{
		public long? Integer;

		public string String;


		public static implicit operator Id(
			long Integer)
		{
			return new Id
			{
				Integer = Integer
			};
		}

		public static implicit operator Id(string String)
		{
			return new Id
			{
				String = String
			};
		}
	}

	internal static class Converter
	{
		public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
			DateParseHandling = DateParseHandling.None,
			Converters =
			{
				DataUnionConverter.Singleton,
				BagConverter.Singleton,
				IdConverter.Singleton,
				KeyConverter.Singleton,
				TypeEnumConverter.Singleton,
				new IsoDateTimeConverter
				{
					DateTimeStyles = DateTimeStyles.AssumeUniversal
				}
			},
		};
	}

	internal class DataUnionConverter
		: JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(DataUnion) || t == typeof(DataUnion?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			switch (reader.TokenType)
			{
				case JsonToken.StartObject:
					var objectValue = serializer.Deserialize<DataDataClass>(reader);
					return new DataUnion { DataDataClass = objectValue };
				case JsonToken.StartArray:
					var arrayValue = serializer.Deserialize<List<string>>(reader);
					return new DataUnion { StringArray = arrayValue };
			}
			throw new Exception("Cannot unmarshal type DataUnion");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			var value = (DataUnion)untypedValue;
			if (value.StringArray != null)
			{
				serializer.Serialize(writer, value.StringArray);
				return;
			}
			if (value.DataDataClass != null)
			{
				serializer.Serialize(writer, value.DataDataClass);
				return;
			}
			throw new Exception("Cannot marshal type DataUnion");
		}

		public static readonly DataUnionConverter Singleton = new DataUnionConverter();
	}

	internal class BagConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(MinoKind) || t == typeof(MinoKind?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			switch (value)
			{
				case "gb":
					return MinoKind.Garbage;
				case "i":
					return MinoKind.I;
				case "j":
					return MinoKind.J;
				case "l":
					return MinoKind.L;
				case "o":
					return MinoKind.O;
				case "s":
					return MinoKind.S;
				case "t":
					return MinoKind.T;
				case "z":
					return MinoKind.Z;
			}
			throw new Exception("Cannot unmarshal type MinoKind");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (MinoKind)untypedValue;
			switch (value)
			{
				case MinoKind.Garbage:
					serializer.Serialize(writer, "gb");
					return;
				case MinoKind.I:
					serializer.Serialize(writer, "i");
					return;
				case MinoKind.J:
					serializer.Serialize(writer, "j");
					return;
				case MinoKind.L:
					serializer.Serialize(writer, "l");
					return;
				case MinoKind.O:
					serializer.Serialize(writer, "o");
					return;
				case MinoKind.S:
					serializer.Serialize(writer, "s");
					return;
				case MinoKind.T:
					serializer.Serialize(writer, "t");
					return;
				case MinoKind.Z:
					serializer.Serialize(writer, "z");
					return;
			}
			throw new Exception("Cannot marshal type MinoKind");
		}

		public static readonly BagConverter Singleton = new BagConverter();
	}

	internal class IdConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(Id) || t == typeof(Id?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			switch (reader.TokenType)
			{
				case JsonToken.Integer:
					var integerValue = serializer.Deserialize<long>(reader);
					return new Id { Integer = integerValue };
				case JsonToken.String:
				case JsonToken.Date:
					var stringValue = serializer.Deserialize<string>(reader);
					return new Id { String = stringValue };
			}
			throw new Exception("Cannot unmarshal type ID");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			var value = (Id)untypedValue;
			if (value.Integer != null)
			{
				serializer.Serialize(writer, value.Integer.Value);
				return;
			}
			if (value.String != null)
			{
				serializer.Serialize(writer, value.String);
				return;
			}
			throw new Exception("Cannot marshal type ID");
		}

		public static readonly IdConverter Singleton = new IdConverter();
	}

	internal class KeyConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(MoveKind) || t == typeof(MoveKind?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			switch (value)
			{
				case "hardDrop":
					return MoveKind.HardDrop;
				case "hold":
					return MoveKind.Hold;
				case "moveLeft":
					return MoveKind.MoveLeft;
				case "moveRight":
					return MoveKind.MoveRight;
				case "rotateCCW":
					return MoveKind.RotateCCW;
				case "rotateCW":
					return MoveKind.RotateCW;
				case "softDrop":
					return MoveKind.SoftDrop;
			}
			throw new Exception("Cannot unmarshal type MoveKind");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (MoveKind)untypedValue;
			switch (value)
			{
				case MoveKind.HardDrop:
					serializer.Serialize(writer, "hardDrop");
					return;
				case MoveKind.Hold:
					serializer.Serialize(writer, "hold");
					return;
				case MoveKind.MoveLeft:
					serializer.Serialize(writer, "moveLeft");
					return;
				case MoveKind.MoveRight:
					serializer.Serialize(writer, "moveRight");
					return;
				case MoveKind.RotateCCW:
					serializer.Serialize(writer, "rotateCCW");
					return;
				case MoveKind.RotateCW:
					serializer.Serialize(writer, "rotateCW");
					return;
				case MoveKind.SoftDrop:
					serializer.Serialize(writer, "softDrop");
					return;
			}
			throw new Exception("Cannot marshal type MoveKind");
		}

		public static readonly KeyConverter Singleton = new KeyConverter();
	}

	internal class TypeEnumConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(EventKind) || t == typeof(EventKind?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			switch (value)
			{
				case "end":
					return EventKind.End;
				case "full":
					return EventKind.Full;
				case "ige":
					return EventKind.InGameEvent;
				case "keydown":
					return EventKind.KeyDown;
				case "keyup":
					return EventKind.KeyUp;
				case "start":
					return EventKind.Start;
				case "targets":
					return EventKind.Targets;
			}
			throw new Exception("Cannot unmarshal type EventKind");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (EventKind)untypedValue;
			switch (value)
			{
				case EventKind.End:
					serializer.Serialize(writer, "end");
					return;
				case EventKind.Full:
					serializer.Serialize(writer, "full");
					return;
				case EventKind.InGameEvent:
					serializer.Serialize(writer, "ige");
					return;
				case EventKind.KeyDown:
					serializer.Serialize(writer, "keydown");
					return;
				case EventKind.KeyUp:
					serializer.Serialize(writer, "keyup");
					return;
				case EventKind.Start:
					serializer.Serialize(writer, "start");
					return;
				case EventKind.Targets:
					serializer.Serialize(writer, "targets");
					return;
			}
			throw new Exception("Cannot marshal type EventKind");
		}

		public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
	}
}
