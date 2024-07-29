using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Constants;
using CatalogService.Infrastructure.Configs;
using CatalogService.Infrastructure.Data;
using CatalogService.Infrastructure.Data.Interceptors;
using CatalogService.Infrastructure.Identity;
using CleanArchitecture.Infrastructure.Messaging;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlite(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddTransient<IIdentityService, IdentityService>();

        var jwtSettings = new JwtBearerOptions();
        configuration.Bind("JwtBearerOptions", jwtSettings);

        // Add services to the container.
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
            options.AddPolicy("ManagerRole", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole("Manager");
            });

            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "API");
            });
        });

        services.AddHostedService<OutboxMessagePublisher>();

        return services;
    }
}
