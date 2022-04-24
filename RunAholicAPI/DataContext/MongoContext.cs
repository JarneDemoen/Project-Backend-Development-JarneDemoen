namespace RunAholicAPI.DataContext;

public interface IMongoContext
{
    IMongoClient Client { get; }
    IMongoDatabase Database { get; }
    IMongoCollection<Activity> ActivitiesCollection { get; }
    IMongoCollection<Athlete> AthletesCollection { get; }
    IMongoCollection<Stats> StatsCollection { get; }
}

public class MongoContext : IMongoContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    private readonly DatabaseSettings _settings;

    public IMongoClient Client
    {
        get
        {
            return _client;
        }
    }
    public IMongoDatabase Database => _database;

    public MongoContext(IOptions<DatabaseSettings> dbOptions)
    {
        _settings = dbOptions.Value;
        _client = new MongoClient(_settings.ConnectionString);
        _database = _client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<Activity> ActivitiesCollection
    {
        get
        {
            return _database.GetCollection<Activity>(_settings.ActivitiesCollection);
        }
    }
    public IMongoCollection<Athlete> AthletesCollection
    {
        get
        {
            return _database.GetCollection<Athlete>(_settings.AthletesCollection);
        }
    }
    public IMongoCollection<Stats> StatsCollection
    {
        get
        {
            return _database.GetCollection<Stats>(_settings.StatsCollection);
        }
    }
}