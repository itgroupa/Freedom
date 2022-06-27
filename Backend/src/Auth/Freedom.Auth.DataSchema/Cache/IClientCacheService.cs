using Freedom.Auth.DataSchema.Models.Clients;

namespace Freedom.Auth.DataSchema.Cache;

public interface IClientCacheService
{
    Task<ClientData?> GetAsync(Guid id);
    Task AddOrUpdateAsync(ClientData model);
    Task DeleteAsync(Guid id);
}
