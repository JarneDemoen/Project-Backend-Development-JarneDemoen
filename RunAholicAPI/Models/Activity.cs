namespace RunAholicAPI.Models;

public class Activity
{
    public string? ActivityId { get; set; }
    public string? Name { get; set; }
    public DateTime StartDateLocal { get; set; }
    public int ElapsedTimeInSec { get; set; }
    public string? Description { get; set; }
    public int DistanceInMeters { get; set; }
    public string? Tempo
    {
        get
            {
                // Verkregen seconden die in Elapsed_time zitten omzetten naar een string in formaat MM:SS
                decimal seconds = ElapsedTimeInSec / (DistanceInMeters / 1000);
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
    public string? Pace 
    {
        get
            {
                decimal kmh = DistanceInMeters / ElapsedTimeInSec * 3600/1000;
                decimal roundedKmh = Math.Round(kmh, 1);
                return roundedKmh.ToString() + " km/h";
            }
    }
    public string ElapsedTimeInHMS
    {
        get
        {
            TimeSpan t = TimeSpan.FromSeconds(ElapsedTimeInSec);
            string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
            t.Hours,  
            t.Minutes, 
            t.Seconds);
            return answer;
        }    
    }
    public string? AthleteId { get; set; }
}