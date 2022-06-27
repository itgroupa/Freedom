using Freedom.Auth.Business.Models.Certificates;
using MediatR;

namespace Freedom.Auth.Business.Requests.Certificates;

public class GetCertificateByIdRequest : IRequest<CertificateBusiness>
{
    public Guid Id { get; }

    public GetCertificateByIdRequest(Guid id)
    {
        Id = id;
    }
}
