using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Freedom.Common.Mapper;

public static class RegistryExtension
{
    public static IServiceCollection AddMapperFiles(this IServiceCollection collection)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var types = assemblies.SelectMany(a => a.GetTypes()).Where(x =>
                x.GetTypeInfo().IsClass && x.IsAssignableFrom(x) && x.GetTypeInfo().BaseType == typeof(Profile))
            .ToArray();
        var assembliesWithProfiles = types.Select(t => t.GetTypeInfo().Assembly).Distinct();
        collection.AddAutoMapper(assembliesWithProfiles);
        return collection;
    }
}
