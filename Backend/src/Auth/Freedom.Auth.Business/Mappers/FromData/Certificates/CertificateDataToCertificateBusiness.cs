using AutoMapper;
using Freedom.Auth.Business.Models.Certificates;
using Freedom.Auth.DataSchema.Models.Certificates;

namespace Freedom.Auth.Business.Mappers.FromData.Certificates;

internal class CertificateDataToCertificateBusiness : Profile
{
    public CertificateDataToCertificateBusiness()
    {
        CreateMap<CertificateData, CertificateBusiness>();
    }
}
