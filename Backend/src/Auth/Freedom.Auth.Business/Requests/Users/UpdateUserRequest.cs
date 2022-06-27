using Freedom.Auth.Business.Models.Users;
using MediatR;

namespace Freedom.Auth.Business.Requests.Users;

public class UpdateUserRequest : IRequest<UserBusiness>
{
    public UpdateUserBusiness Model { get; }

    public UpdateUserRequest(UpdateUserBusiness model)
    {
        Model = model;
    }
}
