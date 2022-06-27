using AutoMapper;
using Freedom.Auth.Business.Models.Clients;
using Freedom.Auth.DataSchema.Models.Clients;

namespace Freedom.Auth.Business.Mappers.ToData.Clients;

internal class AddClientBusinessToAddClientData : Profile
{
    public AddClientBusinessToAddClientData()
    {
        CreateMap<AddClientBusiness, AddClientData>();
    }
}
