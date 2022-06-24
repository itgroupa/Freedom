namespace Freedom.Auth.DataSchema.Models.Users;

public class AddUserData
{
    public string Provider { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
