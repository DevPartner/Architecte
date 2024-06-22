using CatalogService.Application.Categories.Commands.CreateCategory;
using CatalogService.Application.Categories.Queries.GetCategory;
namespace CatalogService.Application.FunctionalTests.Categorys.Queries;

using static Testing;

public class GetCategoryTests : BaseTestFixture
{

    [Test]
    public async Task ShouldReturnCategory()
    {
        await RunAsDefaultUserAsync();

        var categoryId = await SendAsync(new CreateCategoryCommand
        {
            Name = "New Category"
        });

        var query = new GetCategoryQuery(categoryId);

        var result = await SendAsync(query);

        result.Name.Should().BeEquivalentTo("New Category");
    }

}
