using Freedom.Auth.Business.Requests.Users;
using Freedom.Auth.DataSchema.Interfaces;
using MediatR;

namespace Freedom.Auth.Business.Handlers.Users;

internal class DeleteUserHandler : IRequestHandler<DeleteUserRequest>
{
    private readonly IUserSchemaService _service;

    public DeleteUserHandler(IUserSchemaService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id);

        return new Unit();
    }
}
