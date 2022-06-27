using AutoMapper;
using Freedom.Auth.Dal.Models;
using Freedom.Auth.DataSchema.Models.Certificates;

namespace Freedom.Auth.Dal.Mappers.FromData.Certificates;

internal class AddCertificateDataToCertificate : Profile
{
    public AddCertificateDataToCertificate()
    {
        CreateMap<AddCertificateData, Certificate>()
            .ForMember(r => r.Id, r =>
                r.MapFrom(m => Guid.NewGuid()));
    }
}
