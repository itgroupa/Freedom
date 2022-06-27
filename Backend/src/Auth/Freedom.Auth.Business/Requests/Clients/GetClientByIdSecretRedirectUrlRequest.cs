using Freedom.Auth.Business.Models.Clients;
using MediatR;

namespace Freedom.Auth.Business.Requests.Clients;

public class GetClientByIdSecretRedirectUrlRequest : IRequest<ClientBusiness>
{
    public Guid Id { get; }
    public string Secret { get; }
    public string RedirectUrl { get; }

    public GetClientByIdSecretRedirectUrlRequest(Guid id, string secret, string redirectUrl)
    {
        Id = id;
        Secret = secret;
        RedirectUrl = redirectUrl;
    }
}
