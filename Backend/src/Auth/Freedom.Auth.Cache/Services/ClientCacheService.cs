using Freedom.Access.Redis;
using Freedom.Access.Redis.Models;
using Freedom.Auth.DataSchema.Cache;
using Freedom.Auth.DataSchema.Models.Clients;

namespace Freedom.Auth.Cache.Services;

internal class ClientCacheService : IClientCacheService
{
    private readonly CacheRepository _repository;

    public ClientCacheService(CacheRepository repository)
    {
        _repository = repository;
    }
    public async Task<ClientData?> GetAsync(Guid id)
    {
        var result = await _repository.GetAsync<ClientData>(GetKey(id));

        return result;
    }

    public async Task AddOrUpdateAsync(ClientData model)
    {
        await _repository.PutOrUpdateAsync(new CacheTemplate<ClientData>(GetKey(model.Id),
            TimeSpan.FromHours(1),
            model));
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.RemoveAsync(GetKey(id));
    }

    private string GetKey(Guid id) => $"client.{id}";
}
