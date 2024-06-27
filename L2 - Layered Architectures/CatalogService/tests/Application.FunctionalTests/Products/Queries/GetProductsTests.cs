using CatalogService.Application.Categories.Commands.CreateCategory;
using CatalogService.Domain.Entities;
using CatalogService.Domain.ValueObjects;
using CleanArchitecture.Application.Products.Queries.GetProductsWithPagination;

namespace CatalogService.Application.FunctionalTests.Categorys.Queries;

using static Testing;

public class GetProductsTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnPaginatedListProducts()
    {
        await RunAsDefaultUserAsync();

        var categoryId = await SendAsync(new CreateCategoryCommand
        {
            Name = "New Category"
        });

        await AddAsync(new Product
        {
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

        var query = new GetProductsWithPaginationQuery();

        var result = await SendAsync(query);

        result.Items.Should().HaveCountGreaterThan(1);
        result.Items.First().Name.Should().BeEquivalentTo("Product1");
    }
    /*
    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        var query = new GetProductsWithPaginationQuery();

        var action = () => SendAsync(query);
        
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }*/
}
