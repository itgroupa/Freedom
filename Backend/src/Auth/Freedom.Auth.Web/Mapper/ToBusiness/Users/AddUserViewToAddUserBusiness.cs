using AutoMapper;
using Freedom.Auth.Business.Models.Users;
using Freedom.Auth.Web.Models;

namespace Freedom.Auth.Web.Mapper.ToBusiness.Users;

internal class AddUserViewToAddUserBusiness : Profile
{
    public AddUserViewToAddUserBusiness()
    {
        CreateMap<AddUserView, AddUserBusiness>()
            .ForMember(r => r.Provider,
                r => r.Ignore());
    }
}
