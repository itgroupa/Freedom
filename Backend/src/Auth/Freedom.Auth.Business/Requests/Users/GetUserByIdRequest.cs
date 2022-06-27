using Freedom.Auth.Business.Models.Users;
using MediatR;

namespace Freedom.Auth.Business.Requests.Users;

public class GetUserByIdRequest : IRequest<UserBusiness>
{
    public Guid Id { get; }

    public GetUserByIdRequest(Guid id)
    {
        Id = id;
    }
}
