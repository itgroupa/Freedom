using MongoDB.Bson.Serialization.Attributes;

namespace Freedom.Auth.Dal.Models;

public class Client
{
    [BsonId]
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    public string[] RedirectUrls { get; set; } = null!;
    public string Secret { get; set; } = null!;
}
