using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Freedom.Access.Mongo;

internal class DbRepository : IDbRepository
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
        var collection = await GetCollectionAsync<T>();

        var result = await collection.Find(where).FirstOrDefaultAsync();

        return result;
    }

    public async Task<T[]> GetAllByExpressionAsync<T>(Expression<Func<T, bool>> where) where T : class
    {
        var collection = await GetCollectionAsync<T>();

        var result = await collection.Find(where).ToListAsync();

        return result?.ToArray() ?? Array.Empty<T>();
    }

    public async Task<long> CountAsync<T>(Expression<Func<T, bool>>? where = null) where T : class
    {
        var collection = await GetCollectionAsync<T>();

        var result = await collection.CountDocumentsAsync(where ?? (r => true));

        return result;
    }

    public async Task<T[]> GetAsync<T>(int page, int size, Expression<Func<T, bool>>? where = null) where T : class
    {
        var collection = await GetCollectionAsync<T>();

        var result = await collection.Find(where ?? (r => true)).Skip(page * size).Limit(size).ToListAsync();

        return result.ToArray();
    }

    public async Task DeleteOneAsync<T>(Expression<Func<T, bool>> where) where T : class
    {
        var collection = await GetCollectionAsync<T>();

        await collection.DeleteOneAsync(where);
    }

    public async Task DeleteManyAsync<T>(Expression<Func<T, bool>> where) where T : class
    {
        var collection = await GetCollectionAsync<T>();

        await collection.DeleteManyAsync(where);
    }

    public async Task UpdateOneAsync<T>(T model, Expression<Func<T, bool>> where) where T : class
    {
        var collection = await GetCollectionAsync<T>();

        await collection.ReplaceOneAsync(where, model);
    }

    public async Task<T> AddAsync<T>(T model) where T : class
    {
        var collection = await GetCollectionAsync<T>();

        await collection.InsertOneAsync(model);

        return model;
    }

    private async Task<IMongoCollection<T>> GetCollectionAsync<T>() where T : class
    {
        var name = typeof(T).Name;

        var collection = _mongoDatabase.GetCollection<T>(name);

        if (collection == null)
        {
            await _mongoDatabase.CreateCollectionAsync(name);
        }

        collection = _mongoDatabase.GetCollection<T>(name);

        return collection;
    }
}
