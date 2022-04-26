namespace RunAholicAPI.GraphQL;

public record AddActivityInput(string name, string StartDateLocal,int ElapsedTimeInSec,int DistanceInMeters,string athleteId,string Description);