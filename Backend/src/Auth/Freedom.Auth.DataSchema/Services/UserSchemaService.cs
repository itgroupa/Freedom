using Freedom.Auth.DataSchema.Cache;
using Freedom.Auth.DataSchema.Db;
using Freedom.Auth.DataSchema.Interfaces;
using Freedom.Auth.DataSchema.Models;
using Freedom.Auth.DataSchema.Models.Users;

namespace Freedom.Auth.DataSchema.Services;

internal class UserSchemaService : IUserSchemaService
{
    private readonly IUserDalService _dalService;
    private readonly IUserCacheService _cacheService;

    public UserSchemaService(IUserDalService dalService, IUserCacheService cacheService)
    {
        _dalService = dalService;
        _cacheService = cacheService;
    }

    public async Task<UserData> GetAsync(string email, string password)
    {
        var dal = await _dalService.GetAsync(email, password);

        await _cacheService.AddOrUpdateAsync(dal);

        return dal;
    }

    public async Task<UserData> GetAsync(Guid id)
    {
        var cache = await _cacheService.GetAsync(id);

        if (cache != null) return cache;

        var dal = await _dalService.GetAsync(id);

        await _cacheService.AddOrUpdateAsync(dal);

        return dal;
    }

    public async Task<ResponsePaginator<UserData>> GetAsync(RequestPaginator request)
    {
        var result = await _dalService.GetAsync(request);

        return result;
    }

    public async Task<UserData> AddAsync(AddUserData model)
    {
        var dal = await _dalService.AddAsync(model);

        await _cacheService.AddOrUpdateAsync(dal);

        return dal;
    }

    public async Task<UserData> UpdateAsync(UpdateUserData model)
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
