using AutoMapper;
using Freedom.Auth.Business.Models.Users;
using Freedom.Auth.Business.Requests.Users;
using Freedom.Auth.DataSchema.Interfaces;
using MediatR;

namespace Freedom.Auth.Business.Handlers.Users;

internal class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, UserBusiness>
{
    private readonly IUserSchemaService _service;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUserSchemaService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;

    }

    public async Task<UserBusiness> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var data = await _service.GetAsync(request.Id);

        var result = _mapper.Map<UserBusiness>(data);

        return result;
    }
}
