namespace RunAholicTest.Service;

public class FakeRunAholicService : IRunAholicService
{
    private readonly IActivityRepository _activityRepository;
    private readonly IAthleteRepository _athleteRepository;
    private readonly IStatsRepository _statsRepository;

    public FakeRunAholicService(IActivityRepository activityRepository, IAthleteRepository athleteRepository, IStatsRepository statsRepository)
    {
        _activityRepository = activityRepository;
        _athleteRepository = athleteRepository;
        _statsRepository = statsRepository;
    }
    public Task<Activity> AddActivity(Activity newActivity)
    {
        Stats currentStats =  _statsRepository.GetAthleteStats(newActivity.AthleteId).Result;
        currentStats.NumberOfActivities += 1;
        currentStats.TotalDistanceInMeters += newActivity.DistanceInMeters;
        currentStats.TotalElapsedTimeInSec += newActivity.ElapsedTimeInSec;
         _statsRepository.UpdateAthleteStats(currentStats);
         _activityRepository.AddActivity(newActivity);
        return Task.FromResult(newActivity);
    }

    public Task<Athlete> AddAthlete(Athlete newAthlete)
    {
        newAthlete =  _athleteRepository.AddAthlete(newAthlete).Result;
        Stats defaultStats = new Stats{
            TotalDistanceInMeters=0,
            NumberOfActivities = 0,
            TotalElapsedTimeInSec = 0,
            AthleteId = newAthlete.AthleteId};
         _statsRepository.CreateDefaultStats(defaultStats);
        return Task.FromResult(newAthlete);
    }

    public Task<Stats> CreateDefaultStats(Stats defaultStats)
    {
        return Task.FromResult(_statsRepository.CreateDefaultStats(defaultStats).Result);
    }

    public Task DeleteActivities(string athleteId)
    {
        return Task.FromResult(_activityRepository.DeleteActivities(athleteId));
    }

    public Task DeleteActivity(string activityId)
    {
        Activity activity = _activityRepository.GetActivity(activityId).Result;
        Stats currentStats = _statsRepository.GetAthleteStats(activity.AthleteId).Result;
        currentStats.NumberOfActivities -= 1;
        currentStats.TotalDistanceInMeters -= activity.DistanceInMeters;
        currentStats.TotalElapsedTimeInSec -= activity.ElapsedTimeInSec;
        _statsRepository.UpdateAthleteStats(currentStats);
        return Task.FromResult(_activityRepository.DeleteActivity(activityId));
    }

    public Task DeleteAthlete(string athleteId)
    {
        _athleteRepository.DeleteAthlete(athleteId);
        Stats currentStats =  _statsRepository.GetAthleteStats(athleteId).Result;
        var statsId = currentStats.StatsId;
        _statsRepository.DeleteStats(statsId);
        return Task.FromResult(_activityRepository.DeleteActivities(athleteId));
    }

    public Task DeleteStats(string statsId)
    {
        return Task.FromResult( _statsRepository.DeleteStats(statsId));
    }

    public Task<List<Activity>> GetActivitiesByAthleteId(string athleteId)
    {
        return Task.FromResult(_activityRepository.GetActivitiesByAthleteId(athleteId).Result);
    }

    public Task<Activity> GetActivity(string activityId)
    {
        return Task.FromResult(_activityRepository.GetActivity(activityId).Result);
    }

    public Task<List<Activity>> GetAllActivities()
    {
        return Task.FromResult(_activityRepository.GetAllActivities().Result);
    }

    public Task<List<Athlete>> GetAllAthletes()
    {
        return Task.FromResult(_athleteRepository.GetAllAthletes().Result);
    }

    public Task<List<Stats>> GetAllStats()
    {
        return Task.FromResult(_statsRepository.GetAllStats().Result);
    }

    public Task<Athlete> GetAthlete(string athleteId)
    {
        return Task.FromResult(_athleteRepository.GetAthlete(athleteId).Result);
    }

    public Task<Stats> GetAthleteStats(string athleteId)
    {
        return Task.FromResult(_statsRepository.GetAthleteStats(athleteId).Result);
    }

    public Task<Activity> UpdateActivity(Activity updatedActivity)
    {
        return Task.FromResult(_activityRepository.UpdateActivity(updatedActivity).Result);
    }

    public Task<Athlete> UpdateAthlete(Athlete updatedAthlete)
    {
        return Task.FromResult(_athleteRepository.UpdateAthlete(updatedAthlete).Result);
    }

    public Task<Stats> UpdateAthleteStats(Stats updatedStats)
    {
        return Task.FromResult(_statsRepository.UpdateAthleteStats(updatedStats).Result);
    }
}