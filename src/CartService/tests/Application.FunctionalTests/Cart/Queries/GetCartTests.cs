using CartService.Application.CartItems.Commands.CreateItem;
using CartService.Application.Carts.Queries.GetCarts;
using CartService.Domain.ValueObjects;

namespace CleanArchitecture.Application.FunctionalTests;

using static Testing;

public class GetCartTests : BaseTestFixture
{

    [Test]
    public async Task ShouldReturnAllListsAndItems()
    {
        var command = new CreateCartItemCommand { CartKey = 1.ToString(), ProductKey = "1", Name = "1", Price = new Money(1, "USD"), Quantity = 1 };

        var itemId = await SendAsync(command);

        var query = new GetCartQuery(1.ToString());

        var result = await SendAsync(query);

        result.Items.Count().Should().BeGreaterThanOrEqualTo(1);
    }
}
