namespace RunAholicTest.IntegrationTesting;

public class IntegrationTests
{
    
    [Fact]
    public async Task Should_Return_Activities()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Helper.GenerateBearerToken());
        var result = await client.GetAsync("/activities");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var activities = await result.Content.ReadFromJsonAsync<List<Activity>>();
        Assert.NotNull(activities);
        await File.WriteAllTextAsync("ActivitiesCount.txt", activities.Count().ToString());
        Assert.True(activities.Count() > 0);
        Assert.True(activities.Count() == 9);
    }

    [Fact]
    public async Task Should_Return_Stats()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Helper.GenerateBearerToken());
        var resultStats = await client.GetAsync("/stats");
        // var resultAthletes = await client.GetAsync("/athletes");
        // resultAthletes.StatusCode.Should().Be(HttpStatusCode.OK);
        resultStats.StatusCode.Should().Be(HttpStatusCode.OK);
        var stats = await resultStats.Content.ReadFromJsonAsync<List<Stats>>();
        // var athletes = await resultAthletes.Content.ReadFromJsonAsync<List<Athlete>>();
        Assert.NotNull(stats);
        await File.WriteAllTextAsync("StatsCount.txt", stats.Count().ToString());
        Assert.True(stats.Count() == 4);
    }

    [Fact]
    public async Task Should_Return_Athletes()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Helper.GenerateBearerToken());
        var result = await client.GetAsync("/athletes");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var athletes = await result.Content.ReadFromJsonAsync<List<Athlete>>();
        Assert.NotNull(athletes);
        Assert.True(athletes.Count() > 0);
        await File.WriteAllTextAsync("AthletesCount.txt", athletes.Count.ToString());
        Assert.True(athletes.Count() == 4);
    }

}