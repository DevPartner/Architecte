using CartService.Application.CartItems.Commands.CreateItem;
using CartService.Application.CartItems.Commands.DeleteItem;
using CartService.Domain.Entities;
using CartService.Domain.ValueObjects;
using CleanArchitecture.Application.FunctionalTests;

namespace CleanArchitecture.Application.FunctionalTests;

using static Testing;

public class DeleteCartItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidCartItemId()
    {
        var command = new DeleteCartItemCommand { CartId = 99, Name = "1" };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteCartItem()
    {
        var command = new CreateCartItemCommand { CartId = 1, Name = "1", Price = new Money(1, "USD"), Quantity = 1 };

        var itemId = await SendAsync(command);

        await SendAsync(new DeleteCartItemCommand { CartId = 1, Name = "1" });

        var cart = await FindAsync<Cart>(command.CartId);

        cart.Items.Where(x=>x.Name == command.Name).Should().BeEmpty();
    }
}
