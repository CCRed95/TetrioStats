using System;
using System.Buffers;
using System.Linq;
using System.Threading.Tasks;
using Ccr.Std.Core.Extensions;
using JetBrains.Annotations;
using Newtonsoft.Json;
using TetrioStats.Api.Domain;
using TetrioStats.Api.Domain.Achievements;
using TetrioStats.Api.Domain.General;
using TetrioStats.Api.Domain.Labs;
using TetrioStats.Api.Domain.News;
using TetrioStats.Api.Domain.Users;
using TetrioStats.Api.Domain.Users.Summaries;
using TetrioStats.Api.Domain.Users.UserRecords;
using TetrioStats.Api.Http.Parameters;
using TetrioStats.Api.Infrastructure;
using TetrioStats.Api.Utilities;
using TetrioStats.Data.Enums;
using GameMode = TetrioStats.Data.Enums.GameMode;

#pragma warning disable CA1822

namespace TetrioStats.Api;

/// <summary>
/// Root Tetrio Api Client
/// </summary>
public class TetrioApiClient
{
	/// <summary>
	/// General Client Api
	/// </summary>
	public TetrioGeneralApi General { get; } = new();

	/// <summary>
	/// Users Client Api
	/// </summary>
	public TetrioUsersApi Users { get; } = new();
	
	/// <summary>
	/// Records Client Api
	/// </summary>
	public TetrioUserPersonalRecordsApi Records { get; } = new();

    /// <summary>
    /// News Client Api
    /// </summary>
    public TetrioNewsApi News { get; } = new();

    /// <summary>
    /// Labs Client Api
    /// </summary>
    public TetrioLabsApi Labs { get; } = new();

	/// <summary>
	/// Achievements Client Api
	/// </summary>
	public TetrioAchievementsApi Achievements { get; } = new();
}


/// <summary>
/// General Server Info Api Class
/// </summary>
public class TetrioGeneralApi
{
	/// <summary>
	/// Fetches General Server Statistics.
	/// </summary>
	/// <returns>
	/// Returns General Server Statistics.
	/// </returns>
	public async Task<GeneralServerStatsResponse> FetchGeneralServerStatisticsAsync()
	{
		var url = TetrioRequestUrls.FetchGeneralServerStatisticsUrl();

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithAuth()
			.WithJsonAccept();

		var response = await httpClient.SendAsync(request);
		var jsonContent = await response.Content.ReadAsStringAsync();

		var generalServerStatisticsResponse = JsonConvert
			.DeserializeObject<GeneralServerStatsResponse>(jsonContent);

		return generalServerStatisticsResponse;
	}

	/// <summary>
	/// Fetches a graph of user activity over the last 2 days. A user is seen as active if they logged
	/// in or received XP within the last 30 minutes.
	/// </summary>
	/// <returns>
	/// Returns a graph of user activity over the last 2 days. A user is seen as active if they logged
	/// in or received XP within the last 30 minutes.
	/// </returns>
	public async Task<ActivityData> FetchGeneralUserActivityDataAsync()
	{
		var url = TetrioRequestUrls.FetchGeneralUserActivityDataUrl();

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithJsonAccept();

		var response = await httpClient.SendAsync(request);
		var jsonContent = await response.Content.ReadAsStringAsync();

		var generalServerStatisticsResponse = JsonConvert
			.DeserializeObject<ActivityData>(jsonContent);

		return generalServerStatisticsResponse;
	}
}


public class TetrioUsersApi
{
	/// <summary>
	/// User Summaries nested API.
	/// </summary>
	public TetrioUserSummariesApi Summaries { get; } = new();


	/// <summary>
	/// Fetches an object describing the user in detail.
	/// </summary>
	/// <param name="userIdOrUsername">
	/// The username or user ID to look up.
	/// </param>
	/// <returns>
	/// Returns an object describing the user in detail.
	/// </returns>
	public async Task<UserInfoResponse> FetchUserInfoAsync(
		[NotNull] string userIdOrUsername)
	{
		var url = TetrioRequestUrls.FetchUserInfoUrl(userIdOrUsername);

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithAuth()
			.WithJsonAccept();

		var response = await httpClient.SendAsync(request);
		var jsonContent = await response.Content.ReadAsStringAsync();

		var userDataResponse = JsonConvert
			.DeserializeObject<UserInfoResponse>(jsonContent);

		return userDataResponse;
	}

	/// <summary>
	/// Fetches an object describing the user found based on the provided <paramref name="discordUserId"/>, or
	/// <langword>null</langword> if none were found.
	/// </summary>
	/// <param name="discordUserId">
	/// The Discord User ID associated with the TETR.IO account to look up.
	/// </param>
	/// <returns>
	/// Returns an object describing the user found based on the provided <paramref name="discordUserId"/>, or
	/// <langword>null</langword> if none were found.
	/// </returns>
	/// <remarks>
	/// Support for searching for the other social links will be added to the TETR.IO API in the near future.
	/// </remarks>
	public async Task<UserSearchResponse> FetchUserSearchAsync(
		[NotNull] string discordUserId)
	{
		var url = TetrioRequestUrls.FetchUserSearchUrl(discordUserId);

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithAuth()
			.WithJsonAccept();

		var response = await httpClient.SendAsync(request);
		var jsonContent = await response.Content.ReadAsStringAsync();

		var userDataResponse = JsonConvert
			.DeserializeObject<UserSearchResponse>(jsonContent);

		return userDataResponse;
	}

	/// <summary>
    /// Fetches an array of historical user blobs fulfilling the search criteria.
    /// </summary>
    /// <param name="leaderboard">
    /// The leaderboard to sort users by. Must be <see cref="LeaderboardKind.League"/>.
    /// </param>
    /// <param name="season">
    /// The season to look up.
    /// </param>
    /// <param name="queryBuilderAction">
    /// The parameter builder actions.
    /// </param>
    /// <returns>
    /// Returns an array of historical user blobs fulfilling the search criteria.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// Only <see cref="LeaderboardKind.League"/> is currently supported. If the value is anything
    /// else, then it will throw an exception.
    /// </exception>
    public async Task<UserHistoricalLeaderboardResponse> FetchUserHistoricalLeaderboardAsync(
		LeaderboardKind leaderboard,
		[NotNull] string season,
		[CanBeNull] Action<UserLeaderboardParameterBuilder> queryBuilderAction = null)
	{
		if (leaderboard != LeaderboardKind.League)
			throw new NotSupportedException(
				$"The {nameof(FetchUserHistoricalLeaderboardAsync)}(...) method currently only supports " +
				$"{nameof(LeaderboardKind.League).SQuote()} for the parameter {nameof(leaderboard).SQuote()}.");

		var url = TetrioRequestUrls.FetchUserHistoricalLeaderboardUrl(
			leaderboard, season, queryBuilderAction);

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithAuth()
			.WithJsonAccept();

		var response = await httpClient.SendAsync(request);
		var jsonContent = await response.Content.ReadAsStringAsync();

		var historicalLeaderboardResponse = JsonConvert
			.DeserializeObject<UserHistoricalLeaderboardResponse>(jsonContent);

		return historicalLeaderboardResponse;
	}

	public async Task<UserMeResponse> FetchUserMeAsync()
	{
		var url = TetrioRequestUrls.FetchUsersMeUrl();

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithAuth()
			.WithTheoryPackAccept();

		var response = await httpClient.SendAsync(request);
		var byteContents = await response.Content.ReadAsByteArrayAsync();

        throw new NotImplementedException();
        //var ros = new ReadOnlySequence<byte>(byteContents);

        //var json = Pack.ConvertToJson(ros);

        //var userMeResponseJson = Pack.Unpack<UserMeResponse>(byteContents);//).JsonFromBytes(byteContents)};
        //var e = byteContents.Aggregate("[", (current, b) => current + ($"{b}, "));


        //var userMeResponse = JsonConvert
        //	.DeserializeObject<UserMeResponse>(json);

        //return userMeResponse;
    }
}


public class TetrioLabsApi
{
	/// <summary>
	/// Fetches a condensed graph of all the user's matches in TETRA LEAGUE.
	/// </summary>
	/// <param name="userIdOrUsername">
	/// The lowercase username or user ID to look up.
	/// </param>
	/// <returns>
	/// Returns a condensed graph of all the user's matches in TETRA LEAGUE.
	/// </returns>
	public async Task<LeagueFlowResponse> FetchUserLeagueFlow(
		[NotNull] string userIdOrUsername)
	{
		var url = TetrioRequestUrls.FetchUserLabsLeagueFlowUrl(userIdOrUsername);

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithAuth()
			.WithJsonAccept();

		var response = await httpClient.SendAsync(request);
		var jsonContent = await response.Content.ReadAsStringAsync();

		var leagueFlowUserResponse = JsonConvert
			.DeserializeObject<LeagueFlowResponse>(jsonContent);

		return leagueFlowUserResponse;
	}

	/// <summary>
	/// Fetches a view over all TETRA LEAGUE ranks and their metadata.
	/// </summary>
	/// <returns>
	/// Returns a view over all TETRA LEAGUE ranks and their metadata.
	/// </returns>
	public async Task<LeagueRanksResponse> FetchLeagueRanks()
	{
		var url = TetrioRequestUrls.FetchUserLabsLeagueRanksUrl();

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithAuth()
			.WithJsonAccept();

		var response = await httpClient.SendAsync(request);
		var jsonContent = await response.Content.ReadAsStringAsync();

		var leagueRanksResponse = JsonConvert
			.DeserializeObject<LeagueRanksResponse>(jsonContent);

		return leagueRanksResponse;
	}

	/// <summary>
	/// Fetches a condensed graph of all the user's records in the provided <paramref name="gameMode"/>.
	/// </summary>
	/// <param name="userIdOrUsername"></param>
	/// <param name="gameMode"></param>
	/// <returns></returns>
	public async Task<ScoreFlowResponse> FetchUserScoreFlow(
		[NotNull] string userIdOrUsername,
		GameMode gameMode)
	{
		var url = TetrioRequestUrls.FetchUserLabsScoreFlowUrl(userIdOrUsername, gameMode);

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithAuth()
			.WithJsonAccept();

		var response = await httpClient.SendAsync(request);
		var jsonContent = await response.Content.ReadAsStringAsync();

		var userLabsScoreFlowResponse = JsonConvert
			.DeserializeObject<ScoreFlowResponse>(jsonContent);

		return userLabsScoreFlowResponse;
	}
}

public class TetrioUserPersonalRecordsApi
{
	public async Task<TResponse> FetchRecordsAsync<TResponse, TRecord>(
		[NotNull] string userIdOrUsername,
		GameMode gameMode,
		Leaderboard leaderboard)
			where TRecord : PersonalUserRecord
			where TResponse : TetrioApiResponse<PersonalUserRecord<TRecord>>
	{
		var url = TetrioRequestUrls.FetchUserPersonalRecordsUrl(
			userIdOrUsername, gameMode, leaderboard);

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithAuth()
			.WithJsonAccept();

		var response = await httpClient.SendAsync(request);
		var jsonContent = await response.Content.ReadAsStringAsync();

		var userPersonalRecords = JsonConvert
			.DeserializeObject<TResponse>(jsonContent);

		return userPersonalRecords;
	}
}

public class TetrioNewsApi
{
    /// <summary>
    /// Fetches the latest news items in any stream.
    /// </summary>
    /// <param name="limit">
    /// The amount of entries to return, between 1 and 100. 25 by default.
    /// </param>
    /// <returns>
    /// Returns the latest news items in any stream.
    /// </returns>
    public async Task<LatestNewsResponse> FetchAllLatestNewsAsync(
        int? limit)
    {
        var url = TetrioRequestUrls.FetchAllNewsUrl(limit);

        using var httpClient = new TetrioHttpClient();
        using var request = HttpRequest.Get(url)
            .WithAuth()
            .WithJsonAccept();

        var response = await httpClient.SendAsync(request);
        var jsonContent = await response.Content.ReadAsStringAsync();

        var allLatestNewsResponse = JsonConvert
            .DeserializeObject<LatestNewsResponse>(jsonContent);

        return allLatestNewsResponse;
    }

    public async Task<LatestNewsResponse> FetchLatestNewsInStreamAsync(
        string stream,
        int? limit = null)
    {
        var url = TetrioRequestUrls.FetchLatestNewsInStreamUrl(stream, limit);

        using var httpClient = new TetrioHttpClient();
        using var request = HttpRequest.Get(url)
            .WithAuth()
            .WithJsonAccept();

        var response = await httpClient.SendAsync(request);
        var jsonContent = await response.Content.ReadAsStringAsync();

        var allLatestNewsResponse = JsonConvert
            .DeserializeObject<LatestNewsResponse>(jsonContent);

        return allLatestNewsResponse;
    }
}

public class TetrioUserSummariesApi
{
	public async Task<AchievementsSummaryResponse> FetchAchievementsSummaryAsync(
		[NotNull] string userIdOrUsername)
	{
		var url = TetrioRequestUrls.FetchAchievementsSummariesUrl(userIdOrUsername);

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithAuth()
			.WithJsonAccept();

		var response = await httpClient.SendAsync(request);
		var jsonContent = await response.Content.ReadAsStringAsync();

		var allSummariesResponse = JsonConvert
			.DeserializeObject<AchievementsSummaryResponse>(jsonContent);

		return allSummariesResponse;
	}

	public async Task<AllSummariesResponse> FetchAllSummariesAsync(
		[NotNull] string userIdOrUsername)
	{
		var url = TetrioRequestUrls.FetchAllSummariesUrl(userIdOrUsername);

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithAuth()
			.WithJsonAccept();

		var response = await httpClient.SendAsync(request);
		var jsonContent = await response.Content.ReadAsStringAsync();

		var allSummariesResponse = JsonConvert
			.DeserializeObject<AllSummariesResponse>(jsonContent);

		return allSummariesResponse;
	}

	public async Task<SprintSummaryResponse> FetchSprintSummaryAsync(
		[NotNull] string userIdOrUsername)
	{
		var url = TetrioRequestUrls.FetchSprintSummaryUrl(userIdOrUsername);

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithAuth()
			.WithJsonAccept();

		var response = await httpClient.SendAsync(request);
		var jsonContent = await response.Content.ReadAsStringAsync();

		var sprintSummaryResponse = JsonConvert
			.DeserializeObject<SprintSummaryResponse>(jsonContent);

		return sprintSummaryResponse;
	}

	public async Task<BlitzSummaryResponse> FetchBlitzSummaryAsync(
		[NotNull] string userIdOrUsername)
	{
		var url = TetrioRequestUrls.FetchBlitzSummaryUrl(userIdOrUsername);

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithAuth()
			.WithJsonAccept();

		var response = await httpClient.SendAsync(request);
		var jsonContent = await response.Content.ReadAsStringAsync();

		var blitzSummaryResponse = JsonConvert
			.DeserializeObject<BlitzSummaryResponse>(jsonContent);

		return blitzSummaryResponse;
	}

	public async Task<ZenithSummaryResponse> FetchZenithSummaryAsync(
		[NotNull] string userIdOrUsername)
	{
		var url = TetrioRequestUrls.FetchZenithSummaryUrl(userIdOrUsername);

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithAuth()
			.WithJsonAccept();

		var response = await httpClient.SendAsync(request);
		var jsonContent = await response.Content.ReadAsStringAsync();

		var zenithSummaryResponse = JsonConvert
			.DeserializeObject<ZenithSummaryResponse>(jsonContent);

		return zenithSummaryResponse;
	}

	public async Task<ZenithExSummaryResponse> FetchZenithExSummaryAsync(
		[NotNull] string userIdOrUsername)
	{
		var url = TetrioRequestUrls.FetchZenithExSummaryUrl(userIdOrUsername);

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithAuth()
			.WithJsonAccept();

		var response = await httpClient.SendAsync(request);
		var jsonContent = await response.Content.ReadAsStringAsync();

		var zenithExSummaryResponse = JsonConvert
			.DeserializeObject<ZenithExSummaryResponse>(jsonContent);

		return zenithExSummaryResponse;
	}

	public async Task<LeagueSummaryResponse> FetchLeagueSummaryAsync(
		[NotNull] string userIdOrUsername)
	{
		var url = TetrioRequestUrls.FetchLeagueUserSummaryUrl(userIdOrUsername);

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithAuth()
			.WithJsonAccept();

		var response = await httpClient.SendAsync(request);
		var jsonContent = await response.Content.ReadAsStringAsync();

		var leagueSummaryResponse = JsonConvert
			.DeserializeObject<LeagueSummaryResponse>(jsonContent);

		return leagueSummaryResponse;
	}

	public async Task<ZenSummaryResponse> FetchZenSummaryAsync(
		[NotNull] string userIdOrUsername)
	{
		var url = TetrioRequestUrls.FetchZenSummaryUrl(userIdOrUsername);

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithAuth()
			.WithJsonAccept();

		var response = await httpClient.SendAsync(request);
		var jsonContent = await response.Content.ReadAsStringAsync();

		var zenSummaryResponse = JsonConvert
			.DeserializeObject<ZenSummaryResponse>(jsonContent);

		return zenSummaryResponse;
	}
}


public class TetrioAchievementsApi
{
	/// <summary>
	/// Fetches data about the achievement itself, its cutoffs, and its leaderboard.
	/// </summary>
	/// <param name="achievementId">
	/// The achievement ID to look up.
	/// </param>
	/// <returns>
	/// Returns data about the achievement itself, its cutoffs, and its leaderboard.
	/// </returns>
	public async Task<AchievementInfoResponse> FetchAchievementInfoAsync(
		int achievementId)
	{
		var url = TetrioRequestUrls.FetchAchievementInfoUrl(achievementId);

		using var httpClient = new TetrioHttpClient();
		using var request = HttpRequest.Get(url)
			.WithAuth()
			.WithJsonAccept();

		var response = await httpClient.SendAsync(request);
		var jsonContent = await response.Content.ReadAsStringAsync();

		var achievementInfoResponse = JsonConvert
			.DeserializeObject<AchievementInfoResponse>(jsonContent);

		return achievementInfoResponse;
	}
}