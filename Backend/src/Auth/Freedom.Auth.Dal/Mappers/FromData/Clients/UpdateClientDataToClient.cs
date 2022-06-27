using AutoMapper;
using Freedom.Auth.Dal.Models;
using Freedom.Auth.DataSchema.Models.Clients;

namespace Freedom.Auth.Dal.Mappers.FromData.Clients;

internal class UpdateClientDataToClient : Profile
{
    public UpdateClientDataToClient()
    {
        CreateMap<UpdateClientData, Client>()
            .ForMember(r => r.Id, r => r.Ignore());
    }
}
