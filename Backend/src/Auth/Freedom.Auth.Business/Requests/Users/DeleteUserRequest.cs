using MediatR;

namespace Freedom.Auth.Business.Requests.Users;

public class DeleteUserRequest : IRequest
{
    public Guid Id { get; }

    public DeleteUserRequest(Guid id)
    {
        Id = id;
    }
}
