using AutoMapper;
using Freedom.Auth.Business.Models.Certificates;
using Freedom.Auth.Business.Requests.Certificates;
using Freedom.Auth.DataSchema.Interfaces;
using Freedom.Auth.DataSchema.Models.Certificates;
using Freedom.Common.Crypto;
using MediatR;

namespace Freedom.Auth.Business.Handlers.Certificates;

internal class AddCertificateHandler : IRequestHandler<AddCertificateRequest, CertificateBusiness>
{
    private readonly IMapper _mapper;
    private readonly ICertificateSchemaService _service;

    public AddCertificateHandler(IMapper mapper, ICertificateSchemaService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public async Task<CertificateBusiness> Handle(AddCertificateRequest request, CancellationToken cancellationToken)
    {
        var req = _mapper.Map<AddCertificateData>(request.Model);

        var future = DateTime.UtcNow.AddYears(1);

        var selfSignedCertificate = CertificateUtils.GenerateSelfSignedCertificate(req.Url, future);

        var certificate = CertificateUtils.GetCertificate(selfSignedCertificate, req.Url, future);

        var data = CertificateUtils.ExportCertificate(certificate.ClientCertificate, request.Model.Password);

        req.Data = data;

        var dataResult = await _service.AddAsync(req);

        var result = _mapper.Map<CertificateBusiness>(dataResult);

        return result;
    }
}
