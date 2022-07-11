using Freedom.Auth.DataSchema.Models.Users;

namespace Freedom.Auth.DataSchema.Auth;

public interface IUserAuthService
{
    Task AuthAsync(UserAuthModel model, string sessionId, string remoteIp);
    Task LogoutAsync(string sessionId, string remoteIp);
    Task<UserAuthModel?> GetAsync(string sessionId, string remoteIp);
}
