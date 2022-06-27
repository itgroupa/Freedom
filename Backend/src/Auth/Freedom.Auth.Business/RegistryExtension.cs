using System.Reflection;
using Freedom.Auth.DataSchema;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Freedom.Auth.Business;

public static class RegistryExtension
{
    public static IServiceCollection AddBusiness(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddDataSchema()
            .AddMediatR(Assembly.GetExecutingAssembly());
    }
}
