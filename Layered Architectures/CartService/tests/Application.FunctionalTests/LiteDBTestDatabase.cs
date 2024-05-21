using CartService.Domain.ValueObjects;
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
        var cart = new Cart
        {
            Id = 1,
            Items = new List<CartItem>() {
                new CartItem {  Name = "1", Price = new Money(1, "USD") }
            }
        };

        var liteCollection = _context.GetCollection<Cart>("Cart");
        liteCollection.Insert(cart);

        return Task.CompletedTask;
    }

    public Task ResetAsync()
    {
        _context.DropCollection("Cart");
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        _context.Dispose();
        return Task.CompletedTask;
    }

    public LiteDatabase Context => _context;
}
