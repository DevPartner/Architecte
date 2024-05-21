using System.Linq.Expressions;
using CartService.Application.Common.Interfaces;
using CartService.Application.Common.Models;
using CartService.Domain.Common;
using LiteDB;

namespace CartService.Infrastructure.Data;
public class LiteRepository<T> : IRepository<T> where T : class, IBaseEntity//, new()
{
    private readonly LiteDatabase _database;
    private readonly ILiteCollection<T> _collection;

    public LiteRepository(LiteDatabase database)
    {
        _database = database;
        _collection = _database.GetCollection<T>(nameof(T));
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        bool result = _collection.Delete(new BsonValue(id));
        return Task.FromResult(result);
    }

    public Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken)
    {
        IEnumerable<T> result = _collection.FindAll();
        return Task.FromResult(result);
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate,  CancellationToken cancellationToken)
    {
        var result = _collection.Find(predicate).FirstOrDefault();
        return await Task.FromResult(result);
    }

    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var bsonId = new BsonValue(id);
        T result = _collection.FindOne(x => x.Id == bsonId);
        return await Task.FromResult(result);
    }

    public async Task<PaginatedList<T>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate)
    {
        var query = _collection.Find(predicate).Skip((pageNumber - 1) * pageSize).Take(pageSize);
        var total = _collection.Count(predicate);
        return await Task.FromResult(new PaginatedList<T>(query.ToList(), total, pageNumber, pageSize));
    }

    public async Task<int> InsertAsync(T entity, CancellationToken cancellationToken)
    {
        BsonValue id = _collection.Insert(entity);
        return await Task.FromResult(id.AsInt32);
    }

    public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        bool result = _collection.Update(entity);
        return await Task.FromResult(result);
    }
}
