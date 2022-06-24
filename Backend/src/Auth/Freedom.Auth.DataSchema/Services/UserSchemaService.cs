using Freedom.Auth.DataSchema.Cache;
using Freedom.Auth.DataSchema.Db;
using Freedom.Auth.DataSchema.Interfaces;
using Freedom.Auth.DataSchema.Models.Users;

namespace Freedom.Auth.DataSchema.Services;

internal class UserSchemaService : IUserSchemaService
{
    private readonly IUserDalService _userDalService;
    private readonly IUserCacheService _userCacheService;

    public UserSchemaService(IUserDalService userDalService, IUserCacheService userCacheService)
    {
        _userDalService = userDalService;
        _userCacheService = userCacheService;
    }

    public async Task<UserData> GetAsync(string email, string password)
    {
        var dalId = await _userDalService.GetAsync(email, password);

        var dal = await _userDalService.GetAsync(dalId);

        await _userCacheService.AddOrUpdateAsync(dal);

        return dal;
    }

    public async Task<UserData> AddAsync(AddUserData model)
    {
        var dal = await _userDalService.AddAsync(model);

        await _userCacheService.AddOrUpdateAsync(dal);

        return dal;
    }

    public async Task<UserData> UpdateAsync(UpdateUserData model)
    {
        var dal = await _userDalService.UpdateAsync(model);

        await _userCacheService.AddOrUpdateAsync(dal);

        return dal;
    }

    public async Task DeleteAsync(Guid id)
    {
        await Task.WhenAll(_userCacheService.DeleteAsync(id), _userDalService.DeleteAsync(id));
    }
}
