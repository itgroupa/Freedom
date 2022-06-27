using AutoMapper;
using Freedom.Auth.Dal.Models;
using Freedom.Auth.DataSchema.Models.Certificates;

namespace Freedom.Auth.Dal.Mappers.ToData.Certificates;

internal class CertificateToCertificateData : Profile
{
    public CertificateToCertificateData()
    {
        CreateMap<Certificate, CertificateData>();
    }
}
