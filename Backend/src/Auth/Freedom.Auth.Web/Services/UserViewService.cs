using System.Security.Authentication;
using System.Security.Claims;
using AutoMapper;
using Freedom.Auth.Business.Models.Users;
using Freedom.Auth.Business.Requests.Users;
using Freedom.Auth.DataSchema.Models;
using Freedom.Auth.Web.Const;
using Freedom.Auth.Web.Interfaces;
using Freedom.Auth.Web.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Freedom.Auth.Web.Services;

internal class UserViewService : IUserViewService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ICaptchaVerificationService _verificationService;
    private readonly IAuthorizationService _authorizationService;

    public UserViewService(IMapper mapper, IMediator mediator, ICaptchaVerificationService verificationService,
        IAuthorizationService authorizationService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _verificationService = verificationService;
        _authorizationService = authorizationService;
    }

    public async Task<UserView> AddAsync(AddUserView model, string provider)
    {
        var captchaResult = await _verificationService.IsCaptchaValid(model);

        if (!captchaResult) throw new AuthenticationException("captcha is not valid");

        var request = _mapper.Map<AddUserBusiness>(model);
        request.Provider = provider;

        var business = await _mediator.Send(new AddUserRequest(request));

        var result = _mapper.Map<UserView>(business);

        return result;
    }

    public async Task<ResponsePaginator<UserView>> GetAsync(int page, int size)
    {
        var business = await _mediator.Send(new GetUsersPaginatorRequest(page, size));

        var result = _mapper.Map<ResponsePaginator<UserView>>(business);

        return result;
    }

    public async Task<AuthorizationResult> SignInAsync(SignInView model)
    {
        var captchaResult = await _verificationService.IsCaptchaValid(model);

        if (!captchaResult) throw new AuthenticationException("captcha is not valid");

        var business = await _mediator.Send(new GetUserByEmailPasswordRequest(model.Email, model.Password));

        var viewModel = _mapper.Map<UserView>(business);

        var result = await _authorizationService.AuthorizeAsync(new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new(ClaimTypes.Role, viewModel.Role),
            new(ClaimTypes.Email, viewModel.Email),
            new(ClaimTypes.NameIdentifier, viewModel.Id.ToString()),
            new(ClaimTypes.Name, viewModel.Name)
        })), Policies.AuthPolicy);

        return result;
    }

    public async Task RememberEmailAsync(ForgotEmailView model)
    {
        var captchaResult = await _verificationService.IsCaptchaValid(model);

        if (!captchaResult) throw new AuthenticationException("captcha is not valid");

        await Task.CompletedTask;
    }
}
