using Freedom.Auth.Business.Models.Clients;
using MediatR;

namespace Freedom.Auth.Business.Requests.Clients;

public class AddClientRequest : IRequest<ClientBusiness>
{
    public AddClientBusiness Model { get; }

    public AddClientRequest(AddClientBusiness model)
    {
        Model = model;
    }
}
