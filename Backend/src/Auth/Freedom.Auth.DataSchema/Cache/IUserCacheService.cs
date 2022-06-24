using Freedom.Auth.DataSchema.Models.Users;

namespace Freedom.Auth.DataSchema.Cache;

public interface IUserCacheService
{
    Task<UserData?> GetAsync(Guid id);
    Task AddOrUpdateAsync(UserData model);
    Task DeleteAsync(Guid id);
}
