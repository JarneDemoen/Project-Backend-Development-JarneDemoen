namespace RunAholicTest.Helpers;
public class Helper
{
    public static string GenerateBearerToken() => "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwiZ2l2ZW5fbmFtZSI6Imphcm5lZCIsImNpdHkiOiJIb29nbGVkZSIsIm5iZiI6MTY1MTEzNDA4MSwiZXhwIjoxNjUzODYxNjAwLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDozMDAwIiwiYXVkIjoiUnVuQWhvbGljQVBJX3VzZXJzIn0.10ZvC37cgFn4oReACY0JRTyREfVtQTki10BMfpjHZ3c";
    public static WebApplicationFactory<Program> CreateApi()
    {
        var application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // ActivityRepository
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IActivityRepository));
                services.Remove(descriptor);
                var fakeActivityRepository = new ServiceDescriptor(typeof(IActivityRepository), typeof(FakeActivityRepository), ServiceLifetime.Transient);
                services.Add(fakeActivityRepository);

                // AthleteRepository
                descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IAthleteRepository));
                services.Remove(descriptor);
                var fakeAthleteRepository = new ServiceDescriptor(typeof(IAthleteRepository), typeof(FakeAthleteRepository), ServiceLifetime.Transient);
                services.Add(fakeAthleteRepository);

                // StatsRepository
                descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IStatsRepository));
                services.Remove(descriptor);
                var fakeStatsRepository = new ServiceDescriptor(typeof(IStatsRepository), typeof(FakeStatsRepository), ServiceLifetime.Transient);
                services.Add(fakeStatsRepository);

                // Service
                descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IRunAholicService));
                services.Remove(descriptor);
                var fakeRunAholicService = new ServiceDescriptor(typeof(IRunAholicService), typeof(FakeRunAholicService), ServiceLifetime.Transient);
                services.Add(fakeRunAholicService);
            });
        });

        return application;

    }
}