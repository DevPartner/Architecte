using CatalogService.Application.Common.Exceptions;
using CatalogService.Domain.Entities;
using CatalogService.Application.Categories.Commands.CreateCategory;

namespace CatalogService.Application.FunctionalTests.Categorys.Commands;

using static Testing;

public class CreateCategoryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateCategoryCommand() { Name = ""};
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireUniqueName()
    {
        await SendAsync(new CreateCategoryCommand
        {
            Name = "Shopping"
        });

        var command = new CreateCategoryCommand
        {
            Name = "Shopping"
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateCategory()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateCategoryCommand
        {
            Name = "Tasks"
        };

        var id = await SendAsync(command);

        var cat = await FindAsync<Category>(id);

        cat.Should().NotBeNull();
        cat!.Name.Should().Be(command.Name);
        cat.CreatedBy.Should().Be(userId);
        cat.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
