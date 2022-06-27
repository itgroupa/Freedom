using Freedom.Auth.Business.Models.Clients;
using Freedom.Auth.DataSchema.Models;
using MediatR;

namespace Freedom.Auth.Business.Requests.Clients;

public class GetClientsPaginatorRequest : IRequest<ResponsePaginator<ClientBusiness>>
{
    public int Page { get; }
    public int Size { get; }

    public GetClientsPaginatorRequest(int page, int size)
    {
        Page = page;
        Size = size;
    }
}
