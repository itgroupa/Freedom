using Freedom.Auth.Business.Models.Certificates;
using MediatR;

namespace Freedom.Auth.Business.Requests.Certificates;

public class UpdateCertificateRequest : IRequest<CertificateBusiness>
{
    public UpdateCertificateBusiness Model { get; }

    public UpdateCertificateRequest(UpdateCertificateBusiness model)
    {
        Model = model;
    }
}
