namespace Freedom.Auth.Business.Models.Users;

public class AddUserBusiness
{
    public string Provider { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
