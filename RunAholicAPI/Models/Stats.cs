namespace RunAholicAPI.Models;

public class Stats
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? StatsId { get; set; }
    public decimal TotalDistanceInMeters { get; set; }
    public int NumberOfActivities { get; set; } = 0;
    public int TotalElapsedTimeInSec { get; set; }
    public string? TotalElapsedTimeInHMS 
    { 
        get
            {
                TimeSpan t = TimeSpan.FromSeconds(TotalElapsedTimeInSec);
                string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s", 
                t.Hours, 
                t.Minutes, 
                t.Seconds);
                return answer;
            }
    }
    public string? AverageTempo 
    {  
        get
            {
                // Verkregen seconden die in Elapsed_time zitten omzetten naar een string in formaat (H:)MM:SS
                decimal seconds = TotalElapsedTimeInSec / (TotalDistanceInMeters / 1000);
                decimal minutes = seconds / 60;
                decimal roundedMinutes = Math.Floor(minutes);
                decimal rest = minutes - roundedMinutes;
                seconds = rest * 60;

                // afronden
                seconds = Math.Round(seconds);
                string secondsString = seconds.ToString();
                if (secondsString.Length == 1)
                {
                    secondsString = "0" + secondsString;
                }
                return roundedMinutes.ToString() + ":" + secondsString + "/km";
            }
    }
    public string? AveragePace 
    {
        get
            {
                decimal kmh = TotalDistanceInMeters / TotalElapsedTimeInSec * 3600/1000;
                decimal roundedKmh = Math.Round(kmh, 1);
                return roundedKmh.ToString() + " km/h";
            }
    }
    public string? AthleteId { get; set; }
}
