using AutoMapper;
using Freedom.Auth.Business.Models.Clients;
using Freedom.Auth.Business.Requests.Clients;
using Freedom.Auth.DataSchema.Interfaces;
using MediatR;

namespace Freedom.Auth.Business.Handlers.Clients;

internal class GetClientByIdHandler : IRequestHandler<GetClientByIdRequest, ClientBusiness>
{
    private readonly IMapper _mapper;
    private readonly IClientSchemaService _service;

    public GetClientByIdHandler(IMapper mapper, IClientSchemaService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public async Task<ClientBusiness> Handle(GetClientByIdRequest request, CancellationToken cancellationToken)
    {
        var data = await _service.GetAsync(request.Id);

        var result = _mapper.Map<ClientBusiness>(data);

        return result;
    }
}
