using AutoMapper;
using Freedom.Auth.Business.Models.Certificates;
using Freedom.Auth.DataSchema.Models.Certificates;

namespace Freedom.Auth.Business.Mappers.ToData.Certificates;

internal class AddCertificateBusinessToAddCertificateData : Profile
{
    public AddCertificateBusinessToAddCertificateData()
    {
        CreateMap<AddCertificateBusiness, AddCertificateData>()
            .ForMember(r => r.Data, r => r.Ignore());
    }
}
