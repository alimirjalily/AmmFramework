using AmmFramework.Utilities.ScalarRegistration.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;

namespace AmmFramework.Utilities.ScalarRegistration.Extensions.DependencyInjection;

public static class ScalarServiceCollectionExtensions
{
    public static IServiceCollection AddFrameworkScalar(this IServiceCollection services, IConfiguration configuration, string sectionName)
        => services.AddFrameworkScalar(configuration.GetSection(sectionName));

    public static IServiceCollection AddFrameworkScalar(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ScalarOption>(configuration);
        var option = configuration.Get<ScalarOption>() ?? new();

        return services.AddService(option);
    }

    public static IServiceCollection AddFrameworkScalar(this IServiceCollection services, Action<ScalarOption> action)
    {
        services.Configure(action);
        var option = new ScalarOption();
        action.Invoke(option);

        return services.AddService(option);
    }

    private static IServiceCollection AddService(this IServiceCollection services, ScalarOption option)
    {
        if (option.Enabled)
        {
            services.AddOpenApi();
        }

        return services;
    }

    public static void UseFrameworkScalar(this WebApplication app)
    {
        var option = app.Services.GetRequiredService<IOptions<ScalarOption>>().Value;
        if (option.Enabled)
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
    }
}