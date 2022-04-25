namespace RunAholicAPI.Models;

public class Athlete
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? AthleteId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public int Age { get; set; }
}