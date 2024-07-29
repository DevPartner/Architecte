using CatalogService.Domain.Entities;
using CatalogService.Domain.ValueObjects;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CatalogService.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!_context.Categories.Any())
        {
            var category1 = new Category
            {
                Name = "Electronics",
            };

            var category2 = new Category
            {
                Name = "Books",
                ParentCategory = category1
            };

            _context.Categories.AddRange(category1, category2);

            _context.Products.AddRange(
                new Product
                {
                    Name = "Laptop",
                    Description = "A high performance laptop",
                    Price = new Money ( 500, "USD" ),
                    Amount = 10,
                    Category = category1
                },
                new Product
                {
                    Name = "Hardcover Book",
                    Description = "A suspense thriller novel",
                    Price = new Money (20, "USD" ),
                    Amount = 30,
                    Category = category2
                }
            );

            await _context.SaveChangesAsync();
        }
    }
}
