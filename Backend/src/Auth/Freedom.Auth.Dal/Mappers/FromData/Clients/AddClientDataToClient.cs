using AutoMapper;
using Freedom.Auth.Dal.Models;
using Freedom.Auth.DataSchema.Models.Clients;

namespace Freedom.Auth.Dal.Mappers.FromData.Clients;

internal class AddClientDataToClient : Profile
{
    public AddClientDataToClient()
    {
        CreateMap<AddClientData, Client>()
            .ForMember(r => r.Id, r =>
                r.MapFrom(m => Guid.NewGuid()));
    }
}
