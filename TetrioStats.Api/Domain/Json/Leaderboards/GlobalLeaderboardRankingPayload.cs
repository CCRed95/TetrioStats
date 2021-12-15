using System.Collections.Generic;
using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Json.Leaderboards
{
	public class GlobalLeaderboardRankingPayload
		: ITetrioResponsePayload<List<UserLeaderboardRanking>>
	{
		[JsonProperty("users")]
		public List<UserLeaderboardRanking> Payload { get; }
			= new List<UserLeaderboardRanking>();
	}
}