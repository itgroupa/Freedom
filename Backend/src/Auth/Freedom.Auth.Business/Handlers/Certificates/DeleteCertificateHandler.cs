using Freedom.Auth.Business.Requests.Certificates;
using Freedom.Auth.DataSchema.Interfaces;
using MediatR;

namespace Freedom.Auth.Business.Handlers.Certificates;

internal class DeleteCertificateHandler : IRequestHandler<DeleteCertificateRequest>
{
    private readonly ICertificateSchemaService _service;

    public DeleteCertificateHandler(ICertificateSchemaService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(DeleteCertificateRequest request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id);

        return new Unit();
    }
}
