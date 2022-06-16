using Freedom.Access.Redis.Models;
using Freedom.Common.Json;
using StackExchange.Redis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Freedom.Access.Redis;

internal class RedisRepository : CacheRepository
{
    private readonly ILogger _logger;

    private readonly IConnectionMultiplexer _connection;

    private readonly string _keyPrefix;

    public RedisRepository(IOptions<RedisConfiguration> redisConfiguration, ILogger<RedisRepository> logger)
    {
        var conf = redisConfiguration.Value;
        _logger = logger;

        _keyPrefix = conf.PrefixKey ?? throw new ArgumentNullException(nameof(conf.PrefixKey));

        if (conf.Host == null) throw new ArgumentNullException(nameof(conf.Host));

        var configurationOptions = new ConfigurationOptions
        {
            EndPoints = { { conf.Host, conf.Port } },
            AllowAdmin = false,
            ClientName = conf.ClientName,
            ReconnectRetryPolicy = new LinearRetry(5000),
            AbortOnConnectFail = false,
            Password = conf.Password
        };

        _connection = ConnectionMultiplexer.Connect(configurationOptions);
        _connection.ErrorMessage += _Connection_ErrorMessage;
    }

    public override async Task<bool> PutOrUpdateAsync<T>(CacheTemplate<T> model) where T : class
    {
        try
        {
            var str = JsonConvert.GetJsonObj(model.Obj);
            return await Database.StringSetAsync(GetCacheKey(model.Key), str, model.Duration);
        }
        catch(Exception e)
        {
            _logger.LogError(e, "error into PutOrUpdateAsync");
            return false;
        }
    }

    public override async Task<bool> PutOrUpdateAsync<T>(IEnumerable<CacheTemplate<T>> model) where T : class
    {
        try
        {
            var request = model
                .Select(m => new KeyValuePair<RedisKey, RedisValue>(GetCacheKey(m.Key), JsonConvert.GetJsonObj(m.Obj)))
                .ToArray();
            return await Database.StringSetAsync(request);
        }
        catch(Exception e)
        {
            _logger.LogError(e, "error into PutOrUpdateAsync");
            return false;
        }
    }

    public override async Task<T?> GetAsync<T>(string key) where T : class
    {
        try
        {
            var obj = await Database.StringGetAsync(GetCacheKey(key));
            return !obj.HasValue ? null : JsonConvert.GetObjFromJson<T>(obj);
        }
        catch(Exception e)
        {
            _logger.LogError(e, "error into GetAsync");
            return null;
        }
    }

    public override async Task<IEnumerable<T?>> GetAsync<T>(IEnumerable<string> keys) where T : class
    {
        try
        {
            var values = await Database.StringGetAsync(keys.Select(k => (RedisKey)GetCacheKey(k)).ToArray());
            var results = values.Where(v => v.HasValue).Select(v => JsonConvert.GetObjFromJson<T>(v));

            return results;
        }
        catch(Exception e)
        {
            _logger.LogError(e, "error into GetAsync");
            return ArraySegment<T?>.Empty;
        }
    }

    public override async Task<bool> RemoveAsync(string key)
    {
        try
        {
            return await Database.KeyDeleteAsync(GetCacheKey(key));
        }
        catch(Exception e)
        {
            _logger.LogError(e, "error into RemoveAsync");
            return false;
        }
    }

    public override async Task RemoveAsync(string[] keys)
    {
        try
        {
            await Database.KeyDeleteAsync(keys.Select(r => new RedisKey(GetCacheKey(r))).ToArray());
        }
        catch(Exception e)
        {
            _logger.LogError(e, "error into RemoveAsync");
        }
    }

    public override async Task PublishAsync<T>(string publishChannel, T obj) where T : class
    {
        try
        {
            var str = JsonConvert.GetJsonObj(obj);
            await _connection.GetSubscriber().PublishAsync(publishChannel, str);
        }
        catch(Exception e)
        {
            _logger.LogError(e, "error into PublishAsync");
        }
    }

    public override async Task SubscribeAsync<T>(string publishChannel, Func<T?, Task> act) where T : class
    {
        async void Action(RedisChannel _, RedisValue val)
        {
            try
            {
                var obj = JsonConvert.GetObjFromJson<T>(val);
                await act(obj);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error into executing delegate for publish");
            }
        }

        try
        {
            await _connection.GetSubscriber().SubscribeAsync(publishChannel, Action);
        }
        catch(Exception e)
        {
            _logger.LogCritical(e, "error into SubscribeAsync");
            throw;
        }
    }

    private void _Connection_ErrorMessage(object? sender, RedisErrorEventArgs e)
    {
        _logger.LogError(e.Message, sender);
    }

    private IDatabase Database
    {
        get
        {
            try
            {
                return _connection.GetDatabase();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error into cache");
                throw;
            }
        }
    }
    private string GetCacheKey(string key) => $"{_keyPrefix}-{key}";
}
