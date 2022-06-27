using Freedom.Auth.DataSchema.Models.Certificates;

namespace Freedom.Auth.DataSchema.Cache;

public interface ICertificateCacheService
{
    Task<CertificateData?> GetAsync(Guid id);
    Task AddOrUpdateAsync(CertificateData model);
    Task DeleteAsync(Guid id);
}
