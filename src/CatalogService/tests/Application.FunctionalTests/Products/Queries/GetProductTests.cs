using CatalogService.Application.Categories.Commands.CreateCategory;
using CatalogService.Domain.Entities;
using CatalogService.Domain.ValueObjects;
using CleanArchitecture.Application.Products.Queries.GetProduct;

namespace CatalogService.Application.FunctionalTests.Products.Queries;

using static Testing;

public class GetProductTests : BaseTestFixture
{

    [Test]
    public async Task ShouldReturnProduct()
    {
        await RunAsDefaultUserAsync();

        var categoryId = await SendAsync(new CreateCategoryCommand
        {
            Name = "New Category"
        });

        await AddAsync(new Product {
            CategoryId = categoryId,
            Name = "Product1",
            Amount = 1,
            Price = new Money(1, "USD")
        });

        await AddAsync(new Product
        {
            CategoryId = categoryId,
            Name = "Product2",
            Amount = 1,
            Price = new Money(1, "USD")
        });

        var query = new GetProductQuery(1);

        var result = await SendAsync(query);

        result.Name.Should().BeEquivalentTo("Product1");
    }
    /*
    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        var query = new GetProductQuery(1);

        var action = () => SendAsync(query);
        
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }*/
}
