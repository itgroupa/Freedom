using Freedom.Auth.Business.Models.Clients;
using MediatR;

namespace Freedom.Auth.Business.Requests.Clients;

public class UpdateClientRequest : IRequest<ClientBusiness>
{
    public UpdateClientBusiness Model { get; }

    public UpdateClientRequest(UpdateClientBusiness model)
    {
        Model = model;
    }
}
