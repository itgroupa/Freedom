using System;
using System.Net;
using System.Security.Claims;
using Freedom.Auth.DataSchema.Consts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Moq;

namespace Freedom.Auth.Web.Tests.Utils;

public static class FakeHttpContextBuilder
{
    public static IHttpContextAccessor Build()
    {
        var moqHttpContext = new Mock<HttpContext>();
        moqHttpContext.Setup(x => x.User.Claims).Returns(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Email, "tester@tester.com"),
            new Claim(ClaimTypes.Name, "tester"),
            new Claim(ClaimTypes.Role, Roles.Admin)
        });
        moqHttpContext.Setup(x => x.User.Identity.IsAuthenticated).Returns(true);
        var moqHttpContextAccessor = new HttpContextAccessor
        {
            HttpContext = moqHttpContext.Object
        };

        moqHttpContext.Setup(x => x.Connection.RemoteIpAddress).Returns(IPAddress.Parse("127.0.0.1"));

        var requestFeature = new HttpRequestFeature();
        var featureCollection = new FeatureCollection();

        requestFeature.Headers = new HeaderDictionary();
        requestFeature.Headers.Add(HeaderNames.Cookie, new StringValues("SESSION_ID" + "=" + Guid.NewGuid()));

        featureCollection.Set<IHttpRequestFeature>(requestFeature);

        var cookiesFeature = new RequestCookiesFeature(featureCollection);

        moqHttpContext.Setup(x => x.Request.Cookies).Returns(cookiesFeature.Cookies);

        return moqHttpContextAccessor;
    }
}
