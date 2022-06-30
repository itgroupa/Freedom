using AutoMapper;
using Freedom.Auth.Business.Models.Users;
using Freedom.Auth.Business.Requests.Users;
using Freedom.Auth.DataSchema.Models;
using Freedom.Auth.Web.Interfaces;
using Freedom.Auth.Web.Models;
using MediatR;

namespace Freedom.Auth.Web.Services;

internal class UserViewService : IUserViewService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UserViewService(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<UserView> AddAsync(AddUserView model, string provider)
    {
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
