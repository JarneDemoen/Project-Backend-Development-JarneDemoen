namespace RunAholicAPI.Repositories;

public interface IActivityRepository
{
    Task<Activity> AddActivity(Activity newActivity);
    Task DeleteActivity(string id);
    Task<List<Activity>> GetActivitiesByAthleteId(string athleteId);
    Task<Activity> GetActivity(string id);
    Task<List<Activity>> GetAllActivities();
    Task<Activity> UpdateActivity(Activity activity);
}

public class ActivityRepository : IActivityRepository
{
    private readonly IMongoContext _context;
    public ActivityRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<Activity>> GetAllActivities()
    {
        try
        {
            return await _context.ActivitiesCollection.Find(_ => true).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
    public async Task<Activity> GetActivity(string activityId)
    {
        try
        {
            return await _context.ActivitiesCollection.Find<Activity>(a => a.ActivityId == activityId).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<List<Activity>> GetActivitiesByAthleteId(string athleteId)
    {
        try
        {
            return await _context.ActivitiesCollection.Find(a => a.AthleteId == athleteId).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Activity> AddActivity(Activity newActivity)
    {
        try
        {
            await _context.ActivitiesCollection.InsertOneAsync(newActivity);
            return newActivity;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Activity> UpdateActivity(Activity activity)
    {
        try
        {
            var filter = Builders<Activity>.Filter.Eq("ActivityId", activity.ActivityId);
            var update = Builders<Activity>.Update
            .Set("Name", activity.Name)
            .Set("Description", activity.Description);
            var result = await _context.ActivitiesCollection.UpdateOneAsync(filter, update);
            return await GetActivity(activity.ActivityId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task DeleteActivity(string id)
    {
        try
        {
            var filter = Builders<Activity>.Filter.Eq("ActivityId", id);
            var result = await _context.ActivitiesCollection.DeleteOneAsync(filter);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

}