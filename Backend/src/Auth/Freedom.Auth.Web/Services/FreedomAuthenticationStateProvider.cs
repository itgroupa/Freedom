using System.Security.Claims;
using Freedom.Auth.DataSchema.Auth;
using Freedom.Auth.Web.Const;
using Freedom.Auth.Web.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace Freedom.Auth.Web.Services;

internal class FreedomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IUserAuthService _authService;
    private readonly ISessionService _sessionService;


    public FreedomAuthenticationStateProvider(IUserAuthService authService, ISessionService sessionService)
    {
        _authService = authService;
        _sessionService = sessionService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var sessionId = _sessionService.GetSessionId();
        var remoteIp = _sessionService.GetRemoteIp();

        if (string.IsNullOrEmpty(sessionId) || string.IsNullOrEmpty(remoteIp))
            return new AuthenticationState(new ClaimsPrincipal());

        var userResult = await _authService.GetAsync(sessionId, remoteIp);

        if (userResult == null) return new AuthenticationState(new ClaimsPrincipal());

        var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new(ClaimTypes.Role, userResult.Role),
            new(ClaimTypes.Email, userResult.Email),
            new(ClaimTypes.NameIdentifier, userResult.Id.ToString()),
            new(ClaimTypes.Name, userResult.Name),
        }, Policies.AuthPolicy));

        return new AuthenticationState(claims);
    }
}
