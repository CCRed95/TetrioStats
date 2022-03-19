using System;
using Newtonsoft.Json;
using TetrioStats.Core.Data.Common.Users;
using TetrioStats.Data.Domain;

namespace TetrioStats.Api.Domain.Json.Streams
{
	public class GameRecordInfo
	{
		[JsonProperty("_id")]
		public string ID { get; set; }

		[JsonProperty("stream")]
		public string StreamID { get; set; }

		[JsonProperty("replayid")]
		public string ReplayID { get; set; }

		[JsonProperty("user")]
		public UserInfo UserInfo { get; set; }

		[JsonProperty("ts")]
		public DateTimeOffset TimeStamp { get; set; }

		[JsonProperty("endcontext")]
		public GameEndContext EndContext { get; set; }

		[JsonProperty("ismulti")]
		public bool IsMultiplayer { get; set; }

		private static readonly TetrioApiClient _client
			= new TetrioApiClient();

		public GameRecord ToGameRecord()
		{
			var user = _client.ResolveUser(UserInfo.UserId);

			return new GameRecord
			{
				GameType = EndContext.GameType,
				TetrioGameRecordID = ID,
				StreamKey = StreamID,
				ReplayID = ReplayID,
				UserID = user.UserID,
				Username = UserInfo.Username,
				TimeStamp = TimeStamp.DateTime,

				IsMultiplayer = IsMultiplayer,
				FinalTime = EndContext.FinalGameTime,
				Kills = EndContext.Kills,
				LinesCleared = EndContext.LinesCleared,
				LevelLines = EndContext.LevelLines,
				LevelLinesNeeded = EndContext.LevelLinesNeeded,
				Inputs = EndContext.Inputs,
				Holds = EndContext.Holds,
				Score = EndContext.Score,
				ZenLevel = EndContext.ZenLevel,
				ZenProgress = EndContext.ZenProgress,
				Level = EndContext.Level,
				Combo = EndContext.Combo,
				CurrentComboPower = EndContext.CurrentComboPower,
				TopCombo = EndContext.TopCombo,
				BTB = EndContext.BTB,
				TopBTB = EndContext.TopBTB,
				TSpins = EndContext.TSpins,
				TotalPiecesPlaced = EndContext.TotalPiecesPlaced,

				LineClearsSingles = EndContext.LineClearsStatistics.Singles,
				LineClearsDoubles = EndContext.LineClearsStatistics.Doubles,
				LineClearsTriples = EndContext.LineClearsStatistics.Triples,
				LineClearsQuads = EndContext.LineClearsStatistics.Quads,
				LineClearsRealTSpins = EndContext.LineClearsStatistics.RealTSpins,
				LineClearsMiniTSpins = EndContext.LineClearsStatistics.MiniTSpins,
				LineClearsMiniTSpinSingles = EndContext.LineClearsStatistics.MiniTSpinSingles,
				LineClearsTSpinSingles = EndContext.LineClearsStatistics.TSpinSingles,
				LineClearsMiniTSpinDoubles = EndContext.LineClearsStatistics.MiniTSpinDoubles,
				LineClearsTSpinDoubles = EndContext.LineClearsStatistics.TSpinDoubles,
				LineClearsTSpinTriples = EndContext.LineClearsStatistics.TSpinTriples,
				LineClearsTSpinQuads = EndContext.LineClearsStatistics.TSpinQuads,
				LineClearsAllClears = EndContext.LineClearsStatistics.AllClears,

				GarbageTotalSent = EndContext.GarbageStatistics.TotalSent,
				GarbageTotalReceived = EndContext.GarbageStatistics.TotalReceived,
				GarbageAttack = EndContext.GarbageStatistics.Attack,
				GarbageTotalCleared = EndContext.GarbageStatistics.TotalCleared,

				FinesseCombo = EndContext.FinesseStatistics.Combo,
				FinesseFaults = EndContext.FinesseStatistics.FinesseFaults,
				FinessePerfectPieces = EndContext.FinesseStatistics.PerfectPieces
			};
		}
	}
}