namespace Freedom.Access.Redis;

public class RedisConfiguration
{
    public const string Redis = "Redis";
    public string ClientName { get; set; } = null!;
    public string PrefixKey { get; set; } = null!;
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string Password { get; set; } = null!;
}
