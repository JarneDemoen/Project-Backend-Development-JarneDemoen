namespace RunAholicTest.Helpers;
public class Helper
{

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
            });
        });

        return application;

    }
}