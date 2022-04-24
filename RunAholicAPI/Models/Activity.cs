namespace RunAholicAPI.Models;

public class Activity
{
    public string? ActivityId { get; set; }
    public string? Name { get; set; }
    public DateTime StartDateLocal { get; set; }
    public int ElapsedTime { get; set; }
    public string? Description { get; set; }
    public decimal DistanceInKM { get; set; }
    public string? Tempo
    {
        get
            {
                TimeSpan t = TimeSpan.FromSeconds(ElapsedTime);
                return "uitwerken";
            }    
    }
    public string? Pace 
    {
        get
            {
                decimal kmh = DistanceInKM / ElapsedTime * 3600/1000;
                decimal roundedKmh = Math.Round(kmh, 1);
                return roundedKmh.ToString() + " km/h";
            }
    }
    public string ElapsedTimeInMinAndSec
    {
        get
        {
            decimal minutes = ElapsedTime / 60;
            if (minutes >= 60) // Er is meer dan een uur gelopen dus ik moet het formaat H:MM:SS weergeven
            {
                decimal hours = minutes / 60;
                decimal roundedHours = Math.Floor(hours);
                decimal restHours = hours - roundedHours;
                decimal newMinutes = restHours * 60;

                decimal roundedMinutes = Math.Floor(newMinutes);
                decimal restMinutes = newMinutes - roundedMinutes;
                decimal seconds = restMinutes * 60;
                seconds = Math.Round(seconds, 0);

                //omzetten naar string
                string hoursString = roundedHours.ToString();
                string roundedMinutesString = roundedMinutes.ToString();
                string secondsString = seconds.ToString();

                if (roundedMinutesString.Length == 1)
                {
                    roundedMinutesString = "0" + roundedMinutesString;
                }

                if (secondsString.Length == 1)
                {
                    secondsString = "0" + secondsString;
                }
                return hoursString + ":" + roundedMinutesString + ":" + secondsString;
            }
            else // Minder dan een uur --> MM:SS
            {
                decimal roundedMinutes = Math.Floor(minutes);
                decimal rest = minutes - roundedMinutes;
                decimal seconds = rest * 60;
                seconds = Math.Round(seconds, 0);

                //omzetten naar string
                string roundedMinutesString = roundedMinutes.ToString();
                string secondsString = seconds.ToString();

                if (secondsString.Length == 1)
                {
                    secondsString = "0" + secondsString;
                }
                return roundedMinutesString + ":" + secondsString;
            }
        }
    }
    public string? AthleteId { get; set; }
}