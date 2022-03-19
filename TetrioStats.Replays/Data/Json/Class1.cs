//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;
//using TetrioStats.Replays.Ttrm;

//// ReSharper disable StringLiteralTypo

//namespace TetrioStats.Replays.Data.Json
//{
//	public class TetrioMatchReplay
//		: ITtrmFileStatistics
//  {
//	  [JsonProperty("_id")]
//    public string Id { get; set; }

//    string ITtrmFileStatistics.ReplayID => null;

//    [JsonProperty("shortid")]
//    public string ShortID { get; set; }

//    [JsonProperty("ismulti")]
//    public bool IsMultiPlayer { get; set; }

//    [JsonProperty("endcontext")]
//    public List<EndContext> EndContext { get; set; }

//    [JsonProperty("ts")]
//    public DateTimeOffset TimeStamp { get; set; }

//    IUserData ITtrmFileStatistics.LeftUser => new UserInfo
//    UserID = Rounds[0].Opponents[0].User.UserID,
//      Username = Rounds[0].Opponents[0].User.Username,
//    };

//    IUserData ITtrmFileStatistics.RightUser => new UserData
//    {
//	    UserID = Rounds[0].Opponents[1].User.UserID,
//	    Username = Rounds[0].Opponents[1].User.Username,
//    };

//    IReplayGameStats ITtrmFileStatistics.OverallStats => new ReplayGameStats
//    {

//    };

//    IList<IReplayGameStats> ITtrmFileStatistics.Replays
//    {
//	    get => null;
//	   // get => Rounds.Select(t => t.Replays.
//	    // .Replays.)
//    }

//    [JsonProperty("gametype")]
//    public string GameType { get; set; }

//    [JsonProperty("verified")]
//    public bool IsVerified { get; set; }

//    [JsonProperty("data")]
//    public List<RoundData> Rounds { get; set; }

//    [JsonProperty("back")]
//    public string Back { get; set; }


//    public static TetrioMatchReplay ParseFromFile(
//	    string filePath)
//    {
//	    var fileContent = File.ReadAllText(filePath);

//	    var tetrioMatchReplay = JsonConvert
//		    .DeserializeObject<TetrioMatchReplay>(
//			    fileContent, Converter.Settings);

//	    return tetrioMatchReplay;
//    }
//  }
  
//  public class RoundData
//  {
//    [JsonProperty("board")]
//    public List<OpponentStanding> Opponents { get; set; }

//    [JsonProperty("replays")]
//    public List<RoundReplayData> Replays { get; set; }
//  }

//  public class OpponentStanding
//  {
//    [JsonProperty("user")]
//    public User User { get; set; }

//    [JsonProperty("active")]
//    public bool Active { get; set; }

//    [JsonProperty("success")]
//    public bool Success { get; set; }

//    [JsonProperty("winning")]
//    public int Winning { get; set; }
//  }

//  public class User
//  {
//    [JsonProperty("_id")]
//    public string UserID { get; set; }

//    [JsonProperty("username")]
//    public string Username { get; set; }
//  }

//  public class RoundReplayData
//  {
//    [JsonProperty("frames")]
//    public long Frames { get; set; }

//    [JsonProperty("events")]
//    public List<Event> Events { get; set; }
//  }

//  public class Event
//  {
//    [JsonProperty("frame")]
//    public long Frame { get; set; }

//    [JsonProperty("type")]
//    public EventType Type { get; set; }

//    [JsonProperty("data")]
//    public EventData Data { get; set; }
//  }

//  public class EventData
//  {
//    [JsonProperty("successful", NullValueHandling = NullValueHandling.Ignore)]
//    public bool? Successful { get; set; }

//    [JsonProperty("gameoverreason")]
//    public object GameOverReason { get; set; }

//    [JsonProperty("replay", NullValueHandling = NullValueHandling.Ignore)]
//    public ExportReplay Replay { get; set; }

//    [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
//    public Source Source { get; set; }

//    [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
//    public Options Options { get; set; }

//    [JsonProperty("stats", NullValueHandling = NullValueHandling.Ignore)]
//    public Stats Stats { get; set; }

//    [JsonProperty("targets", NullValueHandling = NullValueHandling.Ignore)]
//    public List<object> Targets { get; set; }

//    [JsonProperty("fire", NullValueHandling = NullValueHandling.Ignore)]
//    public long? Fire { get; set; }

//    [JsonProperty("game", NullValueHandling = NullValueHandling.Ignore)]
//    public DataGame Game { get; set; }

//    [JsonProperty("killer", NullValueHandling = NullValueHandling.Ignore)]
//    public Killer Killer { get; set; }

//    [JsonProperty("aggregatestats", NullValueHandling = NullValueHandling.Ignore)]
//    public AggregateStats AggregateStats { get; set; }

//    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
//    public IdUnion? Id { get; set; }

//    [JsonProperty("frame", NullValueHandling = NullValueHandling.Ignore)]
//    public long? Frame { get; set; }

//    [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
//    public EventType? Type { get; set; }

//    [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
//    public DataUnion? Data { get; set; }

//    [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
//    public Key? Key { get; set; }

//    [JsonProperty("subframe", NullValueHandling = NullValueHandling.Ignore)]
//    public double? SubFrame { get; set; }

//    [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
//    public string Reason { get; set; }

//    [JsonProperty("export", NullValueHandling = NullValueHandling.Ignore)]
//    public Export Export { get; set; }

//    [JsonProperty("hoisted", NullValueHandling = NullValueHandling.Ignore)]
//    public bool? Hoisted { get; set; }
//  }

//  public class AggregateStats
//  {
//    [JsonProperty("apm")]
//    public double APM { get; set; }

//    [JsonProperty("pps")]
//    public double PPS { get; set; }

//    [JsonProperty("vsscore")]
//    public double VSScore { get; set; }
//  }

//  public class PurpleData
//  {
//    [JsonProperty("type")]
//    public InteractionKind Type { get; set; }

//    [JsonProperty("data")]
//    public FluffyData Data { get; set; }

//    [JsonProperty("sender")]
//    public string Sender { get; set; }

//    [JsonProperty("sent_frame")]
//    public long SentFrame { get; set; }

//    [JsonProperty("cid")]
//    public long Cid { get; set; }
//  }

//  public class FluffyData
//  {
//    [JsonProperty("type")]
//    public PurpleType Type { get; set; }

//    [JsonProperty("amt")]
//    public long Amt { get; set; }

//    [JsonProperty("x")]
//    public long X { get; set; }

//    [JsonProperty("y")]
//    public long Y { get; set; }

//    [JsonProperty("column")]
//    public long Column { get; set; }
//  }

//  public class Export
//  {
//    [JsonProperty("successful")]
//    public bool Successful { get; set; }

//    [JsonProperty("gameoverreason")]
//    public string GameOverReason { get; set; }

//    [JsonProperty("replay")]
//    public ExportReplay Replay { get; set; }

//    [JsonProperty("source")]
//    public Source Source { get; set; }

//    [JsonProperty("options")]
//    public Options Options { get; set; }

//    [JsonProperty("stats")]
//    public Stats Stats { get; set; }

//    [JsonProperty("targets")]
//    public List<string> Targets { get; set; }

//    [JsonProperty("fire")]
//    public long Fire { get; set; }

//    [JsonProperty("game")]
//    public ExportGame Game { get; set; }

//    [JsonProperty("killer")]
//    public Killer Killer { get; set; }

//    [JsonProperty("aggregatestats")]
//    public AggregateStats AggregateStats { get; set; }
//  }

//  public class ExportGame
//  {
//    [JsonProperty("board")]
//    public List<List<MinoKind?>> Board { get; set; }

//    [JsonProperty("bag")]
//    public List<MinoKind> Bag { get; set; }

//    [JsonProperty("hold")]
//    public Hold Hold { get; set; }

//    [JsonProperty("g")]
//    public double G { get; set; }

//    [JsonProperty("controlling")]
//    public Controlling Controlling { get; set; }

//    [JsonProperty("handling")]
//    public Handling Handling { get; set; }

//    [JsonProperty("playing")]
//    public bool Playing { get; set; }
//  }

//  public class Controlling
//  {
//    [JsonProperty("ldas")]
//    public double Ldas { get; set; }

//    [JsonProperty("ldasiter")]
//    public double Ldasiter { get; set; }

//    [JsonProperty("lshift")]
//    public bool Lshift { get; set; }

//    [JsonProperty("rdas")]
//    public double Rdas { get; set; }

//    [JsonProperty("rdasiter")]
//    public double Rdasiter { get; set; }

//    [JsonProperty("rshift")]
//    public bool Rshift { get; set; }

//    [JsonProperty("lastshift")]
//    public long Lastshift { get; set; }

//    [JsonProperty("softdrop")]
//    public bool Softdrop { get; set; }
//  }

//  public class Handling
//  {
//    [JsonProperty("arr")]
//    public long Arr { get; set; }

//    [JsonProperty("das")]
//    public double Das { get; set; }

//    [JsonProperty("dcd")]
//    public long Dcd { get; set; }

//    [JsonProperty("sdf")]
//    public long Sdf { get; set; }

//    [JsonProperty("safelock")]
//    public bool SafeLock { get; set; }

//    [JsonProperty("cancel")]
//    public bool Cancel { get; set; }
//  }

//  public class Hold
//  {
//    [JsonProperty("piece")]
//    public MinoKind? Piece { get; set; }

//    [JsonProperty("locked")]
//    public bool Locked { get; set; }
//  }

//  public class Killer
//  {
//    [JsonProperty("name")]
//    public string Username { get; set; }

//    [JsonProperty("type")]
//    public string Type { get; set; }
//  }

//  public class Options
//  {
//    [JsonProperty("version")]
//    public long Version { get; set; }

//    [JsonProperty("seed_random")]
//    public bool SeedRandom { get; set; }

//    [JsonProperty("seed")]
//    public long Seed { get; set; }

//    [JsonProperty("g")]
//    public double G { get; set; }

//    [JsonProperty("stock")]
//    public long Stock { get; set; }

//    [JsonProperty("countdown")]
//    public bool Countdown { get; set; }

//    [JsonProperty("countdown_count")]
//    public long CountdownCount { get; set; }

//    [JsonProperty("countdown_interval")]
//    public long CountdownInterval { get; set; }

//    [JsonProperty("precountdown")]
//    public long PreCountDown { get; set; }

//    [JsonProperty("prestart")]
//    public long PreStart { get; set; }

//    [JsonProperty("mission")]
//    public string Mission { get; set; }

//    [JsonProperty("mission_type")]
//    public string MissionType { get; set; }

//    [JsonProperty("zoominto")]
//    public string ZoomInto { get; set; }

//    [JsonProperty("slot_counter1")]
//    public string SlotCounter1 { get; set; }

//    [JsonProperty("slot_counter2")]
//    public string SlotCounter2 { get; set; }

//    [JsonProperty("slot_counter3")]
//    public string SlotCounter3 { get; set; }

//    [JsonProperty("slot_counter4")]
//    public object SlotCounter4 { get; set; }

//    [JsonProperty("slot_counter5")]
//    public string SlotCounter5 { get; set; }

//    [JsonProperty("slot_bar1")]
//    public string SlotBar1 { get; set; }

//    [JsonProperty("display_fire")]
//    public bool DisplayFire { get; set; }

//    [JsonProperty("display_username")]
//    public bool DisplayUsername { get; set; }

//    [JsonProperty("hasgarbage")]
//    public bool HasGarbage { get; set; }

//    [JsonProperty("neverstopbgm")]
//    public bool NeverStopBgm { get; set; }

//    [JsonProperty("display_next")]
//    public bool DisplayNext { get; set; }

//    [JsonProperty("display_hold")]
//    public bool DisplayHold { get; set; }

//    [JsonProperty("gmargin")]
//    public long GMargin { get; set; }

//    [JsonProperty("gincrease")]
//    public double GIncrease { get; set; }

//    [JsonProperty("garbagemultiplier")]
//    public long GarbageMultiplier { get; set; }

//    [JsonProperty("garbagemargin")]
//    public long GarbageMargin { get; set; }

//    [JsonProperty("garbageincrease")]
//    public double GarbageIncrease { get; set; }

//    [JsonProperty("garbagecap")]
//    public long GarbageCap { get; set; }

//    [JsonProperty("garbagecapincrease")]
//    public long GarbageCapIncrease { get; set; }

//    [JsonProperty("garbagecapmax")]
//    public long GarbageCapMax { get; set; }

//    [JsonProperty("bagtype")]
//    public string BagType { get; set; }

//    [JsonProperty("spinbonuses")]
//    public string SpinBonuses { get; set; }

//    [JsonProperty("kickset")]
//    public string KickSet { get; set; }

//    [JsonProperty("nextcount")]
//    public long NextCount { get; set; }

//    [JsonProperty("allow_harddrop")]
//    public bool AllowHardDrop { get; set; }

//    [JsonProperty("display_shadow")]
//    public bool DisplayShadow { get; set; }

//    [JsonProperty("locktime")]
//    public long LockTime { get; set; }

//    [JsonProperty("garbagespeed")]
//    public long GarbageSpeed { get; set; }

//    [JsonProperty("forfeit_time")]
//    public long ForfeitTime { get; set; }

//    [JsonProperty("are")]
//    public long Are { get; set; }

//    [JsonProperty("lineclear_are")]
//    public long LineClearAre { get; set; }

//    [JsonProperty("infinitemovement")]
//    public bool InfiniteMovement { get; set; }

//    [JsonProperty("lockresets")]
//    public long LockResets { get; set; }

//    [JsonProperty("allow180")]
//    public bool Allow180 { get; set; }

//    [JsonProperty("objective")]
//    public Objective Objective { get; set; }

//    [JsonProperty("room_handling")]
//    public bool RoomHandling { get; set; }

//    [JsonProperty("room_handling_arr")]
//    public long RoomHandlingArr { get; set; }

//    [JsonProperty("room_handling_das")]
//    public long RoomHandlingDas { get; set; }

//    [JsonProperty("room_handling_sdf")]
//    public long RoomHandlingSdf { get; set; }

//    [JsonProperty("manual_allowed")]
//    public bool ManualAllowed { get; set; }

//    [JsonProperty("b2bchaining")]
//    public bool B2BChaining { get; set; }

//    [JsonProperty("clutch")]
//    public bool Clutch { get; set; }

//    [JsonProperty("passthrough")]
//    public bool PassThrough { get; set; }

//    [JsonProperty("latencypreference")]
//    public string LatencyPreference { get; set; }

//    [JsonProperty("handling")]
//    public Handling Handling { get; set; }

//    [JsonProperty("fulloffset")]
//    public long FullOffset { get; set; }

//    [JsonProperty("fullinterval")]
//    public long FullInterval { get; set; }

//    [JsonProperty("onfail")]
//    public object OnFail { get; set; }

//    [JsonProperty("onfinish")]
//    public object OnFinish { get; set; }

//    [JsonProperty("oninteraction")]
//    public object OnInteraction { get; set; }

//    [JsonProperty("username")]
//    public string Username { get; set; }

//    [JsonProperty("boardwidth")]
//    public long BoardWidth { get; set; }

//    [JsonProperty("boardheight")]
//    public long BoardHeight { get; set; }

//    [JsonProperty("boardbuffer")]
//    public long BoardBuffer { get; set; }

//    [JsonProperty("physical")]
//    public bool Physical { get; set; }
//  }

//  public class Objective
//  {
//    [JsonProperty("type")]
//    public string Type { get; set; }
//  }

//  public class ExportReplay
//  {
//    [JsonProperty("advanceFrame")]
//    public object AdvanceFrame { get; set; }

//    [JsonProperty("getFrame")]
//    public object GetFrame { get; set; }

//    [JsonProperty("getEventCount")]
//    public object GetEventCount { get; set; }

//    [JsonProperty("getStarter")]
//    public object GetStarter { get; set; }

//    [JsonProperty("getStartEvent")]
//    public object GetStartEvent { get; set; }

//    [JsonProperty("getEndEvent")]
//    public object GetEndEvent { get; set; }

//    [JsonProperty("pushEvent")]
//    public object PushEvent { get; set; }

//    [JsonProperty("getEventsAtFrame")]
//    public object GetEventsAtFrame { get; set; }

//    [JsonProperty("export")]
//    public object Export { get; set; }

//    [JsonProperty("import")]
//    public object Import { get; set; }

//    [JsonProperty("seek")]
//    public object Seek { get; set; }

//    [JsonProperty("bindRolling")]
//    public object BindRolling { get; set; }

//    [JsonProperty("setListenID")]
//    public object SetListenId { get; set; }

//    [JsonProperty("flush")]
//    public object Flush { get; set; }

//    [JsonProperty("amendTargets")]
//    public object AmendTargets { get; set; }

//    [JsonProperty("setLatencyPreference")]
//    public object SetLatencyPreference { get; set; }
//  }

//  public class Source
//  {
//    [JsonProperty("advanceFrame")]
//    public object AdvanceFrame { get; set; }

//    [JsonProperty("getFrame")]
//    public object GetFrame { get; set; }

//    [JsonProperty("readyEventQueue")]
//    public object ReadyEventQueue { get; set; }

//    [JsonProperty("pull")]
//    public object Pull { get; set; }

//    [JsonProperty("nextFrameReady")]
//    public object NextFrameReady { get; set; }

//    [JsonProperty("fallingBehind")]
//    public object FallingBehind { get; set; }

//    [JsonProperty("amountToCatchUp")]
//    public object AmountToCatchUp { get; set; }

//    [JsonProperty("behindness")]
//    public object Behindness { get; set; }

//    [JsonProperty("bind")]
//    public object Bind { get; set; }

//    [JsonProperty("unbind")]
//    public object Unbind { get; set; }

//    [JsonProperty("seek")]
//    public object Seek { get; set; }

//    [JsonProperty("finished")]
//    public object Finished { get; set; }

//    [JsonProperty("type")]
//    public object Type { get; set; }

//    [JsonProperty("pushIGE")]
//    public object PushIge { get; set; }

//    [JsonProperty("pushTargets")]
//    public object PushTargets { get; set; }

//    [JsonProperty("unhook")]
//    public object Unhook { get; set; }

//    [JsonProperty("destroy")]
//    public object Destroy { get; set; }

//    [JsonProperty("socket")]
//    public object Socket { get; set; }

//    [JsonProperty("bindHyperRetry")]
//    public object BindHyperRetry { get; set; }

//    [JsonProperty("bindHyperForfeit")]
//    public object BindHyperForfeit { get; set; }
//  }

//  public class Stats
//  {
//    [JsonProperty("seed")]
//    public long Seed { get; set; }

//    [JsonProperty("lines")]
//    public long Lines { get; set; }

//    [JsonProperty("level_lines")]
//    public long LevelLines { get; set; }

//    [JsonProperty("level_lines_needed")]
//    public long LevelLinesNeeded { get; set; }

//    [JsonProperty("inputs")]
//    public long Inputs { get; set; }

//    [JsonProperty("holds")]
//    public long Holds { get; set; }

//    [JsonProperty("time")]
//    public Time Time { get; set; }

//    [JsonProperty("score")]
//    public long Score { get; set; }

//    [JsonProperty("level")]
//    public long Level { get; set; }

//    [JsonProperty("combo")]
//    public long Combo { get; set; }

//    [JsonProperty("currentcombopower")]
//    public long CurrentComboPower { get; set; }

//    [JsonProperty("topcombo")]
//    public long TopCombo { get; set; }

//    [JsonProperty("btb")]
//    public long Btb { get; set; }

//    [JsonProperty("topbtb")]
//    public long TopBTB { get; set; }

//    [JsonProperty("tspins")]
//    public long TSpins { get; set; }

//    [JsonProperty("piecesplaced")]
//    public long PiecesPlaced { get; set; }

//    [JsonProperty("clears")]
//    public Dictionary<string, long> Clears { get; set; }

//    [JsonProperty("garbage")]
//    public Garbage Garbage { get; set; }

//    [JsonProperty("kills")]
//    public long Kills { get; set; }

//    [JsonProperty("finesse")]
//    public Finesse Finesse { get; set; }
//  }

//  public class Finesse
//  {
//    [JsonProperty("combo")]
//    public long Combo { get; set; }

//    [JsonProperty("faults")]
//    public long Faults { get; set; }

//    [JsonProperty("perfectpieces")]
//    public long PerfectPieces { get; set; }
//  }

//  public class Garbage
//  {
//    [JsonProperty("sent")]
//    public long Sent { get; set; }

//    [JsonProperty("received")]
//    public long Received { get; set; }

//    [JsonProperty("attack")]
//    public long Attack { get; set; }

//    [JsonProperty("cleared")]
//    public long Cleared { get; set; }
//  }

//  public class Time
//  {
//    [JsonProperty("start")]
//    public long Start { get; set; }

//    [JsonProperty("zero")]
//    public bool Zero { get; set; }

//    [JsonProperty("locked")]
//    public bool Locked { get; set; }

//    [JsonProperty("prev")]
//    public long Prev { get; set; }

//    [JsonProperty("frameoffset")]
//    public long FrameOffset { get; set; }
//  }

//  public class DataGame
//  {
//    [JsonProperty("board")]
//    public List<List<object>> Board { get; set; }

//    [JsonProperty("bag")]
//    public List<MinoKind> Bag { get; set; }

//    [JsonProperty("hold")]
//    public Hold Hold { get; set; }

//    [JsonProperty("g")]
//    public double G { get; set; }

//    [JsonProperty("controlling")]
//    public Controlling Controlling { get; set; }

//    [JsonProperty("handling")]
//    public Handling Handling { get; set; }

//    [JsonProperty("playing")]
//    public bool Playing { get; set; }
//  }

//  public class EndContext
//  {
//    [JsonProperty("naturalorder")]
//    public int NaturalOrder { get; set; }

//    [JsonProperty("user")]
//    public User User { get; set; }

//    [JsonProperty("active")]
//    public bool IsActive { get; set; }

//    [JsonProperty("wins")]
//    public int Wins { get; set; }

//    [JsonProperty("points")]
//    public Points Points { get; set; }

//    [JsonProperty("inputs")]
//    public int Inputs { get; set; }

//    [JsonProperty("piecesplaced")]
//    public int PiecesPlaced { get; set; }
//  }

//  public class Points
//  {
//    [JsonProperty("primary")]
//    public double Primary { get; set; }

//    [JsonProperty("secondary")]
//    public double Secondary { get; set; }

//    [JsonProperty("tertiary")]
//    public double Tertiary { get; set; }

//    [JsonProperty("extra")]
//    public Extra Extra { get; set; }

//    [JsonProperty("secondaryAvgTracking")]
//    public List<double> SecondaryAvgTracking { get; set; }

//    [JsonProperty("tertiaryAvgTracking")]
//    public List<double> TertiaryAvgTracking { get; set; }

//    [JsonProperty("extraAvgTracking")]
//    public ExtraAvgTracking ExtraAvgTracking { get; set; }
//  }

//  public class Extra
//  {
//    [JsonProperty("vs")]
//    public double VSScore { get; set; }
//  }

//  public class ExtraAvgTracking
//  {
//    [JsonProperty("aggregatestats___vsscore")]
//    public List<double> AggregateStatsVSScore { get; set; }
//  }

//  public enum PurpleType { Garbage };

//  public enum InteractionKind { Interaction, InteractionConfirm };

//  public enum MinoKind
//  {
//	  Gb, 
//	  I, 
//	  J,
//	  L,
//	  O,
//	  S,
//	  T,
//	  Z
//  }

//  public enum IdEnum
//  {
//	  Diyusi
//  };

//  public enum Key
//  {
//	  HardDrop, 
//	  Hold, 
//	  MoveLeft,
//	  MoveRight,
//	  Rotate180, 
//	  RotateCcw, 
//	  RotateCw, 
//	  SoftDrop
//  };

//  public enum EventType
//  {
//	  End,
//	  Full,
//	  InGameEvent, 
//	  Keydown,
//	  Keyup, 
//	  Start, 
//	  Targets
//  };

//  public struct DataUnion
//  {
//    public PurpleData PurpleData;
//    public List<string> StringArray;

//    public static implicit operator DataUnion(PurpleData PurpleData) => new DataUnion { PurpleData = PurpleData };
//    public static implicit operator DataUnion(List<string> StringArray) => new DataUnion { StringArray = StringArray };
//  }

//  public struct IdUnion
//  {
//    public IdEnum? Enum;
//    public long? Integer;

//    public static implicit operator IdUnion(IdEnum Enum) => new IdUnion { Enum = Enum };
//    public static implicit operator IdUnion(long Integer) => new IdUnion { Integer = Integer };
//  }

//  internal static class Converter
//  {
//    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
//    {
//      MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
//      DateParseHandling = DateParseHandling.None,
//      Converters =
//      {
//        DataUnionConverter.Singleton,
//        PurpleTypeConverter.Singleton,
//        FluffyTypeConverter.Singleton,
//        BagConverter.Singleton,
//        IdUnionConverter.Singleton,
//        IdEnumConverter.Singleton,
//        KeyConverter.Singleton,
//        EventTypeConverter.Singleton,
//        new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
//      },
//    };
//  }

//  internal class DataUnionConverter : JsonConverter
//  {
//    public override bool CanConvert(Type t) => t == typeof(DataUnion) || t == typeof(DataUnion?);

//    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//    {
//      switch (reader.TokenType)
//      {
//        case JsonToken.StartObject:
//          var objectValue = serializer.Deserialize<PurpleData>(reader);
//          return new DataUnion { PurpleData = objectValue };
//        case JsonToken.StartArray:
//          var arrayValue = serializer.Deserialize<List<string>>(reader);
//          return new DataUnion { StringArray = arrayValue };
//      }
//      throw new Exception("Cannot unmarshal type DataUnion");
//    }

//    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//    {
//      var value = (DataUnion)untypedValue;
//      if (value.StringArray != null)
//      {
//        serializer.Serialize(writer, value.StringArray);
//        return;
//      }
//      if (value.PurpleData != null)
//      {
//        serializer.Serialize(writer, value.PurpleData);
//        return;
//      }
//      throw new Exception("Cannot marshal type DataUnion");
//    }

//    public static readonly DataUnionConverter Singleton = new DataUnionConverter();
//  }

//  internal class PurpleTypeConverter : JsonConverter
//  {
//    public override bool CanConvert(Type t) => t == typeof(PurpleType) || t == typeof(PurpleType?);

//    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//    {
//      if (reader.TokenType == JsonToken.Null) return null;
//      var value = serializer.Deserialize<string>(reader);
//      if (value == "garbage")
//      {
//        return PurpleType.Garbage;
//      }
//      throw new Exception("Cannot unmarshal type PurpleType");
//    }

//    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//    {
//      if (untypedValue == null)
//      {
//        serializer.Serialize(writer, null);
//        return;
//      }
//      var value = (PurpleType)untypedValue;
//      if (value == PurpleType.Garbage)
//      {
//        serializer.Serialize(writer, "garbage");
//        return;
//      }
//      throw new Exception("Cannot marshal type PurpleType");
//    }

//    public static readonly PurpleTypeConverter Singleton = new PurpleTypeConverter();
//  }

//  internal class FluffyTypeConverter : JsonConverter
//  {
//    public override bool CanConvert(Type t) => t == typeof(InteractionKind) || t == typeof(InteractionKind?);

//    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//    {
//      if (reader.TokenType == JsonToken.Null) return null;
//      var value = serializer.Deserialize<string>(reader);
//      switch (value)
//      {
//        case "interaction":
//          return InteractionKind.Interaction;
//        case "interaction_confirm":
//          return InteractionKind.InteractionConfirm;
//      }
//      throw new Exception("Cannot unmarshal type InteractionKind");
//    }

//    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//    {
//      if (untypedValue == null)
//      {
//        serializer.Serialize(writer, null);
//        return;
//      }
//      var value = (InteractionKind)untypedValue;
//      switch (value)
//      {
//        case InteractionKind.Interaction:
//          serializer.Serialize(writer, "interaction");
//          return;
//        case InteractionKind.InteractionConfirm:
//          serializer.Serialize(writer, "interaction_confirm");
//          return;
//      }
//      throw new Exception("Cannot marshal type InteractionKind");
//    }

//    public static readonly FluffyTypeConverter Singleton = new FluffyTypeConverter();
//  }

//  internal class BagConverter : JsonConverter
//  {
//    public override bool CanConvert(Type t) => t == typeof(MinoKind) || t == typeof(MinoKind?);

//    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//    {
//      if (reader.TokenType == JsonToken.Null) return null;
//      var value = serializer.Deserialize<string>(reader);
//      switch (value)
//      {
//        case "gb":
//          return MinoKind.Gb;
//        case "i":
//          return MinoKind.I;
//        case "j":
//          return MinoKind.J;
//        case "l":
//          return MinoKind.L;
//        case "o":
//          return MinoKind.O;
//        case "s":
//          return MinoKind.S;
//        case "t":
//          return MinoKind.T;
//        case "z":
//          return MinoKind.Z;
//      }
//      throw new Exception("Cannot unmarshal type MinoKind");
//    }

//    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//    {
//      if (untypedValue == null)
//      {
//        serializer.Serialize(writer, null);
//        return;
//      }
//      var value = (MinoKind)untypedValue;
//      switch (value)
//      {
//        case MinoKind.Gb:
//          serializer.Serialize(writer, "gb");
//          return;
//        case MinoKind.I:
//          serializer.Serialize(writer, "i");
//          return;
//        case MinoKind.J:
//          serializer.Serialize(writer, "j");
//          return;
//        case MinoKind.L:
//          serializer.Serialize(writer, "l");
//          return;
//        case MinoKind.O:
//          serializer.Serialize(writer, "o");
//          return;
//        case MinoKind.S:
//          serializer.Serialize(writer, "s");
//          return;
//        case MinoKind.T:
//          serializer.Serialize(writer, "t");
//          return;
//        case MinoKind.Z:
//          serializer.Serialize(writer, "z");
//          return;
//      }
//      throw new Exception("Cannot marshal type MinoKind");
//    }

//    public static readonly BagConverter Singleton = new BagConverter();
//  }

//  internal class IdUnionConverter : JsonConverter
//  {
//    public override bool CanConvert(Type t) => t == typeof(IdUnion) || t == typeof(IdUnion?);

//    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//    {
//      switch (reader.TokenType)
//      {
//        case JsonToken.Integer:
//          var integerValue = serializer.Deserialize<long>(reader);
//          return new IdUnion { Integer = integerValue };
//        case JsonToken.String:
//        case JsonToken.Date:
//          var stringValue = serializer.Deserialize<string>(reader);
//          if (stringValue == "diyusi")
//          {
//            return new IdUnion { Enum = IdEnum.Diyusi };
//          }
//          break;
//      }
//      throw new Exception("Cannot unmarshal type IdUnion");
//    }

//    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//    {
//      var value = (IdUnion)untypedValue;
//      if (value.Integer != null)
//      {
//        serializer.Serialize(writer, value.Integer.Value);
//        return;
//      }
//      if (value.Enum != null)
//      {
//        if (value.Enum == IdEnum.Diyusi)
//        {
//          serializer.Serialize(writer, "diyusi");
//          return;
//        }
//      }
//      throw new Exception("Cannot marshal type IdUnion");
//    }

//    public static readonly IdUnionConverter Singleton = new IdUnionConverter();
//  }

//  internal class IdEnumConverter : JsonConverter
//  {
//    public override bool CanConvert(Type t) => t == typeof(IdEnum) || t == typeof(IdEnum?);

//    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//    {
//      if (reader.TokenType == JsonToken.Null) return null;
//      var value = serializer.Deserialize<string>(reader);
//      if (value == "diyusi")
//      {
//        return IdEnum.Diyusi;
//      }
//      throw new Exception("Cannot unmarshal type IdEnum");
//    }

//    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//    {
//      if (untypedValue == null)
//      {
//        serializer.Serialize(writer, null);
//        return;
//      }
//      var value = (IdEnum)untypedValue;
//      if (value == IdEnum.Diyusi)
//      {
//        serializer.Serialize(writer, "diyusi");
//        return;
//      }
//      throw new Exception("Cannot marshal type IdEnum");
//    }

//    public static readonly IdEnumConverter Singleton = new IdEnumConverter();
//  }

//  internal class KeyConverter : JsonConverter
//  {
//    public override bool CanConvert(Type t) => t == typeof(Key) || t == typeof(Key?);

//    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//    {
//      if (reader.TokenType == JsonToken.Null) return null;
//      var value = serializer.Deserialize<string>(reader);
//      switch (value)
//      {
//        case "hardDrop":
//          return Key.HardDrop;
//        case "hold":
//          return Key.Hold;
//        case "moveLeft":
//          return Key.MoveLeft;
//        case "moveRight":
//          return Key.MoveRight;
//        case "rotate180":
//          return Key.Rotate180;
//        case "rotateCCW":
//          return Key.RotateCcw;
//        case "rotateCW":
//          return Key.RotateCw;
//        case "softDrop":
//          return Key.SoftDrop;
//      }
//      throw new Exception("Cannot unmarshal type Key");
//    }

//    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//    {
//      if (untypedValue == null)
//      {
//        serializer.Serialize(writer, null);
//        return;
//      }
//      var value = (Key)untypedValue;
//      switch (value)
//      {
//        case Key.HardDrop:
//          serializer.Serialize(writer, "hardDrop");
//          return;
//        case Key.Hold:
//          serializer.Serialize(writer, "hold");
//          return;
//        case Key.MoveLeft:
//          serializer.Serialize(writer, "moveLeft");
//          return;
//        case Key.MoveRight:
//          serializer.Serialize(writer, "moveRight");
//          return;
//        case Key.Rotate180:
//          serializer.Serialize(writer, "rotate180");
//          return;
//        case Key.RotateCcw:
//          serializer.Serialize(writer, "rotateCCW");
//          return;
//        case Key.RotateCw:
//          serializer.Serialize(writer, "rotateCW");
//          return;
//        case Key.SoftDrop:
//          serializer.Serialize(writer, "softDrop");
//          return;
//      }
//      throw new Exception("Cannot marshal type Key");
//    }

//    public static readonly KeyConverter Singleton = new KeyConverter();
//  }

//  internal class EventTypeConverter : JsonConverter
//  {
//    public override bool CanConvert(Type t) => t == typeof(EventType) || t == typeof(EventType?);

//    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//    {
//      if (reader.TokenType == JsonToken.Null) return null;
//      var value = serializer.Deserialize<string>(reader);
//      switch (value)
//      {
//        case "end":
//          return EventType.End;
//        case "full":
//          return EventType.Full;
//        case "ige":
//          return EventType.InGameEvent;
//        case "keydown":
//          return EventType.Keydown;
//        case "keyup":
//          return EventType.Keyup;
//        case "start":
//          return EventType.Start;
//        case "targets":
//          return EventType.Targets;
//      }
//      throw new Exception("Cannot unmarshal type EventType");
//    }

//    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//    {
//      if (untypedValue == null)
//      {
//        serializer.Serialize(writer, null);
//        return;
//      }
//      var value = (EventType)untypedValue;
//      switch (value)
//      {
//        case EventType.End:
//          serializer.Serialize(writer, "end");
//          return;
//        case EventType.Full:
//          serializer.Serialize(writer, "full");
//          return;
//        case EventType.InGameEvent:
//          serializer.Serialize(writer, "ige");
//          return;
//        case EventType.Keydown:
//          serializer.Serialize(writer, "keydown");
//          return;
//        case EventType.Keyup:
//          serializer.Serialize(writer, "keyup");
//          return;
//        case EventType.Start:
//          serializer.Serialize(writer, "start");
//          return;
//        case EventType.Targets:
//          serializer.Serialize(writer, "targets");
//          return;
//      }
//      throw new Exception("Cannot marshal type EventType");
//    }

//    public static readonly EventTypeConverter Singleton = new EventTypeConverter();
//  }
//}
