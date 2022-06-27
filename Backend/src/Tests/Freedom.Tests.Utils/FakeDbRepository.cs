using System.Collections.Concurrent;
using System.Linq.Expressions;
using Freedom.Access.Mongo;
using Freedom.Common.Json;

namespace Freedom.Tests.Utils;

public class FakeDbRepository : IDbRepository
{
    private static readonly ConcurrentDictionary<string, ConcurrentBag<string>> Store =
        new();

    public async Task<T?> GetOneByExpressionAsync<T>(Expression<Func<T, bool>> where) where T : class
    {
        var collection = GetCollection<T>();
        var elementCollection = GetCollection<T>(collection);

        var result = elementCollection.FirstOrDefault(where.Compile());

        return await Task.FromResult(result);
    }

    public async Task<T[]> GetAllByExpressionAsync<T>(Expression<Func<T, bool>> where) where T : class
    {
        var collection = GetCollection<T>();
        var elementCollection = GetCollection<T>(collection);

        var result = elementCollection.Where(where.Compile());

        return await Task.FromResult(result.ToArray());
    }

    public async Task<long> CountAsync<T>(Expression<Func<T, bool>>? where = null) where T : class
    {
        var collection = GetCollection<T>();
        var elementCollection = GetCollection<T>(collection);

        return await Task.FromResult(where != null
            ? elementCollection.Where(where.Compile()).Count()
            : elementCollection.Count());
    }

    public async Task<T[]> GetAsync<T>(int page, int size, Expression<Func<T, bool>>? where = null) where T : class
    {
        var collection = GetCollection<T>();
        var elementCollection = GetCollection<T>(collection);

        var query = where != null
            ? elementCollection.Where(where.Compile()).AsQueryable()
            : elementCollection.AsQueryable();


        return await Task.FromResult(query.Skip(page * size).Take(size).ToArray());
    }

    public async Task DeleteOneAsync<T>(Expression<Func<T, bool>> where) where T : class
    {
        var collection = GetCollection<T>();
        var elementCollection = GetCollection<T>(collection);

        var arr = elementCollection as T[] ?? elementCollection.ToArray();

        var item = arr.First(where.Compile());

        elementCollection = arr.Where(r => r != item);

        collection.Clear();
        foreach (var newVal in elementCollection)
        {
            collection.Add(JsonConvert.GetJsonObj(newVal));
        }

        await Task.CompletedTask;
    }

    public async Task DeleteManyAsync<T>(Expression<Func<T, bool>> where) where T : class
    {
        var collection = GetCollection<T>();
        var elementCollection = GetCollection<T>(collection);

        var arr = elementCollection as T[] ?? elementCollection.ToArray();

        var item = arr.Where(where.Compile());

        elementCollection = arr.Where(r => !item.Contains(r));

        collection.Clear();
        foreach (var newVal in elementCollection)
        {
            collection.Add(JsonConvert.GetJsonObj(newVal));
        }

        await Task.CompletedTask;
    }

    public async Task UpdateOneAsync<T>(T model, Expression<Func<T, bool>> where) where T : class
    {
        var collection = GetCollection<T>();
        var elementCollection = GetCollection<T>(collection);

        var arr = elementCollection as T[] ?? elementCollection.ToArray();

        var item = arr.First(where.Compile());
        var index = Array.IndexOf(arr, item);

        arr[index] = model;

        collection.Clear();
        foreach (var newVal in arr)
        {
            collection.Add(JsonConvert.GetJsonObj(newVal));
        }

        await Task.CompletedTask;
    }

    public async Task<T> AddAsync<T>(T model) where T : class
    {
        var collection = GetCollection<T>();
        collection.Add(JsonConvert.GetJsonObj(model));

        return await Task.FromResult(model);
    }

    private static IEnumerable<T> GetCollection<T>(ConcurrentBag<string> collection) where T : class
    {
        return collection.Select(JsonConvert.GetObjFromJson<T>)!;
    }

    private static ConcurrentBag<string> GetCollection<T>() where T : class
    {
        var collectionName = typeof(T).Name;
        if (Store.TryGetValue(collectionName, out var value))
        {
            return value;
        }

        var newCollection = new ConcurrentBag<string>();
        Store.TryAdd(collectionName, newCollection);

        return newCollection;
    }
}
