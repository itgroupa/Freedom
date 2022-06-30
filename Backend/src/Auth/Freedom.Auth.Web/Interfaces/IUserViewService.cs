using Freedom.Auth.Web.Models;

namespace Freedom.Auth.Web.Interfaces;

public interface IUserViewService
{
    Task<UserView> AddAsync(AddUserView model, string provider);
}
