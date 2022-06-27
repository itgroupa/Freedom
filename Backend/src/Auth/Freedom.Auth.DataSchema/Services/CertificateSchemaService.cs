using Freedom.Auth.DataSchema.Cache;
using Freedom.Auth.DataSchema.Db;
using Freedom.Auth.DataSchema.Interfaces;
using Freedom.Auth.DataSchema.Models;
using Freedom.Auth.DataSchema.Models.Certificates;

namespace Freedom.Auth.DataSchema.Services;

internal class CertificateSchemaService : ICertificateSchemaService
{
    private readonly ICertificateCacheService _cacheService;
    private readonly ICertificateDalService _dalService;

    public CertificateSchemaService(ICertificateCacheService cacheService, ICertificateDalService dalService)
    {
        _cacheService = cacheService;
        _dalService = dalService;
    }
    public async Task<CertificateData> GetAsync(Guid id)
    {
        var cache = await _cacheService.GetAsync(id);

        if (cache != null) return cache;

        var dal = await _dalService.GetAsync(id);

        await _cacheService.AddOrUpdateAsync(dal);

        return dal;
    }

    public async Task<byte[]> GetDataAsync(Guid id)
    {
        var result = await _dalService.GetDataAsync(id);

        return result;
    }

    public async Task<ResponsePaginator<CertificateData>> GetAsync(RequestPaginator request)
    {
        var result = await _dalService.GetAsync(request);

        return result;
    }

    public async Task<CertificateData> AddAsync(AddCertificateData model)
    {
        var dal = await _dalService.AddAsync(model);

        await _cacheService.AddOrUpdateAsync(dal);

        return dal;
    }

    public async Task<CertificateData> UpdateAsync(UpdateCertificateData model)
    {
        var dal = await _dalService.UpdateAsync(model);

        await _cacheService.AddOrUpdateAsync(dal);

        return dal;
    }

    public async Task DeleteAsync(Guid id)
    {
        await Task.WhenAll(_cacheService.DeleteAsync(id), _dalService.DeleteAsync(id));
    }
}
