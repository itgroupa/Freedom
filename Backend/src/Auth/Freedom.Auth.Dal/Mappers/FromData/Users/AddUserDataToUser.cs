using AutoMapper;
using Freedom.Auth.Dal.Models;
using Freedom.Auth.DataSchema.Models.Users;

namespace Freedom.Auth.Dal.Mappers.FromData.Users;

internal class AddUserToUser : Profile
{
    public AddUserToUser()
    {
        CreateMap<AddUserData, User>()
            .ForMember(r=>r.Id, r=>
                r.MapFrom(m=>Guid.NewGuid()))
            .ForMember(r=>r.Role, r=>
                r.Ignore());
    }
}
