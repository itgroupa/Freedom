namespace Freedom.Auth.Web.Models.Users;

public class UserView
{
    public Guid Id { get; set; }
    public string Provider { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
}
