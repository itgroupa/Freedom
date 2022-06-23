using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Freedom.Access.Mongo;

public static class RegistryExtension
{
    public static IServiceCollection AddDbRepository(this IServiceCollection serviceCollection,
        IConfiguration? configuration)
    {
        serviceCollection.AddSingleton<IDbRepository, DbRepository>();
        serviceCollection.Configure<MongoConfiguration>(configuration);
        return serviceCollection;
    }

    public static IServiceCollection AddDbRepository(this IServiceCollection serviceCollection,
        IDbRepository repository)
    {
        serviceCollection.AddSingleton(repository);

        return serviceCollection;
    }
}
