var builder = WebApplication.CreateBuilder(args);

var mongoSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(mongoSettings);

var authSettings = builder.Configuration.GetSection("AuthenticationSettings");
builder.Services.Configure<AuthSettings>(authSettings);

builder.Services.AddTransient<IMongoContext,MongoContext>();

builder.Services.AddTransient<IActivityRepository,ActivityRepository>();
builder.Services.AddTransient<IAthleteRepository,AthleteRepository>();
builder.Services.AddTransient<IStatsRepository,StatsRepository>();

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

//ACTIVITIES
app.MapGet("/activities",[Authorize(Policy="MustBeFromHooglede")] async (IRunAholicService runAholicService,ClaimsPrincipal user) =>
{
    return Results.Ok(await runAholicService.GetAllActivities());
});

app.MapGet("/activities/{activityId}",[Authorize(Policy="MustBeFromHooglede")] async (IRunAholicService runAholicService,ClaimsPrincipal user,string activityId) =>
{
    if (activityId.Length != 24)
    {
        return Results.BadRequest("Invalid activityId");
    }
    var result = await runAholicService.GetActivity(activityId);
    if (result != null)
    {
        return Results.Ok(result);
    }
    else
    {
        return Results.NotFound($"Activity {activityId} is not found");
    }
});

app.MapGet("/activities/athlete/{athleteId}",[Authorize(Policy="MustBeFromHooglede")] async (IRunAholicService runAholicService,ClaimsPrincipal user,string athleteId) =>
{
    if (athleteId.Length != 24)
    {
        return Results.BadRequest("Invalid athleteId");
    }
    var result = await runAholicService.GetAthlete(athleteId);
    if (result != null)
    {
        return Results.Ok(await runAholicService.GetActivitiesByAthleteId(athleteId));
    }
    else
    {
        return Results.NotFound($"Athlete {athleteId} is not found");
    }
});

app.MapPost("/activities",[Authorize(Policy ="MustBeFromHooglede")] async (IValidator<Activity> validator, IRunAholicService runAholicService, Activity newActivity, ClaimsPrincipal user) =>
{
    var validationResult = validator.Validate(newActivity);
    if (validationResult.IsValid)
    {
        newActivity = await runAholicService.AddActivity(newActivity);
        return Results.Created($"/activity/{newActivity.ActivityId}",newActivity);
    }
    else
    {
        var errors = validationResult.Errors.Select(x => new{errors = x.ErrorMessage});
        return Results.BadRequest(errors);
    }
    
});

app.MapPut("/activities",[Authorize(Policy ="MustBeFromHooglede")] async (IValidator<Activity> validator, IRunAholicService runAholicService, Activity updatedActivity, ClaimsPrincipal user) =>
{
    if (updatedActivity.ActivityId.Length != 24)
    {
        return Results.BadRequest("Invalid activityId");
    }
    if (updatedActivity.AthleteId.Length != 24)
    {
        return Results.BadRequest("Invalid athleteId");
    }
    var validationResult = validator.Validate(updatedActivity);
    if (validationResult.IsValid)
    {
        var result = await runAholicService.GetActivity(updatedActivity.ActivityId); 
        var athlete = await runAholicService.GetAthlete(updatedActivity.AthleteId);
        if (result != null && athlete != null)
        {
            await runAholicService.UpdateActivity(updatedActivity);
            return Results.Created($"/activity/{updatedActivity.ActivityId}",updatedActivity);
        }
        else
        {
            if(result == null)
            {
                return Results.NotFound($"Activity {updatedActivity.ActivityId} is not found");
            }
            if (athlete == null)
            {
                return Results.NotFound($"Athlete {updatedActivity.AthleteId} is not found");
            }
            else
            {
                return Results.BadRequest($"Something went wrong");
            }
        }
    }
    else
    {
        var errors = validationResult.Errors.Select(x => new{errors = x.ErrorMessage});
        return Results.BadRequest(errors);
    }
});

app.MapDelete("/activities/{activityId}",[Authorize(Policy ="MustBeFromHooglede")] async (IRunAholicService runAholicService, string activityId, ClaimsPrincipal user) =>
{
    if (activityId.Length != 24)
    {
        return Results.BadRequest("Invalid activityId");
    }
    var result = await runAholicService.GetActivity(activityId);
    if (result != null)
    {
        await runAholicService.DeleteActivity(activityId);
        return Results.Ok($"Activity {activityId} is deleted succesfully");
    }
    else
    {
        return Results.NotFound($"Activity {activityId} is not found");
    }
    
});

// ATHLETE

app.MapGet("/athletes",[Authorize(Policy="MustBeFromHooglede")] async (IRunAholicService runAholicService,ClaimsPrincipal user) =>
{
    return Results.Ok(await runAholicService.GetAllAthletes());
});

app.MapGet("/athletes/{athleteId}",[Authorize(Policy="MustBeFromHooglede")] async (IRunAholicService runAholicService,ClaimsPrincipal user,string athleteId) =>
{
    if (athleteId.Length != 24)
    {
        return Results.BadRequest("Invalid athleteId");
    }
    var result = await runAholicService.GetAthlete(athleteId);
    if (result != null)
    {
        return Results.Ok(result);
    }
    else
    {
        return Results.NotFound($"Athlete {athleteId} is not found");
    }
});

app.MapPost("/athletes",[Authorize(Policy ="MustBeFromHooglede")] async (IValidator<Athlete> validator, IRunAholicService runAholicService, Athlete newAthlete, ClaimsPrincipal user) =>
{
    var validationResult = validator.Validate(newAthlete);
    if (validationResult.IsValid)
    {
        newAthlete = await runAholicService.AddAthlete(newAthlete);
        return Results.Created($"/athlete/{newAthlete.AthleteId}",newAthlete);
    }
    else
    {
        var errors = validationResult.Errors.Select(x => new{errors = x.ErrorMessage});
        return Results.BadRequest(errors);
    }
    
});

app.MapPut("/athletes",[Authorize(Policy ="MustBeFromHooglede")] async (IValidator<Athlete> validator, IRunAholicService runAholicService, Athlete updatedAthlete, ClaimsPrincipal user) =>
{
    if (updatedAthlete.AthleteId.Length != 24)
    {
        return Results.BadRequest("Invalid athleteId");
    }
    var validationResult = validator.Validate(updatedAthlete);
    if (validationResult.IsValid)
    {
        var result = await runAholicService.GetAthlete(updatedAthlete.AthleteId); 
        if (result != null)
        {
            await runAholicService.UpdateAthlete(updatedAthlete);
            return Results.Created($"/acthlete/{updatedAthlete.AthleteId}",updatedAthlete);
        }
        else
        {
            return Results.NotFound($"Athlete {updatedAthlete.AthleteId} is not found");
        }
    }
    else
    {
        var errors = validationResult.Errors.Select(x => new{errors = x.ErrorMessage});
        return Results.BadRequest(errors);
    }
});

app.MapDelete("/athletes/{athleteId}",[Authorize(Policy ="MustBeFromHooglede")] async (IRunAholicService runAholicService, string athleteId, ClaimsPrincipal user) =>
{
    if (athleteId.Length != 24)
    {
        return Results.BadRequest("Invalid athleteId");
    }
    var result = await runAholicService.GetAthlete(athleteId);
    if (result != null)
    {
        await runAholicService.DeleteAthlete(athleteId);
        return Results.Ok($"Athlete {athleteId} is deleted succesfully");
    }
    else
    {
        return Results.NotFound($"Athlete {athleteId} is not found");
    }
    
});

// Stats
app.MapGet("/stats",[Authorize(Policy="MustBeFromHooglede")] async (IRunAholicService runAholicService,ClaimsPrincipal user) =>
{
    return Results.Ok(await runAholicService.GetAllStats());
});

app.MapGet("/stats/athlete/{athleteId}",[Authorize(Policy="MustBeFromHooglede")] async (IRunAholicService runAholicService,ClaimsPrincipal user,string athleteId) =>
{
    if (athleteId.Length != 24)
    {
        return Results.BadRequest("Invalid athleteId");
    }
    var result = await runAholicService.GetAthlete(athleteId);
    if (result != null)
    {
        return Results.Ok(await runAholicService.GetAthleteStats(athleteId));
    }
    else
    {
        return Results.NotFound($"Athlete {athleteId} is not found");
    }
    
});


// AUTHORIZATION

app.MapPost("/authenticate", (IAuthenticationService authenticationService,AuthenticationRequestBody authenticationRequestBody,IOptions<AuthSettings> authSettings) =>
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
    claimsForToken.Add(new Claim("given_name",authenticationRequestBody.username));
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

app.Run("http://0.0.0.0:3000");
// public partial class Program { }