using AutoMapper;
using Freedom.Auth.Business.Models.Certificates;
using Freedom.Auth.Business.Requests.Certificates;
using Freedom.Auth.DataSchema.Interfaces;
using Freedom.Auth.DataSchema.Models;
using MediatR;

namespace Freedom.Auth.Business.Handlers.Certificates;

internal class GetCertificatesPaginatorHandler : IRequestHandler<GetCertificatesPaginatorRequest, ResponsePaginator<CertificateBusiness>>
{
    private readonly IMapper _mapper;
    private readonly ICertificateSchemaService _service;

    public GetCertificatesPaginatorHandler(IMapper mapper, ICertificateSchemaService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public async Task<ResponsePaginator<CertificateBusiness>> Handle(GetCertificatesPaginatorRequest request, CancellationToken cancellationToken)
    {
        var req = new RequestPaginator(request.Page, request.Size);

        var data = await _service.GetAsync(req);

        var result = _mapper.Map<ResponsePaginator<CertificateBusiness>>(data);

        return result;
    }
}
