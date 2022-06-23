using Freedom.Access.Mongo;
using Freedom.Auth.Dal.Services;
using Freedom.Auth.DataSchema.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Freedom.Auth.Dal;

public static class RegistryExtension
{
    public static IServiceCollection AddDal(this IServiceCollection serviceCollection, IConfiguration? configuration)
    {
        serviceCollection.AddDbRepository(configuration);

        ServiceRegistry(serviceCollection);

        return serviceCollection;
    }

    public static IServiceCollection AddDal(this IServiceCollection serviceCollection, IDbRepository dbRepository)
    {
        serviceCollection.AddDbRepository(dbRepository);

        ServiceRegistry(serviceCollection);

        return serviceCollection;
    }

    private static void ServiceRegistry(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IUserDalService, UserDalService>();
    }
}
