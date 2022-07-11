namespace Freedom.Auth.Web.Interfaces;

public interface ISessionService
{
    string GetSessionId();
    string? GetRemoteIp();
}
