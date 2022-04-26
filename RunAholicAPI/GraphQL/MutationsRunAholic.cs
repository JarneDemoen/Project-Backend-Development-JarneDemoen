namespace RunAholicAPI.GraphQL;

public class MutationsRunaholic
{
    public async Task<AddActivityPayload> AddActivity([Service]IRunAholicService runAholicService,AddActivityInput input) 
    {
        var newActivity = new Activity()
        {
            Name = input.name,
            StartDateLocal = input.StartDateLocal,
            ElapsedTimeInSec = input.ElapsedTimeInSec,
            DistanceInMeters = input.DistanceInMeters,
            AthleteId = input.athleteId,
            Description = input.Description
        };
        Stats currentStats = await runAholicService.GetAthleteStats(newActivity.AthleteId);
        currentStats.NumberOfActivities += 1;
        currentStats.TotalDistanceInMeters += newActivity.DistanceInMeters;
        currentStats.TotalElapsedTimeInSec += newActivity.ElapsedTimeInSec;
        await runAholicService.UpdateAthleteStats(currentStats);
        var created = await runAholicService.AddActivity(newActivity);
        return new AddActivityPayload(created);
    }
    public async Task<UpdateActivityPayload> UpdateActivity([Service]IRunAholicService runAholicService,UpdateActivityInput input)
    {
        var updatedActivity = new Activity()
        {
            ActivityId = input.activityId,
            Name = input.name,
            Description = input.description
        };
        var updated = await runAholicService.UpdateActivity(updatedActivity);
        return new UpdateActivityPayload(updated);
    }
    public async Task<string> DeleteActivity([Service]IRunAholicService runAholicService,DeleteActivityInput input) 
    {
        Activity activity = await runAholicService.GetActivity(input.activityId);
        Stats currentStats = await runAholicService.GetAthleteStats(activity.AthleteId);
        currentStats.NumberOfActivities -= 1;
        currentStats.TotalDistanceInMeters -= activity.DistanceInMeters;
        currentStats.TotalElapsedTimeInSec -= activity.ElapsedTimeInSec;
        await runAholicService.UpdateAthleteStats(currentStats);
        await runAholicService.DeleteActivity(input.activityId);
        return $"Delete van {input.activityId} is gelukt";
    }

    // ATHLETE
    public async Task<AddAthletePayload> AddAthlete([Service]IRunAholicService runAholicService,AddAthleteInput input)
    {
        var newAthlete = new Athlete()
        {
            FirstName = input.firstName,
            LastName = input.lastName,
            City = input.city,
            Country = input.country,
            Age = input.age
        };
        var created = await runAholicService.AddAthlete(newAthlete);
        Stats defaultStats = new Stats{
            TotalDistanceInMeters=0,
            NumberOfActivities = 0,
            TotalElapsedTimeInSec = 0,
            AthleteId = newAthlete.AthleteId};
        await runAholicService.CreateDefaultStats(defaultStats);
        return new AddAthletePayload(created);
    }
    public async Task<Athlete> UpdateAthlete([Service]IRunAholicService runAholicService,Athlete updatedAthlete) => await runAholicService.UpdateAthlete(updatedAthlete);
    public async Task DeleteAthlete([Service]IRunAholicService runAholicService,string athleteId) 
    {
        await runAholicService.DeleteAthlete(athleteId);
        Stats currentStats = await runAholicService.GetAthleteStats(athleteId);
        var statsId = currentStats.StatsId;
        await runAholicService.DeleteStats(statsId);
        await runAholicService.DeleteActivities(athleteId);
    }

    // STATS
    public async Task<Stats> UpdateAthleteStats([Service]IRunAholicService runAholicService,Stats updatedStats) => await runAholicService.UpdateAthleteStats(updatedStats);
}