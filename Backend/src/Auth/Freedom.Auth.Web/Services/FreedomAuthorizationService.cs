using System.Security.Claims;
using Freedom.Auth.DataSchema.Auth;
using Freedom.Auth.DataSchema.Models.Users;
using Freedom.Auth.Web.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Freedom.Auth.Web.Services;

internal class FreedomAuthorizationService : IAuthorizationService
{
    private readonly IUserAuthService _authService;
    private readonly ISessionService _sessionService;

    public FreedomAuthorizationService(IUserAuthService authService, ISessionService sessionService)
    {
        _authService = authService;
        _sessionService = sessionService;
    }

    public async Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object? resource,
        IEnumerable<IAuthorizationRequirement> requirements)
    {
        return await GetResult(user);
    }

    public async Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object? resource, string policyName)
    {
        return await GetResult(user);
    }

    private async Task<AuthorizationResult> GetResult(ClaimsPrincipal user)
    {
        var sessionId = _sessionService.GetSessionId();
        var remoteIp = _sessionService.GetRemoteIp();

        if (string.IsNullOrEmpty(sessionId) || string.IsNullOrEmpty(remoteIp))
            return AuthorizationResult.Failed();

        var userData = Convert(user);

        if (userData == null) return AuthorizationResult.Failed();

        await _authService.AuthAsync(userData, sessionId, remoteIp);

        return AuthorizationResult.Success();
    }

    private static UserAuthModel? Convert(ClaimsPrincipal user)
    {
        var userIdString = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdString?.Value)) return null;

        var result = new UserAuthModel
        {
            Id = Guid.Parse(userIdString.Value),
            Email = user.Claims.First(claim => claim.Type == ClaimTypes.Email).Value,
            Name = user.Claims.First(claim => claim.Type == ClaimTypes.Name).Value,
            Role = user.Claims.First(claim => claim.Type == ClaimTypes.Role).Value
        };

        return result;
    }
}
