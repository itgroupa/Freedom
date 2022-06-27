using Freedom.Auth.Business.Models.Clients;
using MediatR;

namespace Freedom.Auth.Business.Requests.Clients;

public class GetClientByIdRequest : IRequest<ClientBusiness>
{
    public Guid Id { get; }

    public GetClientByIdRequest(Guid id)
    {
        Id = id;
    }
}
