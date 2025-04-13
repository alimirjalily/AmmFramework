using AmmFramework.Extensions.UsersManagement.Abstractions;
using AmmFramework.Extensions.UsersManagement.Options;
using AmmFramework.Extensions.UsersManagement.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AmmFramework.Extensions.UsersManagement.Extensions.DependencyInjection;

public static class UserInfoServiceCollectionExtensions
{

    public static IServiceCollection AddFrameworkWebUserInfoService(this IServiceCollection services,
        IConfiguration configuration, bool useFake = false)
    {
        if (useFake)
        {
            services.AddSingleton<IUserInfoService, FakeUserInfoService>();

        }
        else
        {
            services.Configure<UserManagementOptions>(configuration);
            services.AddSingleton<IUserInfoService, WebUserInfoService>();

        }

        return services;
    }


    public static IServiceCollection AddFrameworkWebUserInfoService(this IServiceCollection services,
        IConfiguration configuration, string sectionName, bool useFake = false)
    {
        services.AddFrameworkWebUserInfoService(configuration.GetSection(sectionName), useFake);
        return services;
    }

    public static IServiceCollection AddFrameworkWebUserInfoService(this IServiceCollection services,
        Action<UserManagementOptions> setupAction, bool useFake = false)
    {
        if (useFake)
        {
            services.AddSingleton<IUserInfoService, FakeUserInfoService>();

        }
        else
        {
            services.Configure(setupAction);
            services.AddSingleton<IUserInfoService, WebUserInfoService>();

        }

        return services;
    }
}