using TetrioStats.Data.Enums;

namespace TetrioStats.Api.Tests;

[AttributeUsage(AttributeTargets.Class)]
public class ApiGroupNameAttribute(string apiGroupName)
        : Attribute
{
    public string ApiGroupName { get; } = apiGroupName;
}

[ApiGroupName("Root")]
public class TetrioRootApiTests
    : TetrioApiTestsBase
{

}

[ApiGroupName("General")]
public class TetrioGeneralApiTests
    : TetrioApiTestsBase
{
    [Test]
    public async Task CanFetchServerStatistics()
    {
        var serverStats = await client.General.FetchGeneralServerStatisticsAsync();

        Assert.Multiple(() =>
        {
            Assert.That(serverStats.WasSuccessful, Is.True);
            Assert.That(serverStats.Data.UserCount, Is.GreaterThan(9700000L));
            Assert.That(serverStats.Data.AnonCount, Is.GreaterThan(7000000L));
            Assert.That(serverStats.Data.TotalAccounts, Is.GreaterThan(17000000L));
            Assert.That(serverStats.Data.GameTime, Is.GreaterThan(10000000000.0D));
            Assert.That(serverStats.Data.Inputs, Is.GreaterThan(420000000000L));
        });
    }

    [Test]
    public async Task CanFetchGeneralActivityActivity()
    {
        var generalActivity = await client.General.FetchGeneralUserActivityDataAsync();

        //Assert.Multiple(() =>
        //{
        //    Assert.That(generalActivity.Data.UserActivityData, Has.Count.EqualTo(5760));
        //    Assert.That(generalActivity.Data.UserActivityData.Average(), Is.GreaterThan(1));

        //    Assert.That(generalActivity.Content.Id, Is.EqualTo("5ea516e66a5e862e22eba072"));
        //    Assert.That(generalActivity.Content.Username, Is.EqualTo("ccred95"));
        //    Assert.That(generalActivity.Content.Country, Is.EqualTo("MW"));
        //    Assert.That(generalActivity.Content.IsVerified, Is.False);
        //});
    }
}

[ApiGroupName("Records")]
public class TetrioRecordsApiTests
{

}

[ApiGroupName("News")]
public class TetrioNewsApiTests
    : TetrioApiTestsBase
{
    [Test]
    public async Task CanFetchScoreFlow()
    {
        var leagueScoreFlow = await client.News.FetchAllLatestNewsAsync(10);
        var blitzScoreFlow = await client.Labs.FetchUserScoreFlow("ccred95", GameMode.Blitz);
        var sprintScoreFlow = await client.Labs.FetchUserScoreFlow("ccred95", GameMode.FortyLines);
        var zenithScoreFlow = await client.Labs.FetchUserScoreFlow("ccred95", GameMode.QuickPlay);
        var zenithExScoreFlow = await client.Labs.FetchUserScoreFlow("ccred95", GameMode.ExpertQuickPlay);

        //Assert.Multiple(() =>
        //{
        //    Assert.That(generalActivity.Data.UserActivityData, Has.Count.EqualTo(5760));
        //    Assert.That(generalActivity.Data.UserActivityData.Average(), Is.GreaterThan(1));

        //    Assert.That(generalActivity.Content.Id, Is.EqualTo("5ea516e66a5e862e22eba072"));
        //    Assert.That(generalActivity.Content.Username, Is.EqualTo("ccred95"));
        //    Assert.That(generalActivity.Content.Country, Is.EqualTo("MW"));
        //    Assert.That(generalActivity.Content.IsVerified, Is.False);
        //});
    }
}

[ApiGroupName("Labs")]
public class TetrioLabsApiTests
    : TetrioApiTestsBase
{
    [Test]
    public async Task CanFetchScoreFlow()
    {
        var leagueScoreFlow = await client.Labs.FetchUserScoreFlow("ccred95", GameMode.TetraLeague);
        var blitzScoreFlow = await client.Labs.FetchUserScoreFlow("ccred95", GameMode.Blitz);
        var sprintScoreFlow = await client.Labs.FetchUserScoreFlow("ccred95", GameMode.FortyLines);
        var zenithScoreFlow = await client.Labs.FetchUserScoreFlow("ccred95", GameMode.QuickPlay);
        var zenithExScoreFlow = await client.Labs.FetchUserScoreFlow("ccred95", GameMode.ExpertQuickPlay);

        //Assert.Multiple(() =>
        //{
        //    Assert.That(generalActivity.Data.UserActivityData, Has.Count.EqualTo(5760));
        //    Assert.That(generalActivity.Data.UserActivityData.Average(), Is.GreaterThan(1));

        //    Assert.That(generalActivity.Content.Id, Is.EqualTo("5ea516e66a5e862e22eba072"));
        //    Assert.That(generalActivity.Content.Username, Is.EqualTo("ccred95"));
        //    Assert.That(generalActivity.Content.Country, Is.EqualTo("MW"));
        //    Assert.That(generalActivity.Content.IsVerified, Is.False);
        //});
    }

    [Test]
    public async Task CanFetchUserLeagueFlow()
    {
        var userLeagueFlowActivity = await client.Labs.FetchUserLeagueFlow("ccred95");

        //Assert.Multiple(() =>
        //{
        //    Assert.That(generalActivity.Data.UserActivityData, Has.Count.EqualTo(5760));
        //    Assert.That(generalActivity.Data.UserActivityData.Average(), Is.GreaterThan(1));

        //    Assert.That(generalActivity.Content.Id, Is.EqualTo("5ea516e66a5e862e22eba072"));
        //    Assert.That(generalActivity.Content.Username, Is.EqualTo("ccred95"));
        //    Assert.That(generalActivity.Content.Country, Is.EqualTo("MW"));
        //    Assert.That(generalActivity.Content.IsVerified, Is.False);
        //});
    }

    [Test]
    public async Task CanFetchLeagueRanks()
    {
        var leagueRanks = await client.Labs.FetchLeagueRanks();

        //Assert.Multiple(() =>
        //{
        //    Assert.That(generalActivity.Data.UserActivityData, Has.Count.EqualTo(5760));
        //    Assert.That(generalActivity.Data.UserActivityData.Average(), Is.GreaterThan(1));

        //    Assert.That(generalActivity.Content.Id, Is.EqualTo("5ea516e66a5e862e22eba072"));
        //    Assert.That(generalActivity.Content.Username, Is.EqualTo("ccred95"));
        //    Assert.That(generalActivity.Content.Country, Is.EqualTo("MW"));
        //    Assert.That(generalActivity.Content.IsVerified, Is.False);
        //});
    }
}
[ApiGroupName("Achievements")]
public class TetrioAchievementsApiTests : TetrioApiTestsBase
{
    [Test]
    public async Task CanFetchUserInfo()
    {
        var achievement10 = await client.Achievements.FetchAchievementInfoAsync(10);
        var achievement20 = await client.Achievements.FetchAchievementInfoAsync(20);
        var achievement30 = await client.Achievements.FetchAchievementInfoAsync(30);
        var achievement40 = await client.Achievements.FetchAchievementInfoAsync(40);


    }

}

[ApiGroupName("Users")]
public class TetrioUsersApiTests : TetrioApiTestsBase
{
    [Test]
    public async Task CanFetchUserInfo()
    {
        var userInfoResponse = await client.Users.FetchUserInfoAsync("ccred95");

        Assert.Multiple(() =>
        {
            Assert.That(userInfoResponse.Data.Achievements, Has.Count.GreaterThan(0));
            Assert.That(userInfoResponse.Data.Badges, Has.Count.GreaterThan(0));

            Assert.That(userInfoResponse.Data.Id, Is.EqualTo("5ea516e66a5e862e22eba072"));
            Assert.That(userInfoResponse.Data.Username, Is.EqualTo("ccred95"));
            Assert.That(userInfoResponse.Data.CountryCode, Is.EqualTo("MW"));
            Assert.That(userInfoResponse.Data.FriendCount, Is.GreaterThan(400));
        });
    }
}