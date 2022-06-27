using AutoMapper;
using Freedom.Access.Mongo;
using Freedom.Auth.Dal.Models;
using Freedom.Auth.DataSchema.Consts;
using Freedom.Auth.DataSchema.Db;
using Freedom.Auth.DataSchema.Models;
using Freedom.Auth.DataSchema.Models.Users;

namespace Freedom.Auth.Dal.Services;

internal class UserDalService : IUserDalService
{
    private readonly IDbRepository _dbRepository;
    private readonly IMapper _mapper;

    public UserDalService(IDbRepository dbRepository, IMapper mapper)
    {
        _dbRepository = dbRepository;
        _mapper = mapper;
    }
    public async Task<UserData> GetAsync(string email, string password)
    {
        var user = await _dbRepository.GetOneByExpressionAsync<User>(r => r.Email == email && r.Password == password);

        if (user == null) throw new NullReferenceException("user is not found");

        var result = _mapper.Map<UserData>(user);

        return result;
    }

    public async Task<UserData> GetAsync(Guid id)
    {
        var user = await _dbRepository.GetOneByExpressionAsync<User>(r => r.Id == id);

        if (user == null) throw new NullReferenceException("user is not found");

        var result = _mapper.Map<UserData>(user);

        return result;
    }

    public async Task<ResponsePaginator<UserData>> GetAsync(RequestPaginator request)
    {
        var count = await _dbRepository.CountAsync<User>();
        var users = await _dbRepository.GetAsync<User>(request.Page, request.Size);

        var result = new ResponsePaginator<UserData>
        {
            Count = count,
            Items = _mapper.Map<UserData[]>(users)
        };

        return result;
    }

    public async Task<UserData[]> GetAsync(Guid[] id)
    {
        var users = await _dbRepository.GetAllByExpressionAsync<User>(r=> id.Contains(r.Id));

        var result = _mapper.Map<UserData[]>(users);

        return result;
    }

    public async Task<UserData> AddAsync(AddUserData model)
    {
        var countUser = await _dbRepository.CountAsync<User>();

        var request = _mapper.Map<User>(model);

        var dbModel = await _dbRepository.AddAsync(request);

        var result = _mapper.Map<UserData>(dbModel);

        result.Role = countUser == 0 ? Roles.Admin : Roles.User;

        return result;
    }

    public async Task<UserData> UpdateAsync(UpdateUserData model)
    {
        var user = await _dbRepository.GetOneByExpressionAsync<User>(r => r.Id == model.Id);

        var updateModel = _mapper.Map(model, user);

        if (updateModel == null) throw new NullReferenceException("mapper is not found");

        await _dbRepository.UpdateOneAsync(updateModel, r => r.Id == model.Id);

        var result = _mapper.Map<UserData>(updateModel);

        return result;
    }

    public async Task DeleteAsync(Guid id)
    {
        await _dbRepository.DeleteOneAsync<User>(r => r.Id == id);
    }
}
