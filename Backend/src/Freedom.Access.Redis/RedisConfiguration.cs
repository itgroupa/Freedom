namespace Freedom.Access.Redis;

public class RedisConfiguration
{
    public const string Redis = "Redis";
    public string? ClientName { get; set; }
    public string? PrefixKey { get; set; }
    public string? Host { get; set; }
    public int Port { get; set; }
    public string? Password { get; set; }
}
