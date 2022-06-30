using AutoMapper;
using Freedom.Auth.Business.Models.Users;
using Freedom.Auth.Web.Models;

namespace Freedom.Auth.Web.Mapper.FromBusiness.Users;

internal class UserBusinessToUserView : Profile
{
    public UserBusinessToUserView()
    {
        CreateMap<UserBusiness, UserView>();
    }
}
