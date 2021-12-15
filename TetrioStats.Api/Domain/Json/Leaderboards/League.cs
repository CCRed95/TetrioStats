using Newtonsoft.Json;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Domain.Json.Leaderboards
{
	// TODO this should be a base class of TetraLeagueStatistics
	public class League
	{
		[JsonProperty("gamesplayed")]
		public int GamesPlayed { get; set; }

		[JsonProperty("gameswon")]
		public int GamesWon { get; set; }

		[JsonProperty("rating")]
		public double TLRating { get; set; }

		[JsonProperty("glicko")]
		public double GlickoRating { get; set; }

		[JsonProperty("rd")]
		public double GlickoRatingDeviation { get; set; }

		[JsonProperty("rank")]
		public string UserRank { get; set; }

		[JsonProperty("apm")]
		public double AverageRollingAPM { get; set; }

		[JsonProperty("pps")]
		public double AverageRollingPPS { get; set; }

		[JsonProperty("vs")]
		public double AverageRollingVSScore { get; set; }

		[JsonProperty("decaying")]
		public bool IsDecaying { get; set; }
	}
}
