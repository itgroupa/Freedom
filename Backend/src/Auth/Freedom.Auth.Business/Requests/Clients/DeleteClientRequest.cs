using MediatR;

namespace Freedom.Auth.Business.Requests.Clients;

public class DeleteClientRequest : IRequest
{
    public Guid Id { get; }

    public DeleteClientRequest(Guid id)
    {
        Id = id;
    }
}
