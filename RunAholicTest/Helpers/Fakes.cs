namespace RunAholicTest.Fakes;

public class FakeActivityRepository : IActivityRepository
{
    public static List<Activity> _activities = new List<Activity>();
    public Task<Activity> AddActivity(Activity newActivity)
    {
        if(newActivity.ActivityId == null)
        {
            newActivity.ActivityId = Guid.NewGuid().ToString();
        }
        _activities.Add(newActivity);
        return Task.FromResult(newActivity);
    }

    public Task DeleteActivities(string athleteId)
    {
        var activities = GetActivitiesByAthleteId(athleteId).Result;
        foreach (Activity activity in activities)
        {
            _activities.Remove(activity);
        }
        return Task.CompletedTask;

    }

    public Task DeleteActivity(string activityId)
    {
        var activity = GetActivity(activityId).Result;
        _activities.Remove(activity);
        return Task.CompletedTask;
    }

    public Task<List<Activity>> GetActivitiesByAthleteId(string athleteId)
    {
        var activities = _activities.FindAll(_ => _.AthleteId == athleteId);
        return Task.FromResult(activities);
    }

    public Task<Activity> GetActivity(string activityId)
    {
        var activity = _activities.Find(_ => _.ActivityId == activityId);
        return Task.FromResult(activity);
    }

    public Task<List<Activity>> GetAllActivities()
    {
        return Task.FromResult(_activities);
    }

    public Task<Activity> UpdateActivity(Activity activity)
    {
        var updatedActivity = GetActivity(activity.ActivityId).Result;
        int indexOfActivity = _activities.IndexOf(updatedActivity);
        _activities.Insert(indexOfActivity,activity);
        return Task.FromResult(_activities[indexOfActivity]);
    }
}
public class FakeAthleteRepository : IAthleteRepository
{
    public static List<Athlete> _athletes = new List<Athlete>();
    public Task<Athlete> AddAthlete(Athlete newAthlete)
    {
        if(newAthlete.AthleteId == null)
        {
            newAthlete.AthleteId = Guid.NewGuid().ToString();
        }
        _athletes.Add(newAthlete);
        return Task.FromResult(newAthlete);
    }

    public Task DeleteAthlete(string athleteId)
    {
        var athlete = GetAthlete(athleteId).Result;
        _athletes.Remove(athlete);
        return Task.CompletedTask;
    }

    public Task<List<Athlete>> GetAllAthletes()
    {
        return Task.FromResult(_athletes);
    }

    public Task<Athlete> GetAthlete(string athleteId)
    {
        var athlete = _athletes.Find(_ => _.AthleteId == athleteId);
        return Task.FromResult(athlete);
    }

    public Task<Athlete> UpdateAthlete(Athlete athlete)
    {
        var UpdatedAthlete = GetAthlete(athlete.AthleteId).Result;
        int indexOfAthlete = _athletes.IndexOf(UpdatedAthlete);
        _athletes.Insert(indexOfAthlete,athlete);
        return Task.FromResult(_athletes[indexOfAthlete]);
    }
}

public class FakeStatsRepository : IStatsRepository
{
    public static List<Stats> _stats = new List<Stats>();
    public Task<Stats> CreateDefaultStats(Stats defaultStats)
    {
        defaultStats.StatsId = Guid.NewGuid().ToString();
        _stats.Add(defaultStats);
        return Task.FromResult(defaultStats);
    }

    public Task DeleteStats(string statsId)
    {
        var stats = _stats.Find(_ => _.StatsId == statsId);
        _stats.Remove(stats);
        return Task.CompletedTask;
    }

    public Task<List<Stats>> GetAllStats()
    {
        return Task.FromResult(_stats);
    }

    public Task<Stats> GetAthleteStats(string athleteId)
    {
        var stats = _stats.Find(_ => _.AthleteId == athleteId);
        return Task.FromResult(stats);
    }

    public Task<Stats> UpdateAthleteStats(Stats stats)
    {
        var UpdatedStats = GetAthleteStats(stats.AthleteId).Result;
        int indexOfStats = _stats.IndexOf(UpdatedStats);
        _stats.Insert(indexOfStats,stats);
        return Task.FromResult(_stats[indexOfStats]);
    }

}
