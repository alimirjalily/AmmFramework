using AmmFramework.Extensions.MessageBus.Abstractions;
using AmmFramework.Extensions.MessageBus.MessageInbox.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AmmFramework.Extensions.MessageBus.MessageInbox.Extensions.DependencyInjection;

public static class MessageInboxServiceCollectionExtensions
{
    public static IServiceCollection AddFrameworkMessageInbox(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessageInboxOptions>(configuration);
        AddServices(services);
        return services;
    }

    public static IServiceCollection AddFrameworkMessageInbox(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddFrameworkMessageInbox(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddFrameworkMessageInbox(this IServiceCollection services, Action<MessageInboxOptions> setupAction)
    {
        services.Configure(setupAction);
        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IMessageConsumer, InboxMessageConsumer>();
    }
}