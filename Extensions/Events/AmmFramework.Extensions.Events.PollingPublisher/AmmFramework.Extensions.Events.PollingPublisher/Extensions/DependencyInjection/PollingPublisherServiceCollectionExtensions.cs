using AmmFramework.Extensions.Events.PollingPublisher.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AmmFramework.Extensions.Events.PollingPublisher.Extensions.DependencyInjection;

public static class PollingPublisherServiceCollectionExtensions
{
    public static IServiceCollection AddFrameworkPollingPublisher(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PollingPublisherOptions>(configuration);
        AddServices(services);
        return services;
    }

    public static IServiceCollection AddFrameworkPollingPublisher(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddFrameworkPollingPublisher(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddFrameworkPollingPublisher(this IServiceCollection services, Action<PollingPublisherOptions> setupAction)
    {
        services.Configure(setupAction);
        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddHostedService<PoolingPublisherBackgroundService>();
    }
}