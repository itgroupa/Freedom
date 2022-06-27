using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Freedom.Auth.Dal.Models;

public class Certificate
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid Id { get; set; }

    public byte[] Data { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
}
