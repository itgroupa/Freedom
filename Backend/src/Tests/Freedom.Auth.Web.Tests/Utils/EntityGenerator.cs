using AutoFixture;
using Freedom.Auth.Web.Models.Users;

namespace Freedom.Auth.Web.Tests.Utils;

public static class EntityGenerator
{
    public static AddUserView GenerateAddUserView() => new Fixture()
        .Build<AddUserView>()
        .Create();
}
