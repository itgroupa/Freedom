using Freedom.Access.Redis;
using Freedom.Access.Redis.Models;
using Freedom.Auth.DataSchema.Cache;
using Freedom.Auth.DataSchema.Models.Users;

namespace Freedom.Auth.Cache.Services;

internal class UserCacheService : IUserCacheService
{
    private readonly CacheRepository _repository;

    public UserCacheService(CacheRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserData?> GetAsync(Guid id)
    {
        var result = await _repository.GetAsync<UserData>(GetKey(id));

        return result;
    }

    public async Task AddOrUpdateAsync(UserData model)
    {
        await _repository.PutOrUpdateAsync(new CacheTemplate<UserData>(GetKey(model.Id),
            TimeSpan.FromHours(1),
            model));
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.RemoveAsync(GetKey(id));
    }

    private string GetKey(Guid id) => $"user.{id}";
}
