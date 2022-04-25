namespace RunAholicAPI.Services;

public interface IRunAholicService
{
    Task<Activity> AddActivity(Activity newActivity);
    Task<Athlete> AddAthlete(Athlete newAthlete);
    Task DeleteActivity(string activityId);
    Task DeleteAthlete(string athleteId);
    Task<List<Activity>> GetActivitiesByAthleteId(string athleteId);
    Task<Activity> GetActivity(string activityId);
    Task<List<Activity>> GetAllActivities();
    Task<List<Athlete>> GetAllAthletes();
    Task<List<Stats>> GetAllStats();
    Task<Athlete> GetAthlete(string athleteId);
    Task<Stats> GetAthleteStats(string athleteId);
    Task<Activity> UpdateActivity(Activity updatedActivity);
    Task<Athlete> UpdateAthlete(Athlete updatedAthlete);
    Task<Stats> UpdateAthleteStats(Stats updatedStats);
}

public class RunAholicService : IRunAholicService
{
    private readonly IActivityRepository _activityRepository;
    private readonly IAthleteRepository _athleteRepository;
    private readonly IStatsRepository _statsRepository;

    public RunAholicService(IActivityRepository activityRepository, IAthleteRepository athleteRepository, IStatsRepository statsRepository)
    {
        _activityRepository = activityRepository;
        _athleteRepository = athleteRepository;
        _statsRepository = statsRepository;
    }

    // ACTIVITY
    public async Task<List<Activity>> GetAllActivities() => await _activityRepository.GetAllActivities();
    public async Task<Activity> GetActivity(string activityId) => await _activityRepository.GetActivity(activityId);
    public async Task<List<Activity>> GetActivitiesByAthleteId(string athleteId) => await _activityRepository.GetActivitiesByAthleteId(athleteId);
    public async Task<Activity> AddActivity(Activity newActivity) 
    {
        Stats currentStats = await _statsRepository.GetAthleteStats(newActivity.AthleteId);
        currentStats.NumberOfActivities += 1;
        currentStats.TotalDistanceInMeters += newActivity.DistanceInMeters;
        currentStats.TotalElapsedTimeInSec += newActivity.ElapsedTimeInSec;
        await _statsRepository.UpdateAthleteStats(currentStats);
        await _activityRepository.AddActivity(newActivity);
        return newActivity;
    }
    public async Task<Activity> UpdateActivity(Activity updatedActivity) => await _activityRepository.UpdateActivity(updatedActivity);
    public async Task DeleteActivity(string activityId) 
    {
        Activity activity = await _activityRepository.GetActivity(activityId);
        Stats currentStats = await _statsRepository.GetAthleteStats(activity.AthleteId);
        currentStats.NumberOfActivities -= 1;
        currentStats.TotalDistanceInMeters -= activity.DistanceInMeters;
        currentStats.TotalElapsedTimeInSec -= activity.ElapsedTimeInSec;
        await _statsRepository.UpdateAthleteStats(currentStats);
        await _activityRepository.DeleteActivity(activityId);
    }

    // ATHLETE
    public async Task<List<Athlete>> GetAllAthletes() => await _athleteRepository.GetAllAthletes();
    public async Task<Athlete> GetAthlete(string athleteId) => await _athleteRepository.GetAthlete(athleteId);
    public async Task<Athlete> AddAthlete(Athlete newAthlete)
    {
        newAthlete = await _athleteRepository.AddAthlete(newAthlete);
        Stats defaultStats = new Stats{
            TotalDistanceInMeters=0,
            NumberOfActivities = 0,
            TotalElapsedTimeInSec = 0,
            AthleteId = newAthlete.AthleteId};
        await _statsRepository.CreateDefaultStats(defaultStats);
        return newAthlete;
    }
    public async Task<Athlete> UpdateAthlete(Athlete updatedAthlete) => await _athleteRepository.UpdateAthlete(updatedAthlete);
    public async Task DeleteAthlete(string athleteId) 
    {
        await _athleteRepository.DeleteAthlete(athleteId);
        Stats currentStats = await _statsRepository.GetAthleteStats(athleteId);
        var statsId = currentStats.StatsId;
        await _statsRepository.DeleteStats(statsId);
        await _activityRepository.DeleteActivities(athleteId);
    }

    // STATS
    public async Task<List<Stats>> GetAllStats() => await _statsRepository.GetAllStats();
    public async Task<Stats> GetAthleteStats(string athleteId) => await _statsRepository.GetAthleteStats(athleteId);
    public async Task<Stats> UpdateAthleteStats(Stats updatedStats) => await _statsRepository.UpdateAthleteStats(updatedStats);

}

