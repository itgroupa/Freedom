using AutoMapper;
using Freedom.Auth.Business.Requests.Certificates;
using Freedom.Auth.DataSchema.Interfaces;
using MediatR;

namespace Freedom.Auth.Business.Handlers.Certificates;

internal class DeleteCertificateHandler : IRequestHandler<DeleteCertificateRequest>
{
    private readonly IMapper _mapper;
    private readonly ICertificateSchemaService _service;

    public DeleteCertificateHandler(IMapper mapper, ICertificateSchemaService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public async Task<Unit> Handle(DeleteCertificateRequest request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request.Id);

        return new Unit();
    }
}
