using System.Linq.Expressions;

namespace Freedom.Access.Mongo;

public interface IDbRepository
{
    Task<T?> GetOneByExpressionAsync<T>(Expression<Func<T, bool>> where) where T : class;
    Task<T[]> GetAllByExpressionAsync<T>(Expression<Func<T, bool>> where) where T : class;
    Task<long> CountAsync<T>(Expression<Func<T, bool>>? where = null) where T : class;
    Task<T[]> GetAsync<T>(int page, int size, Expression<Func<T, bool>>? where = null) where T : class;
    Task DeleteOneAsync<T>(Expression<Func<T, bool>> where) where T : class;
    Task DeleteManyAsync<T>(Expression<Func<T, bool>> where) where T : class;
    Task UpdateOneAsync<T>(T model, Expression<Func<T, bool>> where) where T : class;
    Task<T> AddAsync<T>(T model) where T : class;
}
