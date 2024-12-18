using System;
using System.ComponentModel.DataAnnotations.Schema;
using TetrioStats.Data.Enums;

namespace TetrioStats.Data.Domain;

public class GameRecord
{
	public int GameRecordID { get; set; }

	public GameType GameType { get; set; }

	public string TetrioGameRecordID { get; set; }

	public string StreamKey { get; set; }

	public string ReplayID { get; set; }

	public int UserID { get; set; }
	[ForeignKey("UserID")]
	public User User { get; set; }

	public string Username { get; set; }

	public DateTime TimeStamp { get; set; }

	public bool IsMultiplayer { get; set; }

	public TimeSpan FinalTime { get; set; }

	public int Kills { get; set; }

	public int LinesCleared { get; set; }

	public int LevelLines { get; set; }

	public int LevelLinesNeeded { get; set; }

	public int Inputs { get; set; }

	public int Holds { get; set; }

	public int Score { get; set; }

	public int ZenLevel { get; set; }

	public int ZenProgress { get; set; }

	public int Level { get; set; }

	public int Combo { get; set; }

	public int? CurrentComboPower { get; set; }

	public int TopCombo { get; set; }

	public int BTB { get; set; }

	public int TopBTB { get; set; }

	public int TSpins { get; set; }

	public int TotalPiecesPlaced { get; set; }

	public int LineClearsSingles { get; set; }

	public int LineClearsDoubles { get; set; }

	public int LineClearsTriples { get; set; }

	public int LineClearsQuads { get; set; }

	public int LineClearsRealTSpins { get; set; }

	public int LineClearsMiniTSpins { get; set; }

	public int LineClearsMiniTSpinSingles { get; set; }

	public int LineClearsTSpinSingles { get; set; }

	public int LineClearsMiniTSpinDoubles { get; set; }

	public int LineClearsTSpinDoubles { get; set; }

	public int LineClearsTSpinTriples { get; set; }

	public int LineClearsTSpinQuads { get; set; }

	public int LineClearsAllClears { get; set; }

	public int GarbageTotalSent { get; set; }

	public int GarbageTotalReceived { get; set; }

	public int GarbageAttack { get; set; }

	public int GarbageTotalCleared { get; set; }

	public int FinesseCombo { get; set; }

	public int FinesseFaults { get; set; }

	public int FinessePerfectPieces { get; set; }


	public GameRecord()
	{
	}

	//public GameRecord(GameRecordInfo record)
	//{
	//  GameType = record.EndContext.GameType;
	//  TetrioGameRecordID = record.ID;
	//  StreamKey = record.StreamID;
	//  ReplayID = record.ReplayID;
	//  UserID = record.UserInfo.UserId;
	//  Username = record.UserInfo.Username;
	//  TimeStamp = record.TimeStamp.DateTime;

	//  IsMultiplayer = record.IsMultiplayer;
	//  FinalTime = record.EndContext.FinalGameTime;
	//  Kills = record.EndContext.Kills;
	//  LinesCleared = record.EndContext.LinesCleared;
	//  LevelLines = record.EndContext.LevelLines;
	//  LevelLinesNeeded = record.EndContext.LevelLinesNeeded;
	//  Inputs = record.EndContext.Inputs;
	//  Holds = record.EndContext.Holds;
	//  Score = record.EndContext.Score;
	//  ZenLevel = record.EndContext.ZenLevel;
	//  ZenProgress = record.EndContext.ZenProgress;
	//  Level = record.EndContext.Level;
	//  Combo = record.EndContext.Combo;
	//  CurrentComboPower = record.EndContext.CurrentComboPower;
	//  TopCombo = record.EndContext.TopCombo;
	//  BTB = record.EndContext.BTB;
	//  TopBTB = record.EndContext.TopBTB;
	//  TSpins = record.EndContext.TSpins;
	//  TotalPiecesPlaced = record.EndContext.TotalPiecesPlaced;

	//  LineClearsSingles = record.EndContext.LineClearsStatistics.Singles;
	//  LineClearsDoubles = record.EndContext.LineClearsStatistics.Doubles;
	//  LineClearsTriples = record.EndContext.LineClearsStatistics.Triples;
	//  LineClearsQuads = record.EndContext.LineClearsStatistics.Quads;
	//  LineClearsRealTSpins = record.EndContext.LineClearsStatistics.RealTSpins;
	//  LineClearsMiniTSpins = record.EndContext.LineClearsStatistics.MiniTSpins;
	//  LineClearsMiniTSpinSingles = record.EndContext.LineClearsStatistics.MiniTSpinSingles;
	//  LineClearsTSpinSingles = record.EndContext.LineClearsStatistics.TSpinSingles;
	//  LineClearsMiniTSpinDoubles = record.EndContext.LineClearsStatistics.MiniTSpinDoubles;
	//  LineClearsTSpinDoubles = record.EndContext.LineClearsStatistics.TSpinDoubles;
	//  LineClearsTSpinTriples = record.EndContext.LineClearsStatistics.TSpinTriples;
	//  LineClearsTSpinQuads = record.EndContext.LineClearsStatistics.TSpinQuads;
	//  LineClearsAllClears = record.EndContext.LineClearsStatistics.AllClears;

	//  GarbageTotalSent = record.EndContext.GarbageStatistics.TotalSent;
	//  GarbageTotalReceived = record.EndContext.GarbageStatistics.TotalReceived;
	//  GarbageAttack = record.EndContext.GarbageStatistics.Attack;
	//  GarbageTotalCleared = record.EndContext.GarbageStatistics.TotalCleared;

	//  FinesseCombo = record.EndContext.FinesseStatistics.Combo;
	//  FinesseFaults = record.EndContext.FinesseStatistics.FinesseFaults;
	//  FinessePerfectPieces = record.EndContext.FinesseStatistics.PerfectPieces;
	//}
}