using System.Collections.Generic;

namespace TetrioStats.Api.Domain.Json.Leaderboards
{
	public class GlobalLeaderboardRankingResponse
		: TetrioApiResponseBase<
			GlobalLeaderboardRankingPayload, 
			List<UserLeaderboardRanking>>
	{
	}
}