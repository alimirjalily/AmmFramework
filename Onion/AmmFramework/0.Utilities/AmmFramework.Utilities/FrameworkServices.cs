using AmmFramework.Extensions.Caching.Abstractions;
using AmmFramework.Extensions.ObjectMappers.Abstractions;
using AmmFramework.Extensions.Serializers.Abstractions;
using AmmFramework.Extensions.Translations.Abstractions;
using AmmFramework.Extensions.UsersManagement.Abstractions;
using Microsoft.Extensions.Logging;

namespace AmmFramework.Utilities;

public class FrameworkServices
{
    public readonly ITranslator Translator;
    public readonly ICacheAdapter CacheAdapter;
    public readonly IMapperAdapter MapperFacade;
    public readonly ILoggerFactory LoggerFactory;
    public readonly IJsonSerializer Serializer;
    public readonly IUserInfoService UserInfoService;

    public FrameworkServices(ITranslator translator,
        ILoggerFactory loggerFactory,
        IJsonSerializer serializer,
        IUserInfoService userInfoService,
        ICacheAdapter cacheAdapter,
        IMapperAdapter mapperFacade)
    {
        Translator = translator;
        LoggerFactory = loggerFactory;
        Serializer = serializer;
        UserInfoService = userInfoService;
        CacheAdapter = cacheAdapter;
        MapperFacade = mapperFacade;
    }
}