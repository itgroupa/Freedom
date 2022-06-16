namespace Freedom.Access.Redis.Models;

public class CacheTemplate<T> where T : class
{
    public string Key { get; set; }
    public TimeSpan Duration { get; set; }
    public T Obj { get; set; }

    public CacheTemplate(string key, TimeSpan duration, T obj)
    {
        Key = key;
        Duration = duration;
        Obj = obj;
    }
}
