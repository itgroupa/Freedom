using Freedom.Auth.Business.Models.Users;
using Freedom.Auth.DataSchema.Models;
using MediatR;

namespace Freedom.Auth.Business.Requests.Users;

public class GetUsersPaginatorRequest : IRequest<ResponsePaginator<UserBusiness>>
{
    public int Page { get; }
    public int Size { get; }

    public GetUsersPaginatorRequest(int page, int size)
    {
        Page = page;
        Size = size;
    }
}
