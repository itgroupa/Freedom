using Freedom.Access.Redis;
using Freedom.Access.Redis.Models;
using Freedom.Auth.DataSchema.Auth;
using Freedom.Auth.DataSchema.Models.Users;

namespace Freedom.Auth.Cache.Services;

internal class UserAuthService : IUserAuthService
{
    private readonly CacheRepository _repository;
    private string GetKey(string sessionId, string remoteIp) => $"Auth.{remoteIp}-{sessionId}";

    public UserAuthService(CacheRepository repository)
    {
        _repository = repository;
    }

    public async Task AuthAsync(UserAuthModel model, string sessionId, string remoteIp)
    {
        await _repository.PutOrUpdateAsync(new CacheTemplate<UserAuthModel>(GetKey(sessionId, remoteIp),
            TimeSpan.FromHours(3), model));
    }

    public async Task LogoutAsync(string sessionId, string remoteIp)
    {
        await _repository.RemoveAsync(GetKey(sessionId, remoteIp));
    }

    public async Task<UserAuthModel?> GetAsync(string sessionId, string remoteIp)
    {
        var result = await _repository.GetAsync<UserAuthModel>(GetKey(sessionId, remoteIp));

        return result;
    }
}
