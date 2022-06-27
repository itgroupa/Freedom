using Freedom.Auth.Business.Models.Certificates;
using MediatR;

namespace Freedom.Auth.Business.Requests.Certificates;

public class AddCertificateRequest : IRequest<CertificateBusiness>
{
    public AddCertificateBusiness Model { get; }

    public AddCertificateRequest(AddCertificateBusiness model)
    {
        Model = model;
    }
}
