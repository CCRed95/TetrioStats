using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TetrioStats.Data.Domain;

/// <summary>
/// Describes a user's current Tetra League statistics and standing.
/// </summary>
public class UserStatisticsEntry
{
	public int UserStatisticsEntryID { get; set; }

	public int UserID { get; set; }
	[ForeignKey("UserID")]
	public User User { get; set; }

	public DateTime TimeStamp { get; set; }

	public double XP { get; set; }

	public int GamesPlayed { get; set; }

	public int GamesWon { get; set; }

	public TimeSpan TotalGamePlayDuration { get; }

	/// <summary>
	/// The amount of TETRA LEAGUE games played by this user.
	/// </summary>
	public int TetraLeagueGamesPlayed { get; }

	/// <summary>
	/// The amount of TETRA LEAGUE games won by this user.
	/// </summary>
	public int TetraLeagueGamesWon { get; }

	/// <summary>
	/// This user's TR (Tetra Rating), or -1 if less than 10 games were played.
	/// </summary>
	public double TetraLeagueRating { get; }

	/// <summary>
	/// This user's Glicko-2 rating.
	/// </summary>
	public double GlickoRating { get; }

	/// <summary>
	/// This user's Glicko-2 Rating Deviation. If over 100, this user is unranked.
	/// </summary>
	public double GlickoRatingDeviation { get; }

	/// <summary>
	/// This user's letter rank. Z is unranked.
	/// </summary>
	public string UserRank { get; }

	/// <summary>
	/// This user's average APM (attack per minute) over the last 10 games.
	/// </summary>
	public double AverageRollingAPM { get; }

	/// <summary>
	/// This user's average PPS (pieces per second) over the last 10 games.
	/// </summary>
	public double AverageRollingPPS { get; }

	/// <summary>
	/// This user's average VS (versus score) over the last 10 games.
	/// </summary>
	public double AverageRollingVsScore { get; }

	/// <summary>
	/// This user's position in global leader boards, or -1 if not applicable.
	/// </summary>
	public int GlobalLeaderBoardsStanding { get; }

	/// <summary>
	/// This user's position in local leader boards, or -1 if not applicable.
	/// </summary>
	public int LocalLeaderBoardsStanding { get; }

	/// <summary>
	/// This user's percentile position, between 0.0 (best) and 1.0 (worst).
	/// </summary>
	public double Percentile { get; }

	/// <summary>
	/// This user's percentile rank, or Z if not applicable.
	/// </summary>
	public string PercentileRank { get; }


	public UserStatisticsEntry()
	{
	}

	public UserStatisticsEntry(
		User user,
		DateTime timeStamp,
		double xp,
		int gamesPlayed,
		int gamesWon,
		TimeSpan totalGamePlayDuration,
		int tetraLeagueGamesPlayed,
		int tetraLeagueGamesWon,
		double tetraLeagueRating,
		double glickoRating,
		double glickoRatingDeviation,
		string userRank,
		double averageRollingApm,
		double averageRollingPps,
		double averageRollingVsScore,
		int globalLeaderBoardsStanding,
		int localLeaderBoardsStanding,
		double percentile,
		string percentileRank) : this()
	{
		UserID = user.UserID;
		TimeStamp = timeStamp;
		XP = xp;
		GamesPlayed = gamesPlayed;
		GamesWon = gamesWon;
		TotalGamePlayDuration = totalGamePlayDuration;
		TetraLeagueGamesPlayed = tetraLeagueGamesPlayed;
		TetraLeagueGamesWon = tetraLeagueGamesWon;
		TetraLeagueRating = tetraLeagueRating;
		GlickoRating = glickoRating;
		GlickoRatingDeviation = glickoRatingDeviation;
		UserRank = userRank;
		AverageRollingAPM = averageRollingApm;
		AverageRollingPPS = averageRollingPps;
		AverageRollingVsScore = averageRollingVsScore;
		GlobalLeaderBoardsStanding = globalLeaderBoardsStanding;
		LocalLeaderBoardsStanding = localLeaderBoardsStanding;
		Percentile = percentile;
		PercentileRank = percentileRank;
	}

	public UserStatisticsEntry(
		int userStatisticsEntryId,
		User user,
		DateTime timeStamp,
		double xp,
		int gamesPlayed,
		int gamesWon,
		TimeSpan totalGamePlayDuration,
		int tetraLeagueGamesPlayed,
		int tetraLeagueGamesWon,
		double tetraLeagueRating,
		double glickoRating,
		double glickoRatingDeviation,
		string userRank,
		double averageRollingApm,
		double averageRollingPps,
		double averageRollingVsScore,
		int globalLeaderBoardsStanding,
		int localLeaderBoardsStanding,
		double percentile,
		string percentileRank) : this(
		user,
		timeStamp,
		xp,
		gamesPlayed,
		gamesWon,
		totalGamePlayDuration,
		tetraLeagueGamesPlayed,
		tetraLeagueGamesWon,
		tetraLeagueRating,
		glickoRating,
		glickoRatingDeviation,
		userRank,
		averageRollingApm,
		averageRollingPps,
		averageRollingVsScore,
		globalLeaderBoardsStanding,
		localLeaderBoardsStanding,
		percentile,
		percentileRank)
	{
		UserStatisticsEntryID = userStatisticsEntryId;
	}


	public override bool Equals(object obj)
	{
		if (obj is UserStatisticsEntry stats)
		{
			return UserID.Equals(stats.UserID)
				&& XP.Equals(stats.XP)
				&& GamesPlayed.Equals(stats.GamesPlayed)
				&& GamesWon.Equals(stats.GamesWon)
				&& TotalGamePlayDuration.Equals(stats.TotalGamePlayDuration)
				&& TetraLeagueGamesPlayed.Equals(stats.TetraLeagueGamesPlayed)
				&& TetraLeagueGamesWon.Equals(stats.TetraLeagueGamesWon)
				&& TetraLeagueRating.Equals(stats.TetraLeagueRating)
				&& GlickoRating.Equals(stats.GlickoRating)
				&& GlickoRatingDeviation.Equals(stats.GlickoRatingDeviation)
				&& UserRank.Equals(stats.UserRank)
				&& AverageRollingAPM.Equals(stats.AverageRollingAPM)
				&& AverageRollingPPS.Equals(stats.AverageRollingPPS)
				&& AverageRollingVsScore.Equals(stats.AverageRollingVsScore)
				&& GlobalLeaderBoardsStanding.Equals(stats.GlobalLeaderBoardsStanding)
				&& LocalLeaderBoardsStanding.Equals(stats.LocalLeaderBoardsStanding)
				&& Percentile.Equals(stats.Percentile)
				&& PercentileRank.Equals(stats.PercentileRank);
		}

		return false;
	}
}


public class PublicRoomListInfo
{
	public bool WasSuccessful { get; set; }

	public int RoomId { get; set; }

	public string RoomName { get; set; }
}