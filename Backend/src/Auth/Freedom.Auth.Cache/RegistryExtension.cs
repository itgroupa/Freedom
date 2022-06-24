using Freedom.Access.Redis;
using Freedom.Auth.Cache.Services;
using Freedom.Auth.DataSchema.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Freedom.Auth.Cache;

public static class RegistryExtension
{
    public static IServiceCollection AddCache(this IServiceCollection serviceCollection, IConfiguration? configuration)
    {
        serviceCollection.AddCacheRepository(configuration);

        ServiceRegistry(serviceCollection);

        return serviceCollection;
    }

    public static IServiceCollection AddCache(this IServiceCollection serviceCollection, CacheRepository cacheRepository)
    {
        serviceCollection.AddCacheRepository(cacheRepository);

        ServiceRegistry(serviceCollection);

        return serviceCollection;
    }

    private static void ServiceRegistry(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IUserCacheService, UserCacheService>();
    }
}
