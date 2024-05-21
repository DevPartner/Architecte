using System.Data.Common;
using CartService.Application.FunctionalTests;
using CartService.Domain.Entities;
using LiteDB;

namespace CleanArchitecture.Application.FunctionalTests;
public class LiteDBTestDatabase : ITestDatabase
{
    private readonly LiteDatabase _context;

    public LiteDBTestDatabase()
    {
        _context = new LiteDatabase(new MemoryStream());
    }

    //..... other ITestDatabase interface methods implementation

    public Task InitialiseAsync()
    {
        var cartItem = new CartItem { CartId = 1, AltText = "alt", Name = "1", Price = 1 };

        var liteCollection = _context.GetCollection<CartItem>("CartItems");
        liteCollection.Insert(cartItem);

        return Task.CompletedTask;
    }

    public Task ResetAsync()
    {
        _context.DropCollection("CartItems");
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        _context.Dispose();
        return Task.CompletedTask;
    }

    public LiteDatabase Context => _context;

    // ... other required methods ...
}
