using CatalogService.Application.Categories.Commands.CreateCategory;
using CatalogService.Application.Categories.Commands.DeleteCategory;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.FunctionalTests.Categorys.Commands;

using static Testing;

public class DeleteCategoryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidCategoryId()
    {
        var command = new DeleteCategoryCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteCategory()
    {
        var catId = await SendAsync(new CreateCategoryCommand
        {
            Name = "New Category"
        });

        await SendAsync(new DeleteCategoryCommand(catId));

        var cat = await FindAsync<Category>(catId);

        cat.Should().BeNull();
    }
}
