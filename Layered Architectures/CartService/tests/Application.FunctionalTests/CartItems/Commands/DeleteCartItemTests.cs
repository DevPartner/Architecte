using CartService.Application.CartItems.Commands.CreateItem;
using CartService.Application.CartItems.Commands.DeleteItem;
using CartService.Domain.Entities;
using CleanArchitecture.Application.FunctionalTests;

namespace CleanArchitecture.Application.FunctionalTests;

using static Testing;

public class DeleteCartItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidCartItemId()
    {
        var command = new DeleteCartItemCommand(99);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteCartItem()
    {
        var command = new CreateCartItemCommand { CartId = 1, AltText = "alt", Name = "1", Price = 1 };

        var itemId = await SendAsync(command);

        await SendAsync(new DeleteCartItemCommand(itemId));

        var item = await FindAsync<CartItem>(itemId);

        item.Should().BeNull();
    }
}
