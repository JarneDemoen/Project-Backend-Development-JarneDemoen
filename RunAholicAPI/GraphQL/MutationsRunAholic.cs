namespace RunAholicAPI.GraphQL;

public class MutationsRunaholic
{
    public async Task<Activity> AddActivity([Service]IRunAholicService runAholicService,Activity newActivity) 
    {
        Stats currentStats = await runAholicService.GetAthleteStats(newActivity.AthleteId);
        currentStats.NumberOfActivities += 1;
        currentStats.TotalDistanceInMeters += newActivity.DistanceInMeters;
        currentStats.TotalElapsedTimeInSec += newActivity.ElapsedTimeInSec;
        await runAholicService.UpdateAthleteStats(currentStats);
        await runAholicService.AddActivity(newActivity);
        return newActivity;
    }
    public async Task<Activity> UpdateActivity([Service]IRunAholicService runAholicService,Activity updatedActivity) => await runAholicService.UpdateActivity(updatedActivity);
    public async Task DeleteActivity([Service]IRunAholicService runAholicService,string activityId) 
    {
        Activity activity = await runAholicService.GetActivity(activityId);
        Stats currentStats = await runAholicService.GetAthleteStats(activity.AthleteId);
        currentStats.NumberOfActivities -= 1;
        currentStats.TotalDistanceInMeters -= activity.DistanceInMeters;
        currentStats.TotalElapsedTimeInSec -= activity.ElapsedTimeInSec;
        await runAholicService.UpdateAthleteStats(currentStats);
        await runAholicService.DeleteActivity(activityId);
    }

    // ATHLETE
    public async Task<Athlete> AddAthlete([Service]IRunAholicService runAholicService,Athlete newAthlete)
    {
        newAthlete = await runAholicService.AddAthlete(newAthlete);
        Stats defaultStats = new Stats{
            TotalDistanceInMeters=0,
            NumberOfActivities = 0,
            TotalElapsedTimeInSec = 0,
            AthleteId = newAthlete.AthleteId};
        await runAholicService.CreateDefaultStats(defaultStats);
        return newAthlete;
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