using Freedom.Auth.DataSchema.Models;
using Freedom.Auth.DataSchema.Models.Users;

namespace Freedom.Auth.DataSchema.Db;

public interface IUserDalService
{
    Task<UserData> GetAsync(string email, string password);
    Task<UserData> GetAsync(Guid id);
    Task<ResponsePaginator<UserData>> GetAsync(RequestPaginator request);
    Task<UserData[]> GetAsync(Guid[] id);
    Task<UserData> AddAsync(AddUserData model);
    Task<UserData> UpdateAsync(UpdateUserData model);
    Task DeleteAsync(Guid id);
}
