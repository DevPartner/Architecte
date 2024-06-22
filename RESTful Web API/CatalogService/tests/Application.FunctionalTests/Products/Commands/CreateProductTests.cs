using CatalogService.Application.Categories.Commands.CreateCategory;
using CatalogService.Application.Common.Exceptions;
using CatalogService.Domain.Entities;
using CatalogService.Domain.ValueObjects;
using CleanArchitecture.Application.Products.Commands.CreateProduct;

namespace CatalogService.Application.FunctionalTests.Products.Commands;

using static Testing;

public class CreateProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateProductCommand()
        {
            Name = "",
            Amount = 1,
            Price = new Money(1, "USD")
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateProduct()
    {
        var userId = await RunAsDefaultUserAsync();

        var catId = await SendAsync(new CreateCategoryCommand
        {
            Name = "New Category"
        });

        var command = new CreateProductCommand
        {
            CategoryId = catId,
            Name = "Product",
            Amount = 1,
            Price = new Money(1, "USD")
        };

        var itemId = await SendAsync(command);

        var item = await FindAsync<Product>(itemId);

        item.Should().NotBeNull();
        item!.CategoryId.Should().Be(command.CategoryId);
        item.Name.Should().Be(command.Name);
        item.CreatedBy.Should().Be(userId);
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
