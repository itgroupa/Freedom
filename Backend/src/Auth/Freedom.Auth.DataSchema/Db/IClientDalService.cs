using Freedom.Auth.DataSchema.Models;
using Freedom.Auth.DataSchema.Models.Clients;

namespace Freedom.Auth.DataSchema.Db;

public interface IClientDalService
{
    Task<ClientData> GetAsync(Guid id, string secret, string redirectUrl);
    Task<ClientData> GetAsync(Guid id);
    Task<ResponsePaginator<ClientData>> GetAsync(RequestPaginator request);
    Task<ClientData[]> GetAsync(Guid[] id);
    Task<ClientData> AddAsync(AddClientData model);
    Task<ClientData> UpdateAsync(UpdateClientData model);
    Task DeleteAsync(Guid id);
}
