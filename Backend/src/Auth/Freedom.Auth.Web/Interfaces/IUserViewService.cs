using Freedom.Auth.DataSchema.Models;
using Freedom.Auth.Web.Models.Users;
using Microsoft.AspNetCore.Authorization;

namespace Freedom.Auth.Web.Interfaces;

public interface IUserViewService
{
    Task<UserView> AddAsync(AddUserView model, string provider);
    Task<ResponsePaginator<UserView>> GetAsync(int page, int size);
    Task<AuthorizationResult> SignInAsync(SignInView model);
    Task RememberEmailAsync(ForgotEmailView model);
}
