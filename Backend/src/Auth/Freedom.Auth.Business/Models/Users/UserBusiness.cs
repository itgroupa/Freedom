namespace Freedom.Auth.Business.Models.Users;

public class UserBusiness
{
    public Guid Id { get; set; }
    public string Provider { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
}
