namespace Freedom.Auth.DataSchema.Models;

public class ResponsePaginator<T>
{
    public T[] Items { get; set; } = null!;
    public long Count { get; set; }
}
