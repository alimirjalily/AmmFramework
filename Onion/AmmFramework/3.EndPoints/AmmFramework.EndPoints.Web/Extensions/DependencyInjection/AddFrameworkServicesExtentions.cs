using AmmFramework.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace AmmFramework.EndPoints.Web.Extensions.DependencyInjection;
public static class AddFrameworkServicesExtensions
{
    public static IServiceCollection AddFrameworkUtilityServices(
        this IServiceCollection services)
    {
        services.AddTransient<FrameworkServices>();
        return services;
    }
}