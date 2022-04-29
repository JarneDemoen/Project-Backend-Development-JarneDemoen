using Xunit;

namespace RunAholicTest;

public class UnitTest1
{
    [Fact]
    public async Task CheckCalculatedProperties()
    {
        // setup data 
        FakeRunAholicService fakeRunAholicService = new FakeRunAholicService(new FakeActivityRepository(),new FakeAthleteRepository(), new FakeStatsRepository());
        fakeRunAholicService.Setup();

        RunAholicService runAholicService = new RunAholicService(new FakeActivityRepository(), new FakeAthleteRepository(), new FakeStatsRepository());
    
        Activity activity = new Activity()
        {
            Name = "Test",
            StartDateLocal = "28/04/2022 16:24",
            ElapsedTimeInSec = 1027,
            DistanceInMeters = 4236,
            Description = "Unittesten van activity",
            AthleteId = "6266df68f932ca11ff0e5e6c"
        };
        var athlete = await runAholicService.GetAthlete(activity.AthleteId);
        var result = await runAholicService.AddActivity(activity);
        Assert.True(result.Tempo == "4:02 min/km");
        Assert.True(result.Pace == "14,8 km/h");
        Assert.True(result.ElapsedTimeInHMS == "00h:17m:07s");
    }

    [Fact]
    public async Task NrStatsEqNrAthletes()
    {
        // setup data 
        FakeRunAholicService fakeRunAholicService = new FakeRunAholicService(new FakeActivityRepository(),new FakeAthleteRepository(), new FakeStatsRepository());
        fakeRunAholicService.Setup();

        RunAholicService runAholicService = new RunAholicService(new FakeActivityRepository(), new FakeAthleteRepository(), new FakeStatsRepository());
        var stats = await runAholicService.GetAllStats();
        var athletes = await runAholicService.GetAllAthletes();
        Assert.True(stats.Count() == athletes.Count() && stats.Count() == 4);
    }

    [Fact]
    public async Task CalculateStats()
    {
        // setup data 
        FakeRunAholicService fakeRunAholicService = new FakeRunAholicService(new FakeActivityRepository(),new FakeAthleteRepository(), new FakeStatsRepository());
        fakeRunAholicService.Setup();

        RunAholicService runAholicService = new RunAholicService(new FakeActivityRepository(), new FakeAthleteRepository(), new FakeStatsRepository());
        Stats stat = await runAholicService.GetAthleteStats("6266df68f932ca11ff0e5e6a");
        Assert.True(stat.AveragePace == "12,0 km/h");
        Assert.True(stat.AverageTempo == "5:01 min/km");
        Assert.True(stat.NumberOfActivities == 3);
        Assert.True(stat.TotalDistanceInMeters == 21013);
        Assert.True(stat.TotalElapsedTimeInSec == 6327);
        Assert.True(stat.TotalElapsedTimeInHMS == "01h:45m:27s");
    }
}

