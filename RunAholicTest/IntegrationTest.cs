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
        Assert.True(activities.Count > 0);
    }
}