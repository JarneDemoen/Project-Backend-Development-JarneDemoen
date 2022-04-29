namespace RunAholicAPI.Models;

public class Stats
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? StatsId { get; set; }
    public decimal TotalDistanceInMeters { get; set; }
    public int NumberOfActivities { get; set; }
    public decimal TotalElapsedTimeInSec { get; set; }
    public string? TotalElapsedTimeInHMS 
    { 
        get
            {
                TimeSpan t = TimeSpan.FromSeconds(Convert.ToInt32(TotalElapsedTimeInSec));
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
                decimal seconds = 0; 
                decimal roundedMinutes = 0;
                if (TotalElapsedTimeInSec != 0)
                {
                    // Verkregen seconden die in Elapsed_time zitten omzetten naar een string in formaat (H:)MM:SS
                    seconds = TotalElapsedTimeInSec / (TotalDistanceInMeters / 1000);
                    decimal minutes = seconds / 60;
                    roundedMinutes = Math.Floor(minutes);
                    decimal rest = minutes - roundedMinutes;
                    seconds = rest * 60;
                }

                // afronden
                seconds = Math.Round(seconds);
                string secondsString = seconds.ToString();
                if (secondsString.Length == 1)
                {
                    secondsString = "0" + secondsString;
                }
                return roundedMinutes.ToString() + ":" + secondsString + " min/km";
            }
    }
    public string? AveragePace 
    {
        get
            {
                if (TotalDistanceInMeters != TotalElapsedTimeInSec && TotalElapsedTimeInSec != 0)
                {
                    decimal kmh = TotalDistanceInMeters / TotalElapsedTimeInSec * 3600/1000;
                    decimal roundedKmh = Math.Round(kmh, 1);
                    return roundedKmh.ToString() + " km/h";
                }
                else
                {
                    return "0 km/h";
                }
            }
    }
    public string? AthleteId { get; set; }
}
