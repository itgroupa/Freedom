using AutoMapper;
using Freedom.Auth.DataSchema.Models.Users;

namespace Freedom.Auth.Dal.Mappers.FromData.Users;

internal class UpdateUserDataToUser : Profile
{
    public UpdateUserDataToUser()
    {
        CreateMap<UpdateUserData, UserData>()
            .ForMember(r => r.Id, r => r.Ignore())
            .ForMember(r => r.Provider, r => r.Ignore())
            .ForMember(r => r.Role, r => r.Ignore());
    }
}
