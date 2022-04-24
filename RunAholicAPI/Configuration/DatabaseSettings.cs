namespace RunAholicAPI.Configuration;

public class DatabaseSettings
{
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public string? ActivitiesCollection { get; set; }
    public string? AthletesCollection { get; set; }
    public string? StatsCollection { get; set; }
}