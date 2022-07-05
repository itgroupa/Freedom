using Freedom.Auth.Business;
using Freedom.Auth.Cache;
using Freedom.Auth.Dal;
using Freedom.Auth.Web.Configurations;
using Freedom.Auth.Web.Interfaces;
using Freedom.Auth.Web.Services;
using Freedom.Auth.Web.Tests.Utils;
using Freedom.Common.Mapper;
using Freedom.Tests.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Freedom.Auth.Web.Tests;

public static class DiProvider
{
    public static ServiceProvider GetProvider()
    {
        var captchaConfiguration = new CaptchaConfiguration
        {
            SecretKey = "key",
            SiteKey = "key"
        };
        var collection = new ServiceCollection()
            .AddMapperFiles()
            .AddDal(new FakeDbRepository())
            .AddCache(new FakeCacheRepository())
            .AddTransient<IUserViewService, UserViewService>()
            .Configure<CaptchaConfiguration>(captchaConfiguration.CopyTo)
            .AddBusiness();

        collection.AddTransient<CaptchaResponseHandler>();
        collection.AddHttpClient<ICaptchaVerificationService, CaptchaVerificationService>()
            .AddHttpMessageHandler<CaptchaResponseHandler>();


        return collection
            .BuildServiceProvider();
    }
}
