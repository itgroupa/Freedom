using Freedom.Auth.Web.Interfaces;

namespace Freedom.Auth.Web.Services;

internal class SessionService : ISessionService
{
    private const string CookieKey = "SESSION_ID";
    private readonly IHttpContextAccessor _context;

    public SessionService(IHttpContextAccessor context)
    {
        _context = context;
    }

    public string GetSessionId()
    {
        if (_context.HttpContext == null) return string.Empty;

        if (_context.HttpContext.Request.Cookies.TryGetValue(CookieKey, out var value)
            && !string.IsNullOrEmpty(value))
        {
            return value;
        }

        var newSessionKey = Guid.NewGuid().ToString();

        _context.HttpContext.Response.Cookies.Append(CookieKey, newSessionKey);

        return newSessionKey;
    }

    public string? GetRemoteIp()
    {
        if (_context.HttpContext == null) return string.Empty;

        return _context.HttpContext.Connection.RemoteIpAddress?.ToString();
    }
}
