using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Freedom.Access.Redis;

public static class ConfigurationExtension
{
    public static IServiceCollection AddCacheRepository(this IServiceCollection collection,
        IConfiguration? configuration)
    {
        collection.AddSingleton<CacheRepository, RedisRepository>();
        collection.Configure<RedisConfiguration>(configuration);
        return collection;
    }
}
