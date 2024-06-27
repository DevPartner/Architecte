using CartService.Application.FunctionalTests;

namespace CleanArchitecture.Application.FunctionalTests;
public static class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
        var database = new LiteDBTestDatabase();

        await database.InitialiseAsync();

        return database;
    }
}
