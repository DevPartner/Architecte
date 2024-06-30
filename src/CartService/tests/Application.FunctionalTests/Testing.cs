using System.Linq.Expressions;
using CartService.Application.Common.Interfaces;
using LiteDB;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application.FunctionalTests;

[SetUpFixture]
public partial class Testing
{
    //private static ITestDatabase _database;
    private static CustomWebApplicationFactory _factory = null!;
    private static IServiceScopeFactory _scopeFactory = null!;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        //_database = await TestDatabaseFactory.CreateAsync();

        _factory = new CustomWebApplicationFactory();

        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }

    public static Task<TEntity?> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
    where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var repository = scope.ServiceProvider.GetRequiredService<IRepository<TEntity>>();

        var item = repository.FirstOrDefaultAsync(predicate, new CancellationToken());

        return Task.FromResult(item.Result);
    }

    [OneTimeTearDown]
    public async Task RunAfterAnyTests()
    {
        //await _database.DisposeAsync();
        await _factory.DisposeAsync();
    }
}
