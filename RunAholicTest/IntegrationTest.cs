namespace RunAholicTest.IntegrationTesting;

public class IntegrationTests
{
    [Fact]
    public async Task Should_Return_Athletes()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var result = await client.GetAsync("/activities");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}