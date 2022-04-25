namespace RunAholicAPI.Repositories;

public interface IAthleteRepository
{
    Task<Athlete> AddAthlete(Athlete newAthlete);
    Task DeleteAthlete(string athleteId);
    Task<List<Athlete>> GetAllAthletes();
    Task<Athlete> GetAthlete(string athleteId);
    Task<Athlete> UpdateAthlete(Athlete athlete);
}

public class AthleteRepository : IAthleteRepository
{
    private readonly IMongoContext _context;
    public AthleteRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<List<Athlete>> GetAllAthletes()
    {
        try
        {
            return await _context.AthletesCollection.Find(_ => true).ToListAsync();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Athlete> GetAthlete(string athleteId)
    {
        try
        {
            return await _context.AthletesCollection.Find<Athlete>(a => a.AthleteId == athleteId).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Athlete> AddAthlete(Athlete newAthlete)
    {
        try
        {
            await _context.AthletesCollection.InsertOneAsync(newAthlete);
            return newAthlete;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Athlete> UpdateAthlete(Athlete updatedAthlete)
    {
        try
        {
            var filter = Builders<Athlete>.Filter.Eq("AthleteId", updatedAthlete.AthleteId);
            var update = Builders<Athlete>.Update
            .Set("FirstName", updatedAthlete.FirstName)
            .Set("LastName", updatedAthlete.LastName)
            .Set("City", updatedAthlete.City)
            .Set("Country", updatedAthlete.Country)
            .Set("Age", updatedAthlete.Age);
            var result = await _context.AthletesCollection.UpdateOneAsync(filter, update);
            return await GetAthlete(updatedAthlete.AthleteId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task DeleteAthlete(string athleteId)
    {
        try
        {
            var filter = Builders<Athlete>.Filter.Eq("AthleteId", athleteId);
            var result = await _context.AthletesCollection.DeleteOneAsync(filter);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}

