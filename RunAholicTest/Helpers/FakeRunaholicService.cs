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
        Setup();
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
        CreateDefaultStats(defaultStats);
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
        if(currentStats == null)
        {
            return Task.CompletedTask;
        }
        DeleteStats(athleteId);
        return Task.FromResult(_activityRepository.DeleteActivities(athleteId));
    }

    public Task DeleteStats(string athleteId)
    {
        return Task.FromResult( _statsRepository.DeleteStats(athleteId));
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
    public void Setup()
    {
        // Athletes toevoegen
        Athlete jarne = new Athlete()
        {
            AthleteId = "1",
            FirstName = "Jarne",
            LastName = "Demoen",
            City = "Hooglede",
            Age = 21,
            Country = "Belgium"
        };
        DeleteAthlete(jarne.AthleteId);
        AddAthlete(jarne);

        Athlete tammin = new Athlete()
        {
            AthleteId = "2",
            FirstName = "Tammin",
            LastName = "Demoen",
            City = "Izegem",
            Age = 18,
            Country = "Belgium"
        };
        DeleteAthlete(tammin.AthleteId);
        AddAthlete(tammin);

        Athlete bjorn = new Athlete()
        {
            AthleteId = "3",
            FirstName = "Bjorn",
            LastName = "Demoen",
            City = "Izegem",
            Age = 43,
            Country = "Belgium"
        };
        DeleteAthlete(bjorn.AthleteId);
        AddAthlete(bjorn);

        Athlete lindsay = new Athlete()
        {
            AthleteId = "4",
            FirstName = "Lindsay",
            LastName = "Verhoeven",
            City = "Hooglede",
            Age = 44,
            Country = "Belgium"
        };
        DeleteAthlete(lindsay.AthleteId);
        AddAthlete(lindsay);

        // Activities toevoegen
        Activity actjarne1 = new Activity()
        {
            Name = "Morning Run",
            StartDateLocal = "20/04/2022 8:19",
            ElapsedTimeInSec = 1711,
            DistanceInMeters = 6007,
            Description = "Run 2 wake up",
            AthleteId = "1"
        };
        AddActivity(actjarne1);
        Activity actjarne2 = new Activity()
        {
            Name = "Pre-Season Run",
            StartDateLocal = "17/06/2021 18:41",
            ElapsedTimeInSec = 2771,
            DistanceInMeters = 10005,
            Description = "Run 2 wake up",
            AthleteId = "1"
        };
        AddActivity(actjarne2);
        Activity actjarne3 = new Activity()
        {
            Name = "Slow pace",
            StartDateLocal = "27/04/2022 14:07",
            ElapsedTimeInSec = 1845,
            DistanceInMeters = 5001,
            Description = "Revalidation",
            AthleteId = "1"
        };
        AddActivity(actjarne3);

        Activity acttammin1 = new Activity()
        {
            Name = "Running with friend",
            StartDateLocal = "24/02/2022 11:19",
            ElapsedTimeInSec = 1760,
            DistanceInMeters = 4800,
            AthleteId = "2"
        };
        AddActivity(acttammin1);
        Activity acttammin2 = new Activity()
        {
            Name = "Slow pace",
            StartDateLocal = "27/04/2022 14:07",
            ElapsedTimeInSec = 1845,
            DistanceInMeters = 5001,
            Description = "Taking it easy",
            AthleteId = "2"
        };
        AddActivity(acttammin2);

        Activity actbjorn1 = new Activity()
        {
            Name = "First Run @ Wallemote",
            StartDateLocal = "01/04/2022 10:12",
            ElapsedTimeInSec = 994,
            DistanceInMeters = 3004,
            Description = "First run",
            AthleteId = "3"
        };
        AddActivity(actbjorn1);
        Activity actbjorn2 = new Activity()
        {
            Name = "Second Run @ Wallemote",
            StartDateLocal = "05/04/2022 21:05",
            ElapsedTimeInSec = 2465,
            DistanceInMeters = 6019,
            Description = "Second run",
            AthleteId = "3"
        };
        AddActivity(actbjorn2);
        Activity actbjorn3 = new Activity()
        {
            Name = "Last Run @ Wallemote",
            StartDateLocal = "12/04/2022 19:48",
            ElapsedTimeInSec = 2609,
            DistanceInMeters = 7004,
            Description = "Last run",
            AthleteId = "3"
        };
        AddActivity(actbjorn3);

        Activity actlindsay1 = new Activity()
        {
            Name = "Start 2 run",
            StartDateLocal = "21/04/2022 10:17",
            ElapsedTimeInSec = 1154,
            DistanceInMeters = 3000,
            Description = "First Start 2 run sessino",
            AthleteId = "4"
        };
        AddActivity(actlindsay1);

    }
}