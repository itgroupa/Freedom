using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Freedom.Auth.Dal.Models;

public class Client
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    public string[] RedirectUrls { get; set; } = null!;
    public string Secret { get; set; } = null!;
}
