using CartService.Domain.Common;
using CartService.Domain.Entities;

namespace CartService.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
