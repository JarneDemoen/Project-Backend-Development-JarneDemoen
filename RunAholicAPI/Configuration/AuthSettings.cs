namespace RunAholicAPI.Configuration;

public class AuthSettings
{
    public string SecretForKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}