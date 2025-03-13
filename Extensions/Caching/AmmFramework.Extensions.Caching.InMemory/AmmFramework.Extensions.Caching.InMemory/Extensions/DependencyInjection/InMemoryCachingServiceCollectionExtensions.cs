using AmmFramework.Extensions.Caching.InMemory.Services;
using AmmFramework.Extensions.Caching.Abstractions;
using AmmFramework.Extensions.Serializers.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AmmFramework.Extensions.DependencyInjection;

public static class InMemoryCachingServiceCollectionExtensions
{
    public static IServiceCollection AddFrameworkInMemoryCaching(this IServiceCollection services)
        => services.AddMemoryCache().AddTransient<ICacheAdapter, InMemoryCacheAdapter>();
}