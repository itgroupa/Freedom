using Freedom.Access.Redis.Models;

namespace Freedom.Access.Redis;

public abstract class CacheRepository
{
    public abstract Task<bool> PutOrUpdateAsync<T>(CacheTemplate<T> model) where T : class;
    public abstract Task<bool> PutOrUpdateAsync<T>(IEnumerable<CacheTemplate<T>> model) where T : class;
    public abstract Task<T?> GetAsync<T>(string key) where T : class;
    public abstract Task<IEnumerable<T?>> GetAsync<T>(IEnumerable<string> keys) where T : class;
    public abstract Task<bool> RemoveAsync(string key);
    public abstract Task RemoveAsync(string[] keys);

    public virtual Task PublishAsync<T>(string publishChannel, T obj) where T : class =>
        Task.CompletedTask;

    public virtual Task SubscribeAsync<T>(string publishChannel, Func<T?, Task> act) where T : class =>
        Task.CompletedTask;
}
