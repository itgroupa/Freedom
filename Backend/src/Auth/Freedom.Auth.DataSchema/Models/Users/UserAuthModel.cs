namespace Freedom.Auth.DataSchema.Models.Users;

public class UserAuthModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
}
