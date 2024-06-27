using CatalogService.Application.Categories.Commands.CreateCategory;
using CatalogService.Domain.Entities;
using CatalogService.Domain.ValueObjects;
using CleanArchitecture.Application.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Products.Commands.UpdateProduct;

namespace CatalogService.Application.FunctionalTests.Products.Commands;

using static Testing;

public class UpdateProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidProductId()
    {
        var command = new UpdateProductCommand { 
            Id = 99, 
            Name = "New Name",
            Amount = 1,
            Price = new Money(1, "USD")
        };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateProduct()
    {
        var userId = await RunAsDefaultUserAsync();

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

        var command = new UpdateProductCommand
        {
            Id = itemId,
            CategoryId = catId,
            Name = "Updated Item Name",
            Price = new Money(1, "USD"),
            Amount = 1
        };

        await SendAsync(command);

        var item = await FindAsync<Product>(itemId);

        item.Should().NotBeNull();
        item!.Name.Should().Be(command.Name);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
