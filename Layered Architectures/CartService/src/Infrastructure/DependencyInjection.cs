using CartService.Application.Common.Interfaces;
using CartService.Domain.Entities;
using CartService.Infrastructure.Data;
using LiteDB;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddSingleton(x => {
            return new LiteDatabase(connectionString);
        });
        services.AddScoped<ApplicationDbContext>();

        // Register the RepositoryService for each of your domain entities
        services.AddScoped<IRepository<CartItem>, LiteRepository<CartItem>>();

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorizationBuilder();

        services.AddSingleton(TimeProvider.System);

        return services;
    }
}
