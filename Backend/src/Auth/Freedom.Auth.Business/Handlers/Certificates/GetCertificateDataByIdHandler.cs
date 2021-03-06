using Freedom.Auth.Business.Requests.Certificates;
using Freedom.Auth.DataSchema.Interfaces;
using MediatR;

namespace Freedom.Auth.Business.Handlers.Certificates;

internal class GetCertificateDataByIdHandler : IRequestHandler<GetCertificateDataByIdRequest, byte[]>
{
    private readonly ICertificateSchemaService _service;

    public GetCertificateDataByIdHandler(ICertificateSchemaService service)
    {
        _service = service;
    }

    public async Task<byte[]> Handle(GetCertificateDataByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.GetDataAsync(request.Id);

        return result;
    }
}
