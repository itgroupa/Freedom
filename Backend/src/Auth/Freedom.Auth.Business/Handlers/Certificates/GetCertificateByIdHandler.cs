using AutoMapper;
using Freedom.Auth.Business.Models.Certificates;
using Freedom.Auth.Business.Requests.Certificates;
using Freedom.Auth.DataSchema.Interfaces;
using MediatR;

namespace Freedom.Auth.Business.Handlers.Certificates;

internal class GetCertificateByIdHandler : IRequestHandler<GetCertificateByIdRequest, CertificateBusiness>
{
    private readonly IMapper _mapper;
    private readonly ICertificateSchemaService _service;

    public GetCertificateByIdHandler(IMapper mapper, ICertificateSchemaService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public async Task<CertificateBusiness> Handle(GetCertificateByIdRequest request, CancellationToken cancellationToken)
    {
        var data = await _service.GetAsync(request.Id);

        var result = _mapper.Map<CertificateBusiness>(data);

        return result;
    }
}
