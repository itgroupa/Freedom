using Freedom.Auth.Business.Models.Users;
using MediatR;

namespace Freedom.Auth.Business.Requests.Users;

public class AddUserRequest : IRequest<UserBusiness>
{
    public AddUserBusiness Model { get; }

    public AddUserRequest(AddUserBusiness model)
    {
        Model = model;
    }
}
