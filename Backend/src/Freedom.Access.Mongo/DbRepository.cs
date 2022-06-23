using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Freedom.Access.Mongo;

internal class DbRepository: IDbRepository
{
    private readonly IMongoDatabase _mongoDatabase;

    public DbRepository(IOptions<MongoConfiguration> configuration)
    {
        var client = new MongoClient(configuration.Value.ConnectionString);

        _mongoDatabase = client.GetDatabase(configuration.Value.DataBase);

        if (_mongoDatabase == null) throw new MongoClientException("could not connect to db");
    }

    public async Task<T?> GetOneByExpressionAsync<T>(Expression<Func<T, bool>> where) where T : class
    {
        var collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);

        var result = await collection.Find(where).FirstOrDefaultAsync();

        return result;
    }

    public async Task<T[]> GetAllByExpressionAsync<T>(Expression<Func<T, bool>> where) where T : class
    {
        var collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);

        var result = await collection.Find(where).ToListAsync();

        return result?.ToArray() ?? Array.Empty<T>();
    }

    public async Task DeleteOneAsync<T>(Expression<Func<T, bool>> where) where T : class
    {
        var collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);

        await collection.DeleteOneAsync(where);
    }

    public async Task DeleteManyAsync<T>(Expression<Func<T, bool>> where) where T : class
    {
        var collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);

        await collection.DeleteManyAsync(where);
    }

    public async Task UpdateOneAsync<T>(T model, Expression<Func<T, bool>> where) where T : class
    {
        var collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);

        await collection.ReplaceOneAsync(where, model);
    }

    public async Task<T> AddAsync<T>(T model) where T : class
    {
        var collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);

        await collection.InsertOneAsync(model);

        return model;
    }
}
