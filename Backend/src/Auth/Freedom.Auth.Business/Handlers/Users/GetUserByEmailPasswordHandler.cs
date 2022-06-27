using AutoMapper;
using Freedom.Auth.Business.Models.Users;
using Freedom.Auth.Business.Requests.Users;
using Freedom.Auth.DataSchema.Interfaces;
using Freedom.Common.Crypto;
using MediatR;

namespace Freedom.Auth.Business.Handlers.Users;

internal class GetUserByEmailPasswordHandler : IRequestHandler<GetUserByEmailPasswordRequest, UserBusiness>
{
    private readonly IUserSchemaService _service;
    private readonly IMapper _mapper;

    public GetUserByEmailPasswordHandler(IUserSchemaService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<UserBusiness> Handle(GetUserByEmailPasswordRequest request, CancellationToken cancellationToken)
    {
        var data = await _service.GetAsync(request.Email, request.Password.GetHashMd5());

        var result = _mapper.Map<UserBusiness>(data);

        return result;
    }
}
