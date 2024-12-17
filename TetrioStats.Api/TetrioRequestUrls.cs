using System;
using Ccr.Scraping.API.Infrastructure;
using Ccr.Std.Core.Extensions;
using JetBrains.Annotations;
using TetrioStats.Api.Extensions;
using TetrioStats.Api.Http.Parameters;
using TetrioStats.Api.Infrastructure;
using TetrioStats.Core.Extensions;
using TetrioStats.Data.Enums;
using GameMode = TetrioStats.Data.Enums.GameMode;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api;

internal static class TetrioRequestUrls
{
	private const string coreApiDomain = "https://tetr.io/api/";
	private const string chApiDomain = "https://ch.tetr.io/api/";


	public static string FetchGeneralServerStatisticsUrl()
	{
		return new DomainFragment(chApiDomain)
			.Builder
			.WithPath("general")
			.WithPath("stats")
			.Build()
			.TrimEnd('/');
	}

	public static string FetchGeneralUserActivityDataUrl()
	{
		return new DomainFragment(chApiDomain)
			.Builder
			.WithPath("general")
			.WithPath("activity")
			.Build()
			.TrimEnd('/');
	}

	public static string FetchUserInfoUrl(
		[NotNull] string userID)
	{
		userID.IsNotNull(nameof(userID));

		return new DomainFragment(chApiDomain)
			.Builder
			.WithPath("users")
			.WithPath(userID.ToLower())
			.BuildUrl(false);
	}

	public static string FetchUserSearchUrl(
		[NotNull] string discordUserId)
	{
		discordUserId.IsNotNull(nameof(discordUserId));

		return new DomainFragment(chApiDomain)
			.Builder
			.WithPath("users")
			.WithPath("search")
			.WithPath(discordUserId)
			.BuildUrl(false);
	}

    public static string FetchUserHistoricalLeaderboardUrl(
		LeaderboardKind leaderboard,
		[NotNull] string season,
		[CanBeNull] Action<UserLeaderboardParameterBuilder> queryBuilderAction = null)
	{
		if (leaderboard != LeaderboardKind.League)
			throw new NotSupportedException(
				$"The {nameof(FetchUserHistoricalLeaderboardUrl)}(...) method currently only supports " +
				$"{nameof(LeaderboardKind.League).SQuote()} for the parameter {nameof(leaderboard).SQuote()}.");

		season.IsNotNull(nameof(season));

		var builder = UserLeaderboardParameterBuilder.Builder;
		queryBuilderAction?.Invoke(builder);
		var urlQuery = builder.Build();

		return new DomainFragment(chApiDomain)
			.Builder
			.WithPath("users")
			.WithPath("history")
			.WithPath($"{leaderboard:G}".ToLower())
			.WithPath($"{season}")
			.BuildUrl(false) + urlQuery;
	}

	public static string FetchAchievementsSummariesUrl(
		[NotNull] string userIdOrUsername)
	{
		userIdOrUsername.IsNotNull(nameof(userIdOrUsername));

		return new DomainFragment(chApiDomain)
			.Builder
			.WithPath("users")
			.WithPath(userIdOrUsername.ToLower())
			.WithPath("summaries")
			.WithPath("achievements")
			.BuildUrl(false);
	}

	public static string FetchAllSummariesUrl(
		[NotNull] string userIdOrUsername)
	{
		userIdOrUsername.IsNotNull(nameof(userIdOrUsername));

		return new DomainFragment(chApiDomain)
			.Builder
			.WithPath("users")
			.WithPath(userIdOrUsername.ToLower())
			.WithPath("summaries")
			.WithPath("all")
			.BuildUrl(false);
	}

	public static string FetchLeagueUserSummaryUrl(
		[NotNull] string userIdOrUsername)
	{
		userIdOrUsername.IsNotNull(nameof(userIdOrUsername));

		return new DomainFragment(chApiDomain)
			.Builder
			.WithPath("users")
			.WithPath(userIdOrUsername.ToLower())
			.WithPath("summaries")
			.WithPath("league")
			.BuildUrl(false);
	}

	public static string FetchBlitzSummaryUrl(
		[NotNull] string userIdOrUsername)
	{
		userIdOrUsername.IsNotNull(nameof(userIdOrUsername));

		return new DomainFragment(chApiDomain)
			.Builder
			.WithPath("users")
			.WithPath(userIdOrUsername.ToLower())
			.WithPath("summaries")
			.WithPath("blitz")
			.BuildUrl(false);
	}

	public static string FetchSprintSummaryUrl(
		[NotNull] string userIdOrUsername)
	{
		userIdOrUsername.IsNotNull(nameof(userIdOrUsername));

		return new DomainFragment(chApiDomain)
			.Builder
			.WithPath("users")
			.WithPath(userIdOrUsername.ToLower())
			.WithPath("summaries")
			.WithPath("40l")
			.BuildUrl(false);
	}

	public static string FetchZenithSummaryUrl(
		[NotNull] string userIdOrUsername)
	{
		userIdOrUsername.IsNotNull(nameof(userIdOrUsername));

		return new DomainFragment(chApiDomain)
			.Builder
			.WithPath("users")
			.WithPath(userIdOrUsername.ToLower())
			.WithPath("summaries")
			.WithPath("zenith")
			.BuildUrl(false);
	}

	public static string FetchZenithExSummaryUrl(
		[NotNull] string userIdOrUsername)
	{
		userIdOrUsername.IsNotNull(nameof(userIdOrUsername));

		return new DomainFragment(chApiDomain)
			.Builder
			.WithPath("users")
			.WithPath(userIdOrUsername.ToLower())
			.WithPath("summaries")
			.WithPath("zenithex")
			.BuildUrl(false);
	}

	public static string FetchZenSummaryUrl(
		[NotNull] string userIdOrUsername)
	{
		userIdOrUsername.IsNotNull(nameof(userIdOrUsername));

		return new DomainFragment(chApiDomain)
			.Builder
			.WithPath("users")
			.WithPath(userIdOrUsername.ToLower())
			.WithPath("summaries")
			.WithPath("zen")
			.BuildUrl(false);
	}

    public static string FetchAllNewsUrl(
        int? limit = null)
    {
        var fragment = new DomainFragment(chApiDomain)
            .Builder
            .WithPath("news")
            .Build();

        if (limit.HasValue)
            fragment += $"?limit={limit.Value}";

        return fragment;
    }

    public static string FetchLatestNewsInStreamUrl(
        string stream,
        int? limit = null)
    {
        var fragment = new DomainFragment(chApiDomain)
            .Builder
            .WithPath("news")
            .WithPath(stream);

        if (limit.HasValue)
        {
            fragment.WithParameter("limit", limit.Value.ToString());
        }
		
        return fragment.BuildUrl(false);
    }

    public static string FetchUserPersonalRecordsUrl(
		[NotNull] string userIdOrUsername,
		GameMode gameMode,
		Leaderboard leaderboard)
	{
		userIdOrUsername.IsNotNull(nameof(userIdOrUsername));

		return new DomainFragment(chApiDomain)
			.Builder
			.WithPath("users")
			.WithPath(userIdOrUsername.ToLower())
			.WithPath("records")
			.WithPath(gameMode.GetDescription())
			.WithPath(leaderboard.GetDescription())
			.BuildUrl(false);
	}

	public static string FetchUserLabsScoreFlowUrl(
		[NotNull] string userIdOrUsername,
		GameMode gameMode)
	{
		userIdOrUsername.IsNotNull(nameof(userIdOrUsername));

		return new DomainFragment(chApiDomain)
			.Builder
			.WithPath("labs")
			.WithPath("scoreflow")
			.WithPath(userIdOrUsername.ToLower())
			.WithPath($"{gameMode:g}".ToLower())
			.BuildUrl(false);
	}

	public static string FetchUserLabsLeagueFlowUrl(
		[NotNull] string userIdOrUsername)
	{
		userIdOrUsername.IsNotNull(nameof(userIdOrUsername));

		return new DomainFragment(chApiDomain)
			.Builder
			.WithPath("labs")
			.WithPath("leagueflow")
			.WithPath(userIdOrUsername.ToLower())
			.BuildUrl(false);
	}

	public static string FetchUserLabsLeagueRanksUrl()
	{
		return new DomainFragment(chApiDomain)
			.Builder
			.WithPath("labs")
			.WithPath("league_ranks")
			.BuildUrl(false);
	}

	public static string FetchAchievementInfoUrl(
		int achievementId)
	{
		return new DomainFragment(chApiDomain)
			.Builder
			.WithPath("achievements")
			.WithPath($"{achievementId}")
			.BuildUrl(false);
	}



	public static string FetchRibbonUrl()
	{
		return new DomainFragment(coreApiDomain)
			.Builder
			.WithPath("server")
			.WithPath("ribbon")
			.BuildUrl(false);
	}

	public static string ResolveUserUrl(
		[NotNull] string name)
	{
		return new DomainFragment(coreApiDomain)
			.Builder
			.WithPath("users")
			.WithPath(name)
			.WithPath("resolve")
			.BuildUrl(false);
	}

	public static string GetPublicRoomsUrl()
	{
		return new DomainFragment(coreApiDomain)
			.Builder
			.WithPath("rooms")
			.BuildUrl(false);
	}

	public static string FetchUsersMeUrl()
	{
		return new DomainFragment(coreApiDomain)
			.Builder
			.WithPath("users")
			.WithPath("me")
			.BuildUrl(false);
	}

	public static string FetchDirectMessagesUrl()
	{
		return new DomainFragment(coreApiDomain)
			.Builder
			.WithPath("dms")
			.BuildUrl(false);
	}

	public static string RelationshipsFriendUrl()
	{
		return new DomainFragment(coreApiDomain)
			.Builder
			.WithPath("relationships")
			.WithPath("friend")
			.BuildUrl(false);
	}

	public static string RelationshipsUnfriendUrl()
	{
		return new DomainFragment(coreApiDomain)
			.Builder
			.WithPath("relationships")
			.WithPath("remove")
			.BuildUrl(false);
	}
}