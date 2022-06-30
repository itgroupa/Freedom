using System;
using System.Threading.Tasks;
using Freedom.Auth.Web.Const;
using Freedom.Auth.Web.Interfaces;
using Freedom.Auth.Web.Tests.Utils;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Freedom.Auth.Web.Tests.ServicesTests;

public class UserViewServiceTests
{
    private readonly IUserViewService? _service;

    public UserViewServiceTests()
    {
        _service = DiProvider.GetProvider().GetService<IUserViewService>();
    }

    [Test]
    public async Task AddUserTest()
    {
        if (_service == null) throw new NullReferenceException("service is null");

        var newUser = EntityGenerator.GenerateAddUserView();

        var result = await _service.AddAsync(newUser, Providers.Freedom);

        Assert.IsNotNull(result);

        var result2 = await _service.GetAsync(0, 10);

        Assert.IsTrue(result2.Count != 0);
    }
}
