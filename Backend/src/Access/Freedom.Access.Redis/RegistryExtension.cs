using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Freedom.Access.Redis;

public static class RegistryExtension
{
    public static IServiceCollection AddCacheRepository(this IServiceCollection serviceCollection,
        IConfiguration? configuration)
    {
        serviceCollection.AddSingleton<CacheRepository, RedisRepository>();
        serviceCollection.Configure<RedisConfiguration>(configuration);
        return serviceCollection;
    }

    public static IServiceCollection AddCacheRepository(this IServiceCollection serviceCollection, CacheRepository repository)
    {
        serviceCollection.AddSingleton(repository);
        return serviceCollection;
    }
}
