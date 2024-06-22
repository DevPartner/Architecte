using CatalogService.Application.Common.Interfaces;
using CatalogService.Infrastructure.Data;
using CatalogService.Web.Endpoints;
using CatalogService.Web.Helpers;
using CatalogService.Web.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using NSwag;
using NSwag.Generation.Processors.Security;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {

        // Register the AutoMapper profile
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Register LinkGenerator as scoped
        services.AddScoped<ILinkGenerator, CatalogService.Web.Helpers.LinkGenerator>();

        // Register the Products class
        services.AddScoped<Products>();

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<IUser, CurrentUser>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();

        services.AddOpenApiDocument((configure, sp) =>
        {
            configure.Title = "CatalogService API";

            // Add JWT
            configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: Bearer {your JWT token}."
            });

            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });

        return services;
    }
}
