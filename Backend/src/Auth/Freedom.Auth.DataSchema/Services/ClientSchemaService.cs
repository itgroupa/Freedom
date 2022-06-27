using Freedom.Auth.DataSchema.Cache;
using Freedom.Auth.DataSchema.Db;
using Freedom.Auth.DataSchema.Interfaces;
using Freedom.Auth.DataSchema.Models;
using Freedom.Auth.DataSchema.Models.Clients;

namespace Freedom.Auth.DataSchema.Services;

internal class ClientSchemaService : IClientSchemaService
{
    private readonly IClientDalService _dalService;
    private readonly IClientCacheService _cacheService;

    public ClientSchemaService(IClientDalService dalService, IClientCacheService cacheService)
    {
        _dalService = dalService;
        _cacheService = cacheService;
    }

    public async Task<ClientData> GetAsync(Guid id, string secret, string redirectUrl)
    {
        var cache = await _cacheService.GetAsync(id);

        if (cache != null && cache.Secret == secret && cache.RedirectUrls.Contains(redirectUrl)) return cache;

        var dal = await _dalService.GetAsync(id, secret, redirectUrl);

        await _cacheService.AddOrUpdateAsync(dal);

        return dal;
    }

    public async Task<ClientData> GetAsync(Guid id)
    {
        var cache = await _cacheService.GetAsync(id);

        if (cache != null) return cache;

        var dal = await _dalService.GetAsync(id);

        await _cacheService.AddOrUpdateAsync(dal);

        return dal;
    }

    public async Task<ResponsePaginator<ClientData>> GetAsync(RequestPaginator request)
    {
        var result = await _dalService.GetAsync(request);

        return result;
    }

    public async Task<ClientData> AddAsync(AddClientData model)
    {
        var dal = await _dalService.AddAsync(model);

        await _cacheService.AddOrUpdateAsync(dal);

        return dal;
    }

    public async Task<ClientData> UpdateAsync(UpdateClientData model)
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
