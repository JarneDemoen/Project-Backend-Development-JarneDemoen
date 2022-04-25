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
    Task<Athlete> GetAthlete(string athleteId);
    Task<Activity> UpdateActivity(Activity updatedActivity);
    Task<Athlete> UpdateAthlete(Athlete updatedAthlete);
}

public class RunAholicService : IRunAholicService
{
    private readonly IActivityRepository _activityRepository;
    private readonly IAthleteRepository _athleteRepository;

    public RunAholicService(IActivityRepository activityRepository, IAthleteRepository athleteRepository)
    {
        _activityRepository = activityRepository;
        _athleteRepository = athleteRepository;
    }

    // ACTIVITY
    public async Task<List<Activity>> GetAllActivities() => await _activityRepository.GetAllActivities();
    public async Task<Activity> GetActivity(string activityId) => await _activityRepository.GetActivity(activityId);
    public async Task<List<Activity>> GetActivitiesByAthleteId(string athleteId) => await _activityRepository.GetActivitiesByAthleteId(athleteId);
    public async Task<Activity> AddActivity(Activity newActivity) => await _activityRepository.AddActivity(newActivity);
    public async Task<Activity> UpdateActivity(Activity updatedActivity) => await _activityRepository.UpdateActivity(updatedActivity);
    public async Task DeleteActivity(string activityId) => await _activityRepository.DeleteActivity(activityId);

    // ATHLETE
    public async Task<List<Athlete>> GetAllAthletes() => await _athleteRepository.GetAllAthletes();
    public async Task<Athlete> GetAthlete(string athleteId) => await _athleteRepository.GetAthlete(athleteId);
    public async Task<Athlete> AddAthlete(Athlete newAthlete) => await _athleteRepository.AddAthlete(newAthlete);
    public async Task<Athlete> UpdateAthlete(Athlete updatedAthlete) => await _athleteRepository.UpdateAthlete(updatedAthlete);
    public async Task DeleteAthlete(string athleteId) => await _athleteRepository.DeleteAthlete(athleteId);
}

