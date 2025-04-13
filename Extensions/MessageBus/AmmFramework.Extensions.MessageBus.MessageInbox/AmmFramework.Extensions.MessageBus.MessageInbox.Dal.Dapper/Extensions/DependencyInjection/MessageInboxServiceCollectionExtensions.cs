using AmmFramework.Extensions.MessageBus.Abstractions;
using AmmFramework.Extensions.MessageBus.MessageInbox.Dal.Dapper.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AmmFramework.Extensions.MessageBus.MessageInbox.Dal.Dapper.Extensions.DependencyInjection;

public static class MessageInboxServiceCollectionExtensions
{
    public static IServiceCollection AddFrameworkMessageInboxDalSql(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessageInboxDalDapperOptions>(configuration);
        AddServices(services);
        return services;
    }

    public static IServiceCollection AddFrameworkMessageInboxDalSql(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddFrameworkMessageInboxDalSql(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddFrameworkMessageInboxDalSql(this IServiceCollection services, Action<MessageInboxDalDapperOptions> setupAction)
    {
        services.Configure(setupAction);
        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddSingleton<IMessageInboxItemRepository, SqlMessageInboxItemRepository>();
    }
}