using CatalogService.Application.Common.Exceptions;
using CatalogService.Domain.Entities;
using CatalogService.Application.Categories.Commands.UpdateCategory;
using CatalogService.Application.Categories.Commands.CreateCategory;

namespace CatalogService.Application.FunctionalTests.Categorys.Commands;

using static Testing;

public class UpdateCategoryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidCategoryId()
    {
        var command = new UpdateCategoryCommand { Id = 99, Name = "New Name" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldRequireUniqueName()
    {
        var catId = await SendAsync(new CreateCategoryCommand
        {
            Name = "New Category"
        });

        await SendAsync(new CreateCategoryCommand
        {
            Name = "Other Category"
        });

        var command = new UpdateCategoryCommand
        {
            Id = catId,
            Name = "Other Category"
        };

        (await FluentActions.Invoking(() =>
            SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("Name")))
                .And.Errors["Name"].Should().Contain("'Name' must be unique.");
    }

    [Test]
    public async Task ShouldUpdateCategory()
    {
        var userId = await RunAsDefaultUserAsync();

        var catId = await SendAsync(new CreateCategoryCommand
        {
            Name = "New Category"
        });

        var command = new UpdateCategoryCommand
        {
            Id = catId,
            Name = "Updated Category Name"
        };

        await SendAsync(command);

        var cat = await FindAsync<Category>(catId);

        cat.Should().NotBeNull();
        cat!.Name.Should().Be(command.Name);
        cat.LastModifiedBy.Should().NotBeNull();
        cat.LastModifiedBy.Should().Be(userId);
        cat.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
