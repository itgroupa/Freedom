using AutoMapper;
using Freedom.Auth.Dal.Models;
using Freedom.Auth.DataSchema.Models.Certificates;

namespace Freedom.Auth.Dal.Mappers.FromData.Certificates;

internal class UpdateCertificateDataToCertificate : Profile
{
    public UpdateCertificateDataToCertificate()
    {
        CreateMap<UpdateCertificateData, Certificate>()
            .ForMember(r => r.Id, r => r.Ignore());
    }
}
