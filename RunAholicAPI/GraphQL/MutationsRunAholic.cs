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
        return new AddAthletePayload(created);
    }
    public async Task<UpdateAthletePayload> UpdateAthlete([Service]IRunAholicService runAholicService,UpdateAthleteInput input)
    {
        var updatedAthlete = new Athlete()
        {
            AthleteId = input.athleteId,
            FirstName = input.firstName,
            LastName = input.lastName,
            City = input.city,
            Country = input.country,
            Age = input.age
        };
        var updated = await runAholicService.UpdateAthlete(updatedAthlete);
        return new UpdateAthletePayload(updated);
    }
    public async Task<string> DeleteAthlete([Service]IRunAholicService runAholicService,DeleteAthleteInput input)
    {
        await runAholicService.DeleteAthlete(input.athleteId);
        return $"Delete van athlete {input.athleteId} is gelukt";
    }

}
    
