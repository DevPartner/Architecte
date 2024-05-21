using CartService.Application.Common.Interfaces;
using CartService.Infrastructure.Data;
using LiteDB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CleanArchitecture.Application.FunctionalTests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName;

    public CustomWebApplicationFactory()
    {
        _dbName = Path.GetTempFileName();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<IApplicationDbContext>()
                    .AddSingleton(new LiteDatabase(new MemoryStream()))
                    // For transient service use Func<IServiceProvider, LiteDatabase> factory
                    .AddSingleton<IApplicationDbContext, ApplicationDbContext>();
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            File.Delete(_dbName);
        }

        base.Dispose(disposing);
    }
}
