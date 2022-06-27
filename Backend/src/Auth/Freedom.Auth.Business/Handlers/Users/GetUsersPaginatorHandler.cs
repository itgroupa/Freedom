using AutoMapper;
using Freedom.Auth.Business.Models.Users;
using Freedom.Auth.Business.Requests.Users;
using Freedom.Auth.DataSchema.Interfaces;
using Freedom.Auth.DataSchema.Models;
using MediatR;

namespace Freedom.Auth.Business.Handlers.Users;

internal class GetUsersPaginatorHandler : IRequestHandler<GetUsersPaginatorRequest, ResponsePaginator<UserBusiness>>
{
    private readonly IUserSchemaService _service;
    private readonly IMapper _mapper;

    public GetUsersPaginatorHandler(IUserSchemaService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;

    }

    public async Task<ResponsePaginator<UserBusiness>> Handle(GetUsersPaginatorRequest request, CancellationToken cancellationToken)
    {
        var req = new RequestPaginator(request.Page, request.Size);

        var data = await _service.GetAsync(req);

        var result = _mapper.Map<ResponsePaginator<UserBusiness>>(data);

        return result;
    }
}
