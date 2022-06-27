using AutoMapper;
using Freedom.Access.Mongo;
using Freedom.Auth.Dal.Models;
using Freedom.Auth.DataSchema.Db;
using Freedom.Auth.DataSchema.Models;
using Freedom.Auth.DataSchema.Models.Clients;

namespace Freedom.Auth.Dal.Services;

internal class ClientDalService : IClientDalService
{
    private readonly IDbRepository _dbRepository;
    private readonly IMapper _mapper;

    public ClientDalService(IDbRepository dbRepository, IMapper mapper)
    {
        _dbRepository = dbRepository;
        _mapper = mapper;
    }

    public async Task<ClientData> GetAsync(Guid id, string secret, string redirectUrl)
    {
        var client = await _dbRepository.GetOneByExpressionAsync<Client>(r =>
            r.Id == id && r.Secret == secret && r.RedirectUrls.Contains(redirectUrl));

        if (client == null) throw new NullReferenceException("user is not found");

        var result = _mapper.Map<ClientData>(client);

        return result;
    }

    public async Task<ClientData> GetAsync(Guid id)
    {
        var client = await _dbRepository.GetOneByExpressionAsync<Client>(r => r.Id == id);

        if (client == null) throw new NullReferenceException("user is not found");

        var result = _mapper.Map<ClientData>(client);

        return result;
    }

    public async Task<ResponsePaginator<ClientData>> GetAsync(RequestPaginator request)
    {
        var count = await _dbRepository.CountAsync<Client>();
        var clients = await _dbRepository.GetAsync<Client>(request.Page, request.Size);

        var result = new ResponsePaginator<ClientData>
        {
            Count = count,
            Items = _mapper.Map<ClientData[]>(clients)
        };

        return result;
    }

    public async Task<ClientData[]> GetAsync(Guid[] id)
    {
        var clients = await _dbRepository.GetAllByExpressionAsync<Client>(r=> id.Contains(r.Id));

        var result = _mapper.Map<ClientData[]>(clients);

        return result;
    }

    public async Task<ClientData> AddAsync(AddClientData model)
    {
        var request = _mapper.Map<Client>(model);

        var dbModel = await _dbRepository.AddAsync(request);

        var result = _mapper.Map<ClientData>(dbModel);

        return result;
    }

    public async Task<ClientData> UpdateAsync(UpdateClientData model)
    {
        var client = await _dbRepository.GetOneByExpressionAsync<Client>(r => r.Id == model.Id);

        var updateModel = _mapper.Map(model, client);

        if (updateModel == null) throw new NullReferenceException("mapper is not found");

        await _dbRepository.UpdateOneAsync(updateModel, r => r.Id == model.Id);

        var result = _mapper.Map<ClientData>(updateModel);

        return result;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _dbRepository.DeleteOneAsync<Client>(r => r.Id == id);
    }
}
