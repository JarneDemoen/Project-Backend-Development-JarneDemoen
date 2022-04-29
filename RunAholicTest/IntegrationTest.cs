namespace RunAholicTest.IntegrationTesting;

public class IntegrationTests
{
    
    [Fact]
    public async Task Return_Activities()
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
    public async Task Return_Activity()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Helper.GenerateBearerToken());
        var result = await client.GetAsync("/activities/6266df68f932ca11ff0e5e62");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var activity = await result.Content.ReadFromJsonAsync<Activity>();
        Assert.NotNull(activity);
    }

    [Fact]
    public async Task Return_ActivitiesByAthlete()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Helper.GenerateBearerToken());
        var result = await client.GetAsync("/activities/athlete/6266df68f932ca11ff0e5e6a");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var activities = await result.Content.ReadFromJsonAsync<List<Activity>>();
        Assert.NotNull(activities);
        Assert.True(activities.Count() == 3);
    }

    [Fact]
    public async Task Return_AddActivity()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Helper.GenerateBearerToken());

        Activity activity = new Activity()
        {
            Name = "Test",
            StartDateLocal = "28/04/2022 16:24",
            ElapsedTimeInSec = 900,
            DistanceInMeters = 3000,
            Description = "Testen van activity",
            AthleteId = "6266df68f932ca11ff0e5e6a"
        };

        var result = await client.PostAsJsonAsync("/activities",activity);
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Return_UpdateActivity()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Helper.GenerateBearerToken());

        Activity activity = new Activity()
        {
            ActivityId = "6266df68f932ca11ff0e5e64",
            Name = "Running with friend aangepast",
            StartDateLocal = "24/02/2022 11:19",
            ElapsedTimeInSec = 1760,
            DistanceInMeters = 4800,
            Description = "Description aangepast",
            AthleteId = "6266df68f932ca11ff0e5e6b"
        };

        var result = await client.PutAsJsonAsync("/activities",activity);
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Return_DeleteActivity()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Helper.GenerateBearerToken());
        var result = await client.DeleteAsync("/activities/6266df68f932ca11ff0e5e64");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }


    [Fact]
    public async Task Return_Stats()
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
    public async Task Return_StatsByAthleteId()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Helper.GenerateBearerToken());
        var resultStats = await client.GetAsync("/stats/athlete/6266df68f932ca11ff0e5e6c");
        resultStats.StatusCode.Should().Be(HttpStatusCode.OK);
        var stats = await resultStats.Content.ReadFromJsonAsync<Stats>();
        Assert.NotNull(stats);
    }

    [Fact]
    public async Task Return_Athletes()
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

    [Fact]
    public async Task Return_Athlete()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Helper.GenerateBearerToken());
        var result = await client.GetAsync("/athletes/6266df68f932ca11ff0e5e6c");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var athlete = await result.Content.ReadFromJsonAsync<Athlete>();
        Assert.NotNull(athlete);
    }

    [Fact]
    public async Task Return_AddAthlete()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Helper.GenerateBearerToken());

        Athlete athlete = new Athlete()
        {
            AthleteId = "6266df68f932ca23ff0e5e6b",
            FirstName = "Integration",
            LastName = "Test",
            City = "Testcity",
            Country = "Testcountry",
            Age = 21
        };

        var result = await client.PostAsJsonAsync("/athletes",athlete);
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Return_UpdateAthlete()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Helper.GenerateBearerToken());

        Athlete athlete = new Athlete()
        {
            AthleteId = "6266df68f932ca11ff0e5e6c",
            FirstName = "Bjorn",
            LastName = "Demoen",
            City = "Izegem",
            Age = 45,
            Country = "Belgium"
        };

        var result = await client.PutAsJsonAsync("/athletes",athlete);
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Return_DeleteAthlete()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Helper.GenerateBearerToken());
        var result = await client.DeleteAsync("/athletes/6266df68f932ca11ff0e5e6c");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}