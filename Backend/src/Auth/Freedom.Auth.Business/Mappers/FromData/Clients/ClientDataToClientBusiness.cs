using AutoMapper;
using Freedom.Auth.Business.Models.Clients;
using Freedom.Auth.DataSchema.Models.Clients;

namespace Freedom.Auth.Business.Mappers.FromData.Clients;

internal class ClientDataToClientBusiness : Profile
{
    public ClientDataToClientBusiness()
    {
        CreateMap<ClientData, ClientBusiness>();
    }
}
