using AutoMapper;
using Freedom.Access.Mongo;
using Freedom.Auth.Dal.Models;
using Freedom.Auth.DataSchema.Db;
using Freedom.Auth.DataSchema.Models;
using Freedom.Auth.DataSchema.Models.Certificates;

namespace Freedom.Auth.Dal.Services;

internal class CertificateDalService : ICertificateDalService
{
    private readonly IDbRepository _dbRepository;
    private readonly IMapper _mapper;

    public CertificateDalService(IDbRepository dbRepository, IMapper mapper)
    {
        _dbRepository = dbRepository;
        _mapper = mapper;
    }

    public async Task<CertificateData> GetAsync(Guid id)
    {
        var certificate = await _dbRepository.GetOneByExpressionAsync<Certificate>(r => r.Id == id);

        if (certificate == null) throw new NullReferenceException("user is not found");

        var result = _mapper.Map<CertificateData>(certificate);

        return result;
    }

    public async Task<byte[]> GetDataAsync(Guid id)
    {
        var certificate = await _dbRepository.GetOneByExpressionAsync<Certificate>(r => r.Id == id);

        if (certificate == null) throw new NullReferenceException("user is not found");

        return certificate.Data;
    }

    public async Task<ResponsePaginator<CertificateData>> GetAsync(RequestPaginator request)
    {
        var count = await _dbRepository.CountAsync<Certificate>();
        var certificates = await _dbRepository.GetAsync<Certificate>(request.Page, request.Size);

        var result = new ResponsePaginator<CertificateData>
        {
            Count = count,
            Items = _mapper.Map<CertificateData[]>(certificates)
        };

        return result;
    }

    public async Task<CertificateData[]> GetAsync(Guid[] id)
    {
        var certificates = await _dbRepository.GetAllByExpressionAsync<Certificate>(r=> id.Contains(r.Id));

        var result = _mapper.Map<CertificateData[]>(certificates);

        return result;
    }

    public async Task<CertificateData> AddAsync(AddCertificateData model)
    {
        var request = _mapper.Map<Certificate>(model);

        var dbModel = await _dbRepository.AddAsync(request);

        var result = _mapper.Map<CertificateData>(dbModel);

        return result;
    }

    public async Task<CertificateData> UpdateAsync(UpdateCertificateData model)
    {
        var client = await _dbRepository.GetOneByExpressionAsync<Certificate>(r => r.Id == model.Id);

        var updateModel = _mapper.Map(model, client);

        if (updateModel == null) throw new NullReferenceException("mapper is not found");

        await _dbRepository.UpdateOneAsync(updateModel, r => r.Id == model.Id);

        var result = _mapper.Map<CertificateData>(updateModel);

        return result;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _dbRepository.DeleteOneAsync<Certificate>(r => r.Id == id);
    }
}
