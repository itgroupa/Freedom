using System.Security.Authentication;
using AutoMapper;
using Freedom.Auth.Business.Models.Users;
using Freedom.Auth.Business.Requests.Users;
using Freedom.Auth.DataSchema.Models;
using Freedom.Auth.Web.Interfaces;
using Freedom.Auth.Web.Models.Users;
using MediatR;

namespace Freedom.Auth.Web.Services;

internal class UserViewService : IUserViewService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ICaptchaVerificationService _verificationService;

    public UserViewService(IMapper mapper, IMediator mediator, ICaptchaVerificationService verificationService)
    {
        _mapper = mapper;
        _mediator = mediator;
        _verificationService = verificationService;
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
}
