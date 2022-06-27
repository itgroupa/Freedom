using AutoMapper;
using Freedom.Auth.Business.Models.Clients;
using Freedom.Auth.Business.Requests.Clients;
using Freedom.Auth.DataSchema.Interfaces;
using Freedom.Auth.DataSchema.Models.Clients;
using MediatR;

namespace Freedom.Auth.Business.Handlers.Clients;

internal class UpdateClientHandler : IRequestHandler<UpdateClientRequest, ClientBusiness>
{
    private readonly IMapper _mapper;
    private readonly IClientSchemaService _service;

    public UpdateClientHandler(IMapper mapper, IClientSchemaService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public async Task<ClientBusiness> Handle(UpdateClientRequest request, CancellationToken cancellationToken)
    {
        var req = _mapper.Map<UpdateClientData>(request.Model);

        var data = await _service.UpdateAsync(req);

        var result = _mapper.Map<ClientBusiness>(data);

        return result;
    }
}
