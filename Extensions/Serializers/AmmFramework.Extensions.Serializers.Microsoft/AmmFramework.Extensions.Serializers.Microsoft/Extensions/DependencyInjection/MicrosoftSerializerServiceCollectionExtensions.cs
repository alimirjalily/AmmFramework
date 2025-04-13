using AmmFramework.Extensions.Serializers.Abstractions;
using AmmFramework.Extensions.Serializers.Microsoft.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AmmFramework.Extensions.Serializers.Microsoft.Extensions.DependencyInjection;

public static class MicrosoftSerializerServiceCollectionExtensions
{
    public static IServiceCollection AddFrameworkMicrosoftSerializer(this IServiceCollection services)
        => services.AddSingleton<IJsonSerializer, MicrosoftSerializer>();
}