using AutoMapper;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;

namespace Freedom.Auth.Web.Tests;

public class MapperTests
{
    [Test]
    public void MapperTest()
    {
        var mapper = DiProvider.GetProvider().GetService<IMapper>();

        mapper?.ConfigurationProvider.AssertConfigurationIsValid();
        Assert.Pass();
    }
}
