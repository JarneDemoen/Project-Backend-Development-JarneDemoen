namespace RunAholicTest.Fakes;

public class FakeActivityRepository : IActivityRepository
{
    public static List<Activity> _activities = new List<Activity>();
    public Task<Activity> AddActivity(Activity newActivity)
    {
        _activities.Add(newActivity);
        return Task.FromResult(newActivity);
    }

    public Task DeleteActivities(string atlheteId)
    {
        var activities = _activities.FindAll(_ => _.AthleteId == atlheteId);
        foreach (Activity activity in activities)
        {
            _activities.Remove(activity);
        }
        return Task.CompletedTask;;
    }

    public Task DeleteActivity(string activityId)
    {
        var activity = _activities.Find(_ => _.ActivityId == activityId);
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
        int indexOfActivity = _activities.IndexOf(_activities.Single(i => i.ActivityId == activity.ActivityId));
        _activities[indexOfActivity] = activity;
        return Task.FromResult(_activities[indexOfActivity]);
    }
}
public class FakeAthleteRepository : IAthleteRepository
{
    public static List<Athlete> _athletes = new List<Athlete>();
    public Task<Athlete> AddAthlete(Athlete newAthlete)
    {
        _athletes.Add(newAthlete);
        return Task.FromResult(newAthlete);
    }

    public Task DeleteAthlete(string athleteId)
    {
        var athlete = _athletes.Find(_ => _.AthleteId == athleteId);
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
        int indexOfAthlete = _athletes.IndexOf(_athletes.Single(i => i.AthleteId == athlete.AthleteId));
        _athletes[indexOfAthlete] = athlete;
        return Task.FromResult(_athletes[indexOfAthlete]);
    }
}

public class FakeStatsRepository : IStatsRepository
{
    public Task<Stats> CreateDefaultStats(Stats defaultStats)
    {
        throw new NotImplementedException();
    }

    public Task DeleteStats(string statsId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Stats>> GetAllStats()
    {
        throw new NotImplementedException();
    }

    public Task<Stats> GetAthleteStats(string athleteId)
    {
        throw new NotImplementedException();
    }

    public Task<Stats> UpdateAthleteStats(Stats updatedStats)
    {
        throw new NotImplementedException();
    }
}
