using Freedom.Auth.Business.Models.Users;
using MediatR;

namespace Freedom.Auth.Business.Requests.Users;

public class GetUserByEmailPasswordRequest : IRequest<UserBusiness>
{
    public string Email { get; }
    public string Password { get; }

    public GetUserByEmailPasswordRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
