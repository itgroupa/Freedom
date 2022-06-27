using Freedom.Auth.Business.Models.Certificates;
using Freedom.Auth.DataSchema.Models;
using MediatR;

namespace Freedom.Auth.Business.Requests.Certificates;

public class GetCertificatesPaginatorRequest : IRequest<ResponsePaginator<CertificateBusiness>>
{
    public int Page { get; }
    public int Size { get; }

    public GetCertificatesPaginatorRequest(int page, int size)
    {
        Page = page;
        Size = size;
    }
}
