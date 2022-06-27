using MediatR;

namespace Freedom.Auth.Business.Requests.Certificates;

public class GetCertificateDataByIdRequest : IRequest<byte[]>
{
    public Guid Id { get; }

    public GetCertificateDataByIdRequest(Guid id)
    {
        Id = id;
    }
}
