using System;
using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Json.TenchiStats
{
	public class TetraLeagueHistoricalStatistics
		: IEquatable<TetraLeagueHistoricalStatistics>
	{
		/// <summary>
		/// The amount of TETRA LEAGUE games played by this user.
		/// </summary>
		[JsonProperty("gamesplayed")]
		public int? GamesPlayed { get; set; }

		/// <summary>
		/// The amount of TETRA LEAGUE games won by this user.
		/// </summary>
		[JsonProperty("gameswon")]
		public int? GamesWon { get; set; }

		/// <summary>
		/// This user's TR (Tetra Rating), or -1 if less than 10 games were played.
		/// </summary>
		[JsonProperty("rating")]
		public double? TLRating { get; set; }

		/// <summary>
		/// This user's Glicko-2 rating.
		/// </summary>
		[JsonProperty("glicko")]
		public double? GlickoRating { get; set; }

		/// <summary>
		/// This user's Glicko-2 Rating Deviation. If over 100, this user is unranked.
		/// </summary>
		[JsonProperty("rd")]
		public double? GlickoRatingDeviation { get; set; }

		/// <summary>
		/// This user's letter rank. Z is unranked.
		/// </summary>
		[JsonProperty("rank")]
		public string UserRank { get; set; }

		/// <summary>
		/// This user's average APM (attack per minute) over the last 10 games.
		/// </summary>
		[JsonProperty("apm")]
		public double? AverageRollingAPM { get; set; }

		/// <summary>
		/// This user's average PPS (pieces per second) over the last 10 games.
		/// </summary>
		[JsonProperty("pps")]
		public double? AverageRollingPPS { get; set; }

		/// <summary>
		/// This user's average VS (versus score) over the last 10 games.
		/// </summary>
		[JsonProperty("vs")]
		public double? AverageRollingVSScore { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("decaying")]
		public bool? IsDecaying { get; set; }


		public bool Equals(TetraLeagueHistoricalStatistics other)
		{
			if (ReferenceEquals(null, other))
				return false;

			if (ReferenceEquals(this, other))
				return true;

			return GamesPlayed == other.GamesPlayed
				&& GamesWon == other.GamesWon
				&& Nullable.Equals(TLRating, other.TLRating)
				&& Nullable.Equals(GlickoRating, other.GlickoRating)
				&& Nullable.Equals(GlickoRatingDeviation, other.GlickoRatingDeviation)
				&& UserRank == other.UserRank
				&& Nullable.Equals(AverageRollingAPM, other.AverageRollingAPM)
				&& Nullable.Equals(AverageRollingPPS, other.AverageRollingPPS)
				&& Nullable.Equals(AverageRollingVSScore, other.AverageRollingVSScore)
				&& IsDecaying == other.IsDecaying;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;

			if (ReferenceEquals(this, obj))
				return true;

			if (obj.GetType() != GetType())
				return false;

			return Equals((TetraLeagueHistoricalStatistics)obj);
		}

		public override int GetHashCode()
		{
			var hashCode = new HashCode();

			hashCode.Add(GamesPlayed);
			hashCode.Add(GamesWon);
			hashCode.Add(TLRating);
			hashCode.Add(GlickoRating);
			hashCode.Add(GlickoRatingDeviation);
			hashCode.Add(UserRank);
			hashCode.Add(AverageRollingAPM);
			hashCode.Add(AverageRollingPPS);
			hashCode.Add(AverageRollingVSScore);
			hashCode.Add(IsDecaying);

			return hashCode.ToHashCode();
		}
	}
}