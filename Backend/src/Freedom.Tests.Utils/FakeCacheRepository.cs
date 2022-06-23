using Freedom.Access.Redis;
using Freedom.Access.Redis.Models;
using Freedom.Common.Json;

namespace Freedom.Tests.Utils;

public class FakeCacheRepository : CacheRepository
{
    private readonly Dictionary<string, string> _cache;

    public FakeCacheRepository()
    {
        _cache = new Dictionary<string, string>();
    }

    public override async Task<bool> PutOrUpdateAsync<T>(CacheTemplate<T> model) where T : class
    {
        var str = JsonConvert.GetJsonObj(model.Obj);
        if (_cache.ContainsKey(model.Key))
        {
            _cache.Remove(model.Key);
        }
        _cache.TryAdd(model.Key, str);
        return await Task.FromResult(true);
    }

    public override async Task<bool> PutOrUpdateAsync<T>(IEnumerable<CacheTemplate<T>> model) where T : class
    {
        foreach (var item in model)
        {
            await PutOrUpdateAsync(item);
        }

        return true;
    }

    public override async Task<T?> GetAsync<T>(string key) where T : class
    {
        if (_cache.TryGetValue(key, out var str))
        {
            return JsonConvert.GetObjFromJson<T>(str);
        }

        return await Task.FromResult(default(T));
    }

    public override async Task<IEnumerable<T?>> GetAsync<T>(IEnumerable<string> keys) where T : class
    {
        return await Task.FromResult(keys.Select(async k =>
            {
                var result = await GetAsync<T>(k);

                return result;
            })
            .Select(r => r.Result)
            .Where(r => r != default));
    }

    public override async Task<bool> RemoveAsync(string key)
    {
        return await  Task.FromResult(_cache.Remove(key));
    }

    public override async Task RemoveAsync(string[] keys)
    {
        foreach (var key in keys)
        {
            await  Task.FromResult(_cache.Remove(key));
        }
    }
}
