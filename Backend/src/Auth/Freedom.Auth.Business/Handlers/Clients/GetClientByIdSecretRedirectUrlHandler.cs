using AutoMapper;
using Freedom.Auth.Business.Models.Clients;
using Freedom.Auth.Business.Requests.Clients;
using Freedom.Auth.DataSchema.Interfaces;
using MediatR;

namespace Freedom.Auth.Business.Handlers.Clients;

internal class GetClientByIdSecretRedirectUrlHandler : IRequestHandler<GetClientByIdSecretRedirectUrlRequest,
    ClientBusiness>
{
    private readonly IMapper _mapper;
    private readonly IClientSchemaService _service;

    public GetClientByIdSecretRedirectUrlHandler(IMapper mapper, IClientSchemaService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public async Task<ClientBusiness> Handle(GetClientByIdSecretRedirectUrlRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _service.GetAsync(request.Id, request.Secret, request.RedirectUrl);

        var result = _mapper.Map<ClientBusiness>(data);

        return result;
    }
}
