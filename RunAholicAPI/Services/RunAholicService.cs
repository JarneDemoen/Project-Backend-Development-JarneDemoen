namespace RunAholicAPI.Services;

public interface IRunAholicService
{
    Task<Activity> AddActivity(Activity newActivity);
    Task DeleteActivity(string activityId);
    Task<List<Activity>> GetActivitiesByAthleteId(string athleteId);
    Task<Activity> GetActivity(string activityId);
    Task<List<Activity>> GetAllActivities();
    Task<Activity> UpdateActivity(Activity updatedActivity);
}

public class RunAholicService : IRunAholicService
{
    private readonly IActivityRepository _activityRepository;

    public RunAholicService(IActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }

    public async Task<List<Activity>> GetAllActivities() => await _activityRepository.GetAllActivities();
    public async Task<Activity> GetActivity(string activityId) => await _activityRepository.GetActivity(activityId);
    public async Task<List<Activity>> GetActivitiesByAthleteId(string athleteId) => await _activityRepository.GetActivitiesByAthleteId(athleteId);
    public async Task<Activity> AddActivity(Activity newActivity) => await _activityRepository.AddActivity(newActivity);
    public async Task<Activity> UpdateActivity(Activity updatedActivity) => await _activityRepository.UpdateActivity(updatedActivity);
    public async Task DeleteActivity(string activityId) => await _activityRepository.DeleteActivity(activityId);
}

