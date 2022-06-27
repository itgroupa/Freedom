using MediatR;

namespace Freedom.Auth.Business.Requests.Certificates;

public class DeleteCertificateRequest : IRequest
{
    public Guid Id { get; }

    public DeleteCertificateRequest(Guid id)
    {
        Id = id;
    }
}
