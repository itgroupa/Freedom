using Microsoft.Extensions.DependencyInjection;

namespace Freedom.Auth.Business;

public static class RegistryExtension
{
    public static IServiceCollection AddBusiness(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}
