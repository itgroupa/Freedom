using AutoMapper;
using Freedom.Auth.Business.Models.Users;
using Freedom.Auth.DataSchema.Models.Users;
using Freedom.Common.Crypto;

namespace Freedom.Auth.Business.Mappers.ToData.Users;

internal class AddUserBusinessToAdduserData : Profile
{
    public AddUserBusinessToAdduserData()
    {
        CreateMap<AddUserBusiness, AddUserData>()
            .ForMember(r => r.Password, r =>
                r.MapFrom(m => m.Password.GetHashMd5()));
    }
}
