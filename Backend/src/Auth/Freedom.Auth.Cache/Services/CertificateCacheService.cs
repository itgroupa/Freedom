using Freedom.Access.Redis;
using Freedom.Access.Redis.Models;
using Freedom.Auth.DataSchema.Cache;
using Freedom.Auth.DataSchema.Models.Certificates;

namespace Freedom.Auth.Cache.Services;

internal class CertificateCacheService : ICertificateCacheService
{
    private readonly CacheRepository _repository;

    public CertificateCacheService(CacheRepository repository)
    {
        _repository = repository;
    }
    public async Task<CertificateData?> GetAsync(Guid id)
    {
        var result = await _repository.GetAsync<CertificateData>(GetKey(id));

        return result;
    }

    public async Task AddOrUpdateAsync(CertificateData model)
    {
        await _repository.PutOrUpdateAsync(new CacheTemplate<CertificateData>(GetKey(model.Id),
            TimeSpan.FromHours(1),
            model));
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.RemoveAsync(GetKey(id));
    }

    private string GetKey(Guid id) => $"certificate.{id}";
}
