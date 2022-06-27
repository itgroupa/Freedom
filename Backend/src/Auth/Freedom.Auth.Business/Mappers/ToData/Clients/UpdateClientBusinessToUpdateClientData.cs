using AutoMapper;
using Freedom.Auth.Business.Models.Clients;
using Freedom.Auth.DataSchema.Models.Clients;

namespace Freedom.Auth.Business.Mappers.ToData.Clients;

internal class UpdateClientBusinessToUpdateClientData : Profile
{
    public UpdateClientBusinessToUpdateClientData()
    {
        CreateMap<UpdateClientBusiness, UpdateClientData>();
    }
}
