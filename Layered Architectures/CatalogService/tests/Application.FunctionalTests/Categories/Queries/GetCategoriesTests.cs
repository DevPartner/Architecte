using CatalogService.Application.Categories.Commands.CreateCategory;
using CatalogService.Application.Categories.Queries.GetCategoriesWithPagination;

namespace CatalogService.Application.FunctionalTests.Categorys.Queries;

using static Testing;

public class GetCategoriesTestsCategory : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnPaginatedListCategorys()
    {
        await RunAsDefaultUserAsync();

        var categoryId = await SendAsync(new CreateCategoryCommand
        {
            Name = "New Category"
        });

        var query = new GetCategoriesWithPaginationQuery();

        var result = await SendAsync(query);

        result.Items.Should().HaveCount(1);
        result.Items.First().Name.Should().BeEquivalentTo("New Category");
    }

}
