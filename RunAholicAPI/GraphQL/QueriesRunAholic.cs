namespace RunAholicAPI.GraphQL;

public class QueriesRunAholic
{
    // ACTIVITY
    public async Task<List<Activity>> GetAllActivities([Service]IRunAholicService runAholicService) => await runAholicService.GetAllActivities();
    public async Task<Activity> GetActivity([Service]IRunAholicService runAholicService,string activityId) => await runAholicService.GetActivity(activityId);
    public async Task<List<Activity>> GetActivitiesByAthleteId([Service]IRunAholicService runAholicService,string athleteId) => await runAholicService.GetActivitiesByAthleteId(athleteId);
    
    // ATHLETE
    public async Task<List<Athlete>> GetAllAthletes([Service]IRunAholicService runAholicService) => await runAholicService.GetAllAthletes();
    public async Task<Athlete> GetAthlete([Service]IRunAholicService runAholicService,string athleteId) => await runAholicService.GetAthlete(athleteId);

    // STATS
    public async Task<List<Stats>> GetAllStats([Service]IRunAholicService runAholicService) => await runAholicService.GetAllStats();
    public async Task<Stats> GetAthleteStats([Service]IRunAholicService runAholicService,string athleteId) => await runAholicService.GetAthleteStats(athleteId);
    
}