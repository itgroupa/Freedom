using AutoMapper;
using Freedom.Auth.Business.Models.Users;
using Freedom.Auth.Business.Requests.Users;
using Freedom.Auth.DataSchema.Interfaces;
using Freedom.Auth.DataSchema.Models.Users;
using MediatR;

namespace Freedom.Auth.Business.Handlers.Users;

internal class AddUserHandler : IRequestHandler<AddUserRequest, UserBusiness>
{
    private readonly IUserSchemaService _service;
    private readonly IMapper _mapper;

    public AddUserHandler(IUserSchemaService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<UserBusiness> Handle(AddUserRequest request, CancellationToken cancellationToken)
    {
        var req = _mapper.Map<AddUserData>(request.Model);

        var data = await _service.AddAsync(req);

        var result = _mapper.Map<UserBusiness>(data);

        return result;
    }
}
