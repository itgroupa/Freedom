using Freedom.Auth.Business;
using Freedom.Auth.Cache;
using Freedom.Auth.Dal;
using Freedom.Auth.DataSchema;
using Freedom.Common.Mapper;
using Freedom.Tests.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Freedom.Auth.Web.Tests;

public static class DiProvider
{
    public static ServiceProvider GetProvider()
    {
        var collection = new ServiceCollection()
            .AddMapperFiles()
            .AddDal(new FakeDbRepository())
            .AddCache(new FakeCacheRepository())
            .AddBusiness();

        return collection
            .BuildServiceProvider();
    }
}
