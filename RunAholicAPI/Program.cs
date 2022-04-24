var builder = WebApplication.CreateBuilder(args);

var mongoSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(mongoSettings);

var authSettings = builder.Configuration.GetSection("AuthenticationSettings");
builder.Services.Configure<AuthSettings>(authSettings);

builder.Services.AddTransient<IMongoContext,MongoContext>();

builder.Services.AddTransient<IActivityRepository,ActivityRepository>();
// builder.Services.AddTransient<IAthleteRepository,AthleteRepository>();
// builder.Services.AddTransient<IStatsRepository,StatsRepository>();

builder.Services.AddTransient<IRunAholicService,RunAholicService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
// builder.Services
//     .AddGraphQLServer()
//     .AddQueryType<Queries>()
//     .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
//     .AddMutationType<Mutation>();

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ActivityValidator>());
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters(){
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["AuthenticationSettings:Issuer"],
        ValidAudience = builder.Configuration["AuthenticationSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(builder.Configuration["AuthenticationSettings:SecretForKey"]))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBeFromHooglede",policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("city","Hooglede");
    });
});
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// app.MapGraphQL();
app.MapGet("/", () => "Hello World!");
app.MapPost("/activities",async (IValidator<Activity> validator, IRunAholicService runAholicService, Activity activity) =>
{
    var validationResult = validator.Validate(activity);
    if (validationResult.IsValid)
    {
        activity = await runAholicService.AddActivity(activity);
        return Results.Created($"/activity/{activity.ActivityId}",activity);
    }
    else
    {
        var errors = validationResult.Errors.Select(x => new{errors = x.ErrorMessage});
        return Results.BadRequest(errors);
    }
    
});

app.MapPost("/authenticate", (IAuthenticationService authenticationService,string userName, string password, AuthenticationRequestBody authenticationRequestBody,IOptions<AuthSettings>authSettings) =>
{
    var user = authenticationService.ValidateUser(authenticationRequestBody.username, authenticationRequestBody.password);
    if (user == null)
    {
        return Results.Unauthorized();
    }

    // Token maken
    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(authSettings.Value.SecretForKey));
    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claimsForToken = new List<Claim>();
    claimsForToken.Add(new Claim("sub", "1"));
    claimsForToken.Add(new Claim("given_name", userName));
    claimsForToken.Add(new Claim("city", "Hooglede"));

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