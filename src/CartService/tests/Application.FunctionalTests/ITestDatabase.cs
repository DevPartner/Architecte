using System.Data.Common;

namespace CartService.Application.FunctionalTests;

public interface ITestDatabase
{
    Task InitialiseAsync();

    Task ResetAsync();

    Task DisposeAsync();
}
