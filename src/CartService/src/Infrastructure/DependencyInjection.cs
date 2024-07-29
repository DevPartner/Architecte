using CartService.Application.Common.Interfaces;
using CartService.Domain.Constants;
using CartService.Domain.Entities;
using CartService.Infrastructure.Configs;
using CartService.Infrastructure.Data;
using CartService.Infrastructure.Messaging;
using CartService.Infrastructure.Services;
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

        services.AddScoped<IRepository<Cart>, LiteRepository<Cart>>();

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<IUser, UserService>();


        var jwtSettings = new JwtBearerOptions();
        configuration.Bind("JwtBearerOptions", jwtSettings);

        services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.Authority = jwtSettings!.Authority;
                options.TokenValidationParameters.ValidateAudience = jwtSettings!.ValidAudience;
            });

        services.AddAuthorizationBuilder();

        services.AddSingleton(TimeProvider.System);

        services.AddAuthorization(options => {
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator));

            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "API");
            });
        });

        services.AddHostedService<MessageListener>();

        return services;
    }
}
