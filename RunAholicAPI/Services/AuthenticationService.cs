namespace RunAholicAPI.Services;

// public record UserInfo(string username,string name,string city);

// public record AuthenticationRequestBody(string username,string password);

// public interface IAuthenticationService
// {
//     UserInfo ValidateUser(string username, string password);
// }

// public class AuthenticationService : IAuthenticationService
// {
//     private readonly AuthSettings _authSettings;
//     public AuthenticationService(IOptions<AuthSettings> authSettings)
//     {
//         _authSettings = authSettings.Value;
//     }
//     public UserInfo ValidateUser(string username, string password)
//     {
//         // Normaal firebase
//         return new UserInfo("jarned", "Demoen Jarne", "Hooglede");
//     }
// }
public record UserInfo(string username, string name, string city);
public record AuthenticationRequestBody(string username, string password);

public interface IAuthenticationService
{
    UserInfo ValidateUser(string username, string password);
}

public class AuthenticationService : IAuthenticationService
{
    public UserInfo ValidateUser(string username, string password)
    {
        return new UserInfo("jarned", "Demoen Jarne", "Hooglede");
    }
}