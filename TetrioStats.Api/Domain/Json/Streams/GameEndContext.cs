using System;
using Newtonsoft.Json;
using TetrioStats.Api.Domain.Json.Converters;
using TetrioStats.Data.Enums;

namespace TetrioStats.Api.Domain.Json.Streams
{
	public class GameEndContext
	{
		[JsonProperty("gametype")]
		public GameType GameType { get; set; }

		//TODO unix timestamp conversion
		[JsonProperty("finalTime")]
		[JsonConverter(typeof(MillisecondsToTimeStampConverter))]
		public TimeSpan FinalGameTime { get; set; }
		
		[JsonProperty("lines")]
		public int LinesCleared { get; set; }

		[JsonProperty("level_lines")]
		public int LevelLines { get; set; }

		[JsonProperty("level_lines_needed")]
		public int LevelLinesNeeded { get; set; }

		[JsonProperty("inputs")]
		public int Inputs { get; set; }

		[JsonProperty("holds")]
		public int Holds { get; set; }

		[JsonProperty("time")]
		public Time Time { get; set; }

		[JsonProperty("score")]
		public int Score { get; set; }

		[JsonProperty("zenlevel")]
		public int ZenLevel { get; set; }

		[JsonProperty("zenprogress")]
		public int ZenProgress { get; set; }

		[JsonProperty("level")]
		public int Level { get; set; }

		[JsonProperty("combo")]
		public int Combo { get; set; }

		[JsonProperty("currentcombopower")]
		public int? CurrentComboPower { get; set; }

		[JsonProperty("topcombo")]
		public int TopCombo { get; set; }

		[JsonProperty("btb")]
		public int BTB { get; set; }

		[JsonProperty("topbtb")]
		public int TopBTB { get; set; }

		[JsonProperty("tspins")]
		public int TSpins { get; set; }

		[JsonProperty("piecesplaced")]
		public int TotalPiecesPlaced { get; set; }

		[JsonProperty("clears")]
		public LineClearsStatistics LineClearsStatistics { get; set; }
		
		[JsonProperty("garbage")]
		public GarbageStatistics GarbageStatistics { get; set; }

		[JsonProperty("kills")]
		public int Kills { get; set; }

		[JsonProperty("finesse")]
		public FinesseStatistics FinesseStatistics { get; set; }

		[JsonProperty("seed")]
		public int Seed { get; set; }
	}
}