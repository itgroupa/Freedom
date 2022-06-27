namespace Freedom.Auth.DataSchema.Models;

public class RequestPaginator
{
    public int Page { get; set; }
    public int Size { get; set; }

    public RequestPaginator()
    {
    }

    public RequestPaginator(int page, int size) => (page, size) = (Page, Size);
}
