using Freedom.Auth.Business.Requests.Clients;
using Freedom.Auth.DataSchema.Interfaces;
using MediatR;

namespace Freedom.Auth.Business.Handlers.Clients;

internal class DeleteClientHandler : IRequestHandler<DeleteClientRequest>
{
    private readonly IClientSchemaService _service;

    public DeleteClientHandler(IClientSchemaService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(DeleteClientRequest request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id);

        return new Unit();
    }
}
