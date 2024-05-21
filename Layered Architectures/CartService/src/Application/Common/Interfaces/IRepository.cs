using CartService.Application.Common.Models;
using System.Linq.Expressions;

namespace CartService.Application.Common.Interfaces;
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken);
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> InsertAsync(T entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<PaginatedList<T>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate);
}
