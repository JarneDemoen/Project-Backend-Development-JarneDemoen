namespace RunAholicAPI.Repositories;

public interface IStatsRepository
{
    Task<List<Stats>> GetAllStats();
    Task<Stats> GetAthleteStats(string athleteId);
    Task<Stats> UpdateAthleteStats(Stats updatedStats);
    Task<Stats> CreateDefaultStats(Stats defaultStats);
    Task DeleteStats(string statsId);
}

public class StatsRepository : IStatsRepository
{
    private readonly IMongoContext _context;
    public StatsRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<Stats>> GetAllStats()
    {
        try
        {
            return await _context.StatsCollection.Find(_ => true).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Stats> GetAthleteStats(string athleteId)
    {
        try
        {
            return await _context.StatsCollection.Find<Stats>(s => s.AthleteId == athleteId).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Stats> UpdateAthleteStats(Stats updatedStats)
    {
        try
        {
            var filter = Builders<Stats>.Filter.Eq("StatsId", updatedStats.StatsId);
            var update = Builders<Stats>.Update
            .Set("TotalDistanceInMeters", updatedStats.TotalDistanceInMeters)
            .Set("TotalElapsedTimeInSec", updatedStats.TotalElapsedTimeInSec)
            .Set("NumberOfActivities",updatedStats.NumberOfActivities);
            var result = await _context.StatsCollection.UpdateOneAsync(filter, update);
            return await GetAthleteStats(updatedStats.StatsId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Stats> CreateDefaultStats(Stats defaultStats)
    {
        try
        {
            await _context.StatsCollection.InsertOneAsync(defaultStats);
            return defaultStats;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task DeleteStats(string statsId)
    {
        try
        {
            var filter = Builders<Stats>.Filter.Eq("StatsId",statsId);
            var result = await _context.StatsCollection.DeleteOneAsync(filter);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

}
