using TetrioStats.Api.Domain.Labs;
using TetrioStats.Api.Infrastructure;

// ReSharper disable StringLiteralTypo

namespace TetrioStats.Api.Tests;

public class TetrioApiClientTests
{
	private static readonly TetrioApiClient _client = new();

	
	//[Test]
	//public async Task CanFetchPublicRoomData()
	//{
	//	var roomListData = await _client.FetchPublicRoomListAsync();

	//	Assert.That(roomListData.WasSuccessful, Is.True);
	//}

	[Test]
	public async Task CanFetchGeneralServerStatistics()
	{
		var serverStats = await _client.General.FetchGeneralServerStatisticsAsync();

		Assert.Multiple(() =>
		{
			Assert.That(serverStats.Data.UserCount, Is.GreaterThan(9700000L));
			Assert.That(serverStats.Data.AnonCount, Is.GreaterThan(7000000L));
			Assert.That(serverStats.Data.TotalAccounts, Is.GreaterThan(17000000L));
			//Assert.That(serverStats.Data.GameTime, Is.GreaterThan(10000000000.0D));
			Assert.That(serverStats.Data.Inputs, Is.GreaterThan(420000000000L));
		});
	}

	//[Test]
	//public async Task CanFetchGeneralUserActivity()
	//{
	//       // FetchGeneralUserActivityDataAsync
	//       var generalUserActivity = await _client.General.FetchGeneralUserActivityDataAsync();

	//	Assert.Multiple(() =>
	//	{
	//		Assert.That(generalUserActivity.Content.UserActivityData, Has.Count.EqualTo(5760));
	//		Assert.That(generalUserActivity.Content.UserActivityData.Average(), Is.GreaterThan(1));
	//	});
	//}

	[Test]
	public async Task CanFetchUserSummary40Lines()
	{
		// FetchUserSummary40LinesAsync
		var userSummary40l = await _client.Users.Summaries.FetchSprintSummaryAsync("ccred95");

		Assert.Multiple(() =>
        {
            Assert.That(userSummary40l?.Data?.Record, Is.Not.Null);

			//Assert.That(userSummary40l.Data..Username, Is.EqualTo("ccred95"));
			Assert.That(userSummary40l.Data.Record.Results.Stats.Clears.Singles, Is.GreaterThan(0L));
			Assert.That(userSummary40l.Data.Record.Results.AggregateStats.PPS, Is.GreaterThan(0L));
			Assert.That(userSummary40l.Data.Record.Results.AggregateStats.APM, Is.GreaterThan(0L));
			Assert.That(userSummary40l.Data.Rank, Is.GreaterThanOrEqualTo(1L));
		});
	}

	[Test]
	public async Task CanFetchUserSummaryTetraLeague()
	{
		// FetchUserSummaryTetraLeagueAsync
		var userSummaryTl = await _client.Users.Summaries.FetchLeagueSummaryAsync("ccred95");

		Assert.Multiple(() =>
		{
			Assert.That(userSummaryTl.Data.GamesPlayed, Is.GreaterThan(0));
			Assert.That(userSummaryTl.Data.Percentile, Is.GreaterThan(0D));
			Assert.That(userSummaryTl.Data.APM, Is.GreaterThan(0D));
		});
	}

	//[Test]
	//public async Task CanFetchUserPersonalRecords()
	//{
	//	// FetchUserPersonalRecordsAsync
	//	var userBlitzRecords = await _client.Records.FetchRecordsAsync<PersonalBlitzRecordResponse, BlitzRecord>(
	//		"ccred95", GameMode.Blitz, Leaderboard.Top);

	//	Assert.Multiple(() =>
	//	{
	//		Assert.That(userBlitzRecords.Data.Entries.Count, Is.GreaterThan(0));
	//		Assert.That(userBlitzRecords.Data.Entries[0].GameMode, Is.EqualTo("blitz"));
	//		Assert.That(userBlitzRecords.Data.Entries[0].Results.AggregateStats.PPS, Is.GreaterThan(0.1F));
	//		Assert.That(userBlitzRecords.Data.Entries[0].Results.Stats.FinalTime, Is.GreaterThan(10.0F));
	//		Assert.That(userBlitzRecords.Data.Entries[0].Results.Stats.PiecesPlaced, Is.GreaterThan(20));
	//	});
	//}

	[Test]
	public async Task CanFetchUserMe()
	{
		var userMeResponse = await _client.Users.FetchUserMeAsync();

		Assert.Multiple(() =>
		{
			Assert.That(userMeResponse.WasSuccessful, Is.EqualTo(true));
		});
	}

	[Test]
	public async Task CanFetchUserLabsLeagueFlow()
	{
		var userLabelsLeagueFlow = await _client.Labs.FetchUserLeagueFlow("ccred95");

		Assert.Multiple(() =>
		{
			Assert.That(userLabelsLeagueFlow.WasSuccessful, Is.EqualTo(true));
			Assert.That(userLabelsLeagueFlow.Data.StartTime, Is.GreaterThan(DateTime.Parse("2024-08-01")));
			Assert.That(userLabelsLeagueFlow.Data.Points.Count, Is.GreaterThan(0));
			Assert.That(userLabelsLeagueFlow.Data.Points[0].UserTrAfterMatch, Is.GreaterThan(0F));
			Assert.That(userLabelsLeagueFlow.Data.Points[0].OpponentTrBeforeMatch, Is.GreaterThan(0F));
			Assert.That(userLabelsLeagueFlow.Data.Points[0].GameResult, Is.Not.EqualTo(LeagueFlowMatchResult.Unknown));
		});
	}

	[Test]
	public async Task CanFetchUserHistoricalLeaderboard()
	{
		var season1HistoricalLeaderboard = await _client.Users.FetchUserHistoricalLeaderboardAsync(
			LeaderboardKind.League, "1",
			t => t.WithCountryConstraint("CA").WithLimit(30));

		Assert.Multiple(() =>
		{
			Assert.That(season1HistoricalLeaderboard.WasSuccessful, Is.EqualTo(true));
			Assert.That(season1HistoricalLeaderboard.Data.Entries.Count, Is.EqualTo(30));
			Assert.That(season1HistoricalLeaderboard.Data.Entries[0].AverageAPM, Is.GreaterThan(1d));
			Assert.That(season1HistoricalLeaderboard.Data.Entries[0].AveragePPS, Is.GreaterThan(0F));
			Assert.That(season1HistoricalLeaderboard.Data.Entries[0].CountryCode, Is.EqualTo("CA"));
			Assert.That(season1HistoricalLeaderboard.Data.Entries[0].GamesWon, Is.GreaterThan(1));
		});

		var highestPrisecter = season1HistoricalLeaderboard.Data.Entries.Last().Prisecter;

		var season1HistoricalLeaderboardPage2 = await _client.Users.FetchUserHistoricalLeaderboardAsync(
			LeaderboardKind.League, "1",
			t => t.WithCountryConstraint("CA")
				.WithLimit(10)
				.WithLowerBoundConstraint(highestPrisecter));

		Assert.Multiple(() =>
		{
			Assert.That(season1HistoricalLeaderboardPage2.WasSuccessful, Is.EqualTo(true));
			Assert.That(season1HistoricalLeaderboardPage2.Data.Entries.Count, Is.EqualTo(10));
		});
	}
}