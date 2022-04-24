namespace RunAholicAPI.Models;

public class Stats
{
    public string? StatsId { get; set; }
    public decimal TotalDistanceInKM { get; set; }
    public int NumberOfActivities { get; set; } = 0;
    public decimal TotalElapsedTime { get; set; }
    public string? TotalElapsedTimeInHMS { get; set; }
}