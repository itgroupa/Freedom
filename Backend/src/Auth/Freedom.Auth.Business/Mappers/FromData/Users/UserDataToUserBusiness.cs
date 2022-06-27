using AutoMapper;
using Freedom.Auth.Business.Models.Users;
using Freedom.Auth.DataSchema.Models.Users;

namespace Freedom.Auth.Business.Mappers.FromData.Users;

internal class UserDataToUserBusiness : Profile
{
    public UserDataToUserBusiness()
    {
        CreateMap<UserData, UserBusiness>();
    }
}
