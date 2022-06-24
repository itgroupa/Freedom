using Freedom.Auth.DataSchema.Interfaces;
using Freedom.Auth.DataSchema.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Freedom.Auth.DataSchema;

public static class RegistryExtension
{
    public static IServiceCollection AddDataSchema(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IUserSchemaService, UserSchemaService>();

        return serviceCollection;
    }
}
