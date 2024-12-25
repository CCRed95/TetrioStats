namespace TetrioStats.Api.Tests;

[ApiGroupName("General")]
public class TetrioGeneralServerApiTests 
	: TetrioApiTestsBase
{
	[Test]
	public async Task CanFetchUserInfo()
	{
		var generalServerStats = await client.General.FetchGeneralServerStatisticsAsync();

		Assert.Multiple(() =>
		{
			Assert.That(generalServerStats.Data.UserCount, Is.GreaterThan(1000000));
			Assert.That(generalServerStats.Data.AnonCount, Is.GreaterThan(1000000));
			Assert.That(generalServerStats.Data.RankedCount, Is.GreaterThan(10000));
			Assert.That(generalServerStats.Data.GamesPlayed, Is.GreaterThan(7000000000));
			Assert.That(generalServerStats.Data.GameTime, Is.GreaterThan(TimeSpan.FromSeconds(130489041708)));
			Assert.That(generalServerStats.Data.Inputs, Is.GreaterThan(400000000000));
		});
	}
}