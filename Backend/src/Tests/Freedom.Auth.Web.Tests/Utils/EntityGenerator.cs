using AutoFixture;
using Freedom.Auth.Web.Models;

namespace Freedom.Auth.Web.Tests.Utils;

public static class EntityGenerator
{
    public static AddUserView GenerateAddUserView() => new Fixture()
        .Build<AddUserView>()
        .Create();
}
