using CatalogService.Application.Categories.Commands.CreateCategory;
using CatalogService.Domain.Entities;
using CatalogService.Domain.ValueObjects;
using CleanArchitecture.Application.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Products.Commands.DeleteProduct;

namespace CatalogService.Application.FunctionalTests.Products.Commands;

using static Testing;

public class DeleteProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidProductId()
    {
        var command = new DeleteProductCommand(99);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteProduct()
    {

        var catId = await SendAsync(new CreateCategoryCommand
        {
            Name = "New Category"
        });

        var itemId = await SendAsync(new CreateProductCommand
        {
            CategoryId = catId,
            Name = "Product",
            Amount = 1,
            Price = new Money(1, "USD")
        });

        await SendAsync(new DeleteProductCommand(itemId));

        var item = await FindAsync<Product>(itemId);

        item.Should().BeNull();
    }
}
