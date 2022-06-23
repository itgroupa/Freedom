using Freedom.Auth.DataSchema.Models.Users;

namespace Freedom.Auth.DataSchema.Interfaces;

public interface IUserSchemaService
{
    Task<UserData> GetAsync(string email, string password);
    Task<UserData> AddAsync(AddUserData model);
    Task<UserData> UpdateAsync(UpdateUserData model);
    Task DeleteAsync(Guid id);
}
