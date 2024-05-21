using CartService.Application.CartItems.Commands.CreateItem;
using CartService.Application.CartItems.Queries.GetItemsWithPagination;

namespace CleanArchitecture.Application.FunctionalTests;

using static Testing;

public class GetCartItemsTests : BaseTestFixture
{

    [Test]
    public async Task ShouldReturnAllListsAndItems()
    {
        var command = new CreateCartItemCommand { CartId = 1, AltText = "alt", Name = "1", Price = 1 };

        var itemId = await SendAsync(command);

        var query = new GetCartItemsWithPaginationQuery() { CartId = 1};

        var result = await SendAsync(query);

        result.Items.Should().HaveCountGreaterThan(1);
    }
}
