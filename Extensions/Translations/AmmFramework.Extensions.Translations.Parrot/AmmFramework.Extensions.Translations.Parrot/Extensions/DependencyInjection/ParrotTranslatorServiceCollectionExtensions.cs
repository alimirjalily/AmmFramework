using AmmFramework.Extensions.Translations.Abstractions;
using AmmFramework.Extensions.Translations.Parrot.Options;
using AmmFramework.Extensions.Translations.Parrot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AmmFramework.Extensions.Translations.Parrot.Extensions.DependencyInjection;

public static class ParrotTranslatorServiceCollectionExtensions
{
    public static IServiceCollection AddFrameworkParrotTranslator(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ITranslator, ParrotTranslator>();
        services.Configure<ParrotTranslatorOptions>(configuration);
        return services;
    }

    public static IServiceCollection AddFrameworkParrotTranslator(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddFrameworkParrotTranslator(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddFrameworkParrotTranslator(this IServiceCollection services, Action<ParrotTranslatorOptions> setupAction)
    {
        services.AddSingleton<ITranslator, ParrotTranslator>();
        services.Configure(setupAction);
        return services;
    }
}