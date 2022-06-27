using AutoMapper;
using Freedom.Auth.Dal.Models;
using Freedom.Auth.DataSchema.Models.Users;

namespace Freedom.Auth.Dal.Mappers.ToData.Users;

internal class UserToUserData : Profile
{
    public UserToUserData()
    {
        CreateMap<User, UserData>();
    }
}
