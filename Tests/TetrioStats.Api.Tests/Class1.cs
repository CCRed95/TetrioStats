
//using System.Diagnostics;
//using TetrioStats.Api.V2.Http.Clients;
//using TetrioStats.Api.V2.Http.Parameters;
//using TetrioStats.Api.V2.Models;

//namespace TetrioStats.Api.Tests;
//{
//	[TestFixture]
//public class ApiTests
//{
//	private static InMemoryRequestClient _client;
//	private static TaskScheduler _taskScheduler;

//	/// <summary>
//	/// Retrieves or initializes the in-memory HTTP client.
//	/// </summary>
//	private static InMemoryRequestClient GetClient()
//	{
//		if (_client == null)
//		{
//			Debug.WriteLine("CREATED CLIENT AGAIN WEEWOO");
//			_client = new InMemoryHttpClient();
//		}
//		return _client;
//	}

//	/// <summary>
//	/// Ensures the task scheduler is initialized.
//	/// </summary>
//	private static TaskScheduler GetTaskScheduler()
//	{
//		if (_taskScheduler == null)
//		{
//			_taskScheduler = TaskScheduler.Default;
//		}
//		return _taskScheduler;
//	}

//	private static void TestOkSuccessIsSome<T>(Packet<T> packet)
//	{
//		Assert.IsTrue(packet.Success);
//		Assert.IsNotNull(packet.Data, "Data should not be null.");
//		Assert.IsNotNull(packet.Cache, "Cache should not be null.");
//		Assert.IsNull(packet.Error, "Error should be null.");
//	}

//	private static void TestOkFailureIsSome<T>(Packet<T> packet)
//	{
//		Assert.IsFalse(packet.Success, "Request was unexpectedly successful.");
//		Assert.IsNull(packet.Data, "Data should be null.");
//		Assert.IsNull(packet.Cache, "Cache should be null.");
//		Assert.IsNotNull(packet.Error, "Error should not be null.");
//	}

//	private static void TestOkSuccessIsNone<T>(Packet<T> packet)
//	{
//		Assert.IsTrue(packet.Success, "Request was not successful.");
//		Assert.IsNull(packet.Data, "Data should be null.");
//		Assert.IsNotNull(packet.Cache, "Cache should not be null.");
//	}

//	[Test]
//	public async Task FetchGeneralStats()
//	{
//		var client = GetClient();
//		TestOkSuccessIsSome(await client.FetchGeneralStats());
//		TestOkSuccessIsSome(await client.FetchGeneralStats());
//	}

//	[Test]
//	public async Task FetchGeneralActivity()
//	{
//		var client = GetClient();
//		TestOkSuccessIsSome(await client.FetchGeneralActivity());
//		TestOkSuccessIsSome(await client.FetchGeneralActivity());
//	}

//	[Test]
//	public async Task FetchUserInfo()
//	{
//		var client = GetClient();
//		TestOkSuccessIsSome(await client.FetchUserInfo("taka"));
//	}

//	[Test]
//	public async Task FetchFounderInfo()
//	{
//		var client = GetClient();
//		TestOkSuccessIsSome(await client.FetchUserInfo("osk"));
//	}

//	[Test]
//	public async Task FailFetchUserInfo()
//	{
//		var client = GetClient();
//		TestOkFailureIsSome(await client.FetchUserInfo("INVALID_USER_ID"));
//	}

//	[Test]
//	public async Task FetchUserSummaries()
//	{
//		var client = GetClient();
//		TestOkSuccessIsSome(await client.FetchUserSummaries("taka"));
//		TestOkSuccessIsSome(await client.FetchUserSummaries("taka"));
//	}

//	[Test]
//	public async Task SearchDiscordUser()
//	{
//		var client = GetClient();
//		TestOkSuccessIsSome(await client.SearchDiscordUser("434626996262273038"));
//		TestOkSuccessIsSome(await client.SearchDiscordUser("434626996262273038"));
//	}

//	[Test]
//	public async Task SearchInvalidUser()
//	{
//		var client = GetClient();
//		TestOkSuccessIsNone(await client.SearchDiscordUser("INVALID_DISCORD_ID"));
//	}

//	[Test]
//	public async Task FetchLeaderboardNoQuery()
//	{
//		var client = GetClient();
//		TestOkSuccessIsSome(await client.FetchLeaderboard(LeaderboardType.League, ValueBoundQuery.None, null));
//		TestOkSuccessIsSome(await client.FetchLeaderboard(LeaderboardType.League, ValueBoundQuery.None, null));
//	}

//	[Test]
//	public async Task FetchLeaderboardCountryQuery()
//	{
//		var client = GetClient();
//		TestOkSuccessIsSome(await client.FetchLeaderboard(
//				LeaderboardType.League,
//				new ValueBoundQuery.NotBound { Country = "fr" },
//				null));
//		TestOkSuccessIsSome(await client.FetchLeaderboard(
//				LeaderboardType.League,
//				new ValueBoundQuery.NotBound { Country = "fr" },
//				null));
//	}

//	[Test]
//	public async Task FetchNews()
//	{
//		var client = GetClient();
//		TestOkSuccessIsSome(await client.FetchNews(null));
//		TestOkSuccessIsSome(await client.FetchNews(null));
//	}

//	[Test]
//	public async Task FetchLatestNews()
//	{
//		var client = GetClient();
//		TestOkSuccessIsSome(await client.FetchLatestNews("global", null));
//		TestOkSuccessIsSome(await client.FetchLatestNews("global", null));
//	}

//	[Test]
//	public async Task FetchScoreFlow()
//	{
//		var client = GetClient();
//		TestOkSuccessIsSome(await client.FetchScoreFlow("taka", "40l"));
//		TestOkSuccessIsSome(await client.FetchScoreFlow("taka", "40l"));
//	}

//	[Test]
//	public async Task FetchLeagueFlow()
//	{
//		var client = GetClient();
//		TestOkSuccessIsSome(await client.FetchLeagueFlow("taka"));
//		TestOkSuccessIsSome(await client.FetchLeagueFlow("taka"));
//	}

//	[Test]
//	public async Task FetchLeagueRanks()
//	{
//		var client = GetClient();
//		TestOkSuccessIsSome(await client.FetchLeagueRanks());
//		TestOkSuccessIsSome(await client.FetchLeagueRanks());
//	}

//	[Test]
//	public async Task FetchAchievementInfo()
//	{
//		var client = GetClient();
//		TestOkSuccessIsSome(await client.FetchAchievementInfo("2"));
//		TestOkSuccessIsSome(await client.FetchAchievementInfo("2"));
//	}
//}
//}
