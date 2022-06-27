using AutoMapper;
using Freedom.Auth.Business.Models.Certificates;
using Freedom.Auth.DataSchema.Models.Certificates;

namespace Freedom.Auth.Business.Mappers.ToData.Certificates;

internal class UpdateCertificateBusinessToUpdateCertificateData : Profile
{
    public UpdateCertificateBusinessToUpdateCertificateData()
    {
        CreateMap<UpdateCertificateBusiness, UpdateCertificateData>()
            .ForMember(r => r.Data, r => r.Ignore());
    }
}
