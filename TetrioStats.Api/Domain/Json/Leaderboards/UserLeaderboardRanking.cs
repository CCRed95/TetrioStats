using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Json.Leaderboards
{
	public class UserLeaderboardRanking
	{
		[JsonProperty("_id")]
		public string ID { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("role")]
		public string Role { get; set; }

		[JsonProperty("xp")]
		public int XP { get; set; }

		[JsonProperty("league")]
		public League League { get; set; }

		[JsonProperty("supporter")]
		public bool Supporter { get; set; }

		[JsonProperty("verified")]
		public bool Verified { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }
	}
}