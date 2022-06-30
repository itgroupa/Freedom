using Freedom.Auth.Business;
using Freedom.Auth.Cache;
using Freedom.Auth.Dal;
using Freedom.Auth.Web.Interfaces;
using Freedom.Auth.Web.Services;
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
            .AddTransient<IUserViewService, UserViewService>()
            .AddBusiness();

        return collection
            .BuildServiceProvider();
    }
}
