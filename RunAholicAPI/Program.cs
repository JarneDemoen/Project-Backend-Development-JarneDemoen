var builder = WebApplication.CreateBuilder(args);
var mongoSettings = builder.Configuration.GetSection("MongoConnection");
var authSettings = builder.Configuration.GetSection("AuthenticationSettings");

builder.Services.Configure<DatabaseSettings>(mongoSettings);
builder.Services.Configure<AuthSettings>(authSettings);

builder.Services.AddTransient<IMongoContext,MongoContext>();

// builder.Services.AddTransient<IActivityRepository,ActivityRepository>();
// builder.Services.AddTransient<IAthleteRepository,AthleteRepository>();
// builder.Services.AddTransient<IStatsRepository,StatsRepository>();

// builder.Services.AddTransient<IRunAholicService,RunAholicService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
// builder.Services
//     .AddGraphQLServer()
//     .AddQueryType<Queries>()
//     .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
//     .AddMutationType<Mutation>();

// VALIDATIE NOG TOEVOEGEN

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapGraphQL();
app.MapGet("/", () => "Hello World!");

app.MapPost("/authenticate", (IAuthenticationService authenticationService, string userName, string password) =>
{
    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(authSettings.Value.SecretForKey));
    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claimsForToken = new List<Claim>();
    claimsForToken.Add(new Claim("sub", "1"));
    claimsForToken.Add(new Claim("given_name", userName));
    claimsForToken.Add(new Claim("city", "Gent"));

    var jwtSecurityToken = new JwtSecurityToken(
        authSettings.Value.Issuer,
        authSettings.Value.Audience,
        claimsForToken,
        DateTime.UtcNow,
        DateTime.UtcNow.AddHours(1),
        signingCredentials
    );

    var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

    return Results.Ok(tokenToReturn);

});

app.Run("http://localhost:3000");
// public partial class Program { }