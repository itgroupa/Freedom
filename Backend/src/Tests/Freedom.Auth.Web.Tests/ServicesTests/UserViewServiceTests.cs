using System;
using System.Threading.Tasks;
using Freedom.Auth.Web.Const;
using Freedom.Auth.Web.Interfaces;
using Freedom.Auth.Web.Models.Users;
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

        var addUserResult = await _service.AddAsync(newUser, Providers.Freedom);

        Assert.IsNotNull(addUserResult);

        var pageUserResult = await _service.GetAsync(0, 10);

        Assert.IsTrue(pageUserResult.Count != 0);

        var signInResult = await _service.SignInAsync(new SignInView
        {
            Email = newUser.Email,
            Password = newUser.Password,
            Token = "token"
        });

        Assert.IsTrue(signInResult.Succeeded);
    }
}
