using CartService.Application.Common.Interfaces;
using CartService.Domain.Common;
using CartService.Domain.Entities;
using LiteDB;

namespace CartService.Infrastructure.Data;

public class ApplicationDbContext : IApplicationDbContext
{
    private readonly LiteDatabase _context;

    public ApplicationDbContext(LiteDatabase context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        // 'Commit' returns a bool to indicate success. 
        // It's long 'since' would be the UTC timestamp when lock acquired.
        // Thus we're creating an abstraction over LiteDB's transaction API
        // providing translated meaningful result of operation, like the count of items saved.

        _context.BeginTrans(); //Start transaction
        bool success = _context.Commit(); //Commit changes and get success status
                                          //Abstraction over commit's result
        return Task.FromResult(success ? 1 : 0);
    }
}
