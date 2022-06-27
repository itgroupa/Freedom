using AutoMapper;
using Freedom.Auth.Business.Models.Clients;
using Freedom.Auth.Business.Requests.Clients;
using Freedom.Auth.DataSchema.Interfaces;
using Freedom.Auth.DataSchema.Models;
using MediatR;

namespace Freedom.Auth.Business.Handlers.Clients;

internal class GetClientsPaginatorHandler : IRequestHandler<GetClientsPaginatorRequest, ResponsePaginator<ClientBusiness>>
{
    private readonly IMapper _mapper;
    private readonly IClientSchemaService _service;

    public GetClientsPaginatorHandler(IMapper mapper, IClientSchemaService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public async Task<ResponsePaginator<ClientBusiness>> Handle(GetClientsPaginatorRequest request, CancellationToken cancellationToken)
    {
        var req = new RequestPaginator(request.Page, request.Size);

        var data = await _service.GetAsync(req);

        var result = _mapper.Map<ResponsePaginator<ClientBusiness>>(data);

        return result;
    }
}
