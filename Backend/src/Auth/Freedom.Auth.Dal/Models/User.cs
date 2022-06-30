using MongoDB.Bson.Serialization.Attributes;

namespace Freedom.Auth.Dal.Models;

public class User
{
    [BsonId]
    public Guid Id { get; set; }

    public string Provider { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = null!;
}
