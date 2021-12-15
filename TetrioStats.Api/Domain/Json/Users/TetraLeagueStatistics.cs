using Newtonsoft.Json;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Domain.Json.Users
{
	/// <summary>
	/// Describes a user's current Tetra League statistics and standing.
	/// </summary>
	public class TetraLeagueStatistics
	{
		/// <summary>
		/// The amount of TETRA LEAGUE games played by this user.
		/// </summary>
		[JsonProperty("gamesplayed")]
		public int GamesPlayed { get; set; }

		/// <summary>
		/// The amount of TETRA LEAGUE games won by this user.
		/// </summary>
		[JsonProperty("gameswon")]
		public int GamesWon { get; set; }

		/// <summary>
		/// This user's TR (Tetra Rating), or -1 if less than 10 games were played.
		/// </summary>
		[JsonProperty("rating")]
		public double TetraLeagueRating { get; set; }

		/// <summary>
		/// This user's Glicko-2 rating.
		/// </summary>
		[JsonProperty("glicko")]
		public double GlickoRating { get; set; }

		/// <summary>
		/// This user's Glicko-2 Rating Deviation. If over 100, this user is unranked.
		/// </summary>
		[JsonProperty("rd")]
		public double GlickoRatingDeviation { get; set; }

		/// <summary>
		/// This user's letter rank. Z is unranked.
		/// </summary>
		[JsonProperty("rank")]
		public string UserRank { get; set; }

		/// <summary>
		/// This user's average APM (attack per minute) over the last 10 games.
		/// </summary>
		[JsonProperty("apm")]
		public double AverageRollingAPM { get; set; }

		/// <summary>
		/// This user's average PPS (pieces per second) over the last 10 games.
		/// </summary>
		[JsonProperty("pps")]
		public double AverageRollingPPS { get; set; }

		/// <summary>
		/// This user's average VS (versus score) over the last 10 games.
		/// </summary>
		[JsonProperty("vs")]
		public double AverageRollingVSScore { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("decaying")]
		public bool IsDecaying { get; set; }

		/// <summary>
		/// This user's position in global leader boards, or -1 if not applicable.
		/// </summary>
		[JsonProperty("standing")]
		public int GlobalLeaderBoardsStanding { get; set; }

		/// <summary>
		/// This user's position in local leader boards, or -1 if not applicable.
		/// </summary>
		[JsonProperty("standing_local")]
		public int LocalLeaderBoardsStanding { get; set; }

		/// <summary>
		/// This user's percentile position, between 0.0 (best) and 1.0 (worst).
		/// </summary>
		[JsonProperty("percentile")]
		public double Percentile { get; set; }

		/// <summary>
		/// This user's percentile rank, or Z if not applicable.
		/// </summary>
		[JsonProperty("percentile_rank")]
		public string PercentileRank { get; set; }
    
		/// <summary>
		/// The previous rank this user can achieve, if they lose more games, or null if unranked (or the worst rank).
		/// </summary>
		[JsonProperty("prev_rank")]
		public string PreviousRank { get; set; }

		/// <summary>
		/// The next rank this user can achieve, if they win more games, or null if unranked (or the best rank).
		/// </summary>
		[JsonProperty("next_rank")]
		public string NextRank { get; set; }

		/// <summary>
		/// The position of the worst player in the user's current rank. dip below them to go down a
		/// rank. -1 if unranked (or the worst rank).
		/// </summary>
		[JsonProperty("prev_at")]
		public int LowestTLRatingInRank { get; set; }

		/// <summary>
		/// The position of the best player in the user's current rank, surpass them to go up a rank.
		/// -1 if unranked (or the best rank).
		/// </summary>
		[JsonProperty("next_at")]
		public int HighestTLRatingInRank { get; set; }
	}
}