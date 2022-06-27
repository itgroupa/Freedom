using AutoMapper;
using Freedom.Auth.Dal.Models;
using Freedom.Auth.DataSchema.Models.Clients;

namespace Freedom.Auth.Dal.Mappers.ToData.Clients;

internal class ClientToClientData : Profile
{
    public ClientToClientData()
    {
        CreateMap<Client, ClientData>();
    }
}
