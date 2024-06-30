using CartService.Application.CartItems.Commands.CreateItem;
using CartService.Application.CartItems.Commands.DeleteItem;
using CartService.Domain.Entities;
using CartService.Domain.ValueObjects;

namespace CleanArchitecture.Application.FunctionalTests;

using static Testing;

public class DeleteCartItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidCartItemId()
    {
        var command = new DeleteCartItemCommand { CartKey = 99.ToString(), ItemId = 1 };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteCartItem()
    {
        var command = new CreateCartItemCommand { CartKey = 1.ToString(), ProductKey = "1", Name = "1", Price = new Money(1, "USD"), Quantity = 1 };

        var itemId = await SendAsync(command);
        
        var command2 = new CreateCartItemCommand { CartKey = 1.ToString(), ProductKey = "2", Name = "2", Price = new Money(1, "USD"), Quantity = 1 };

        var itemId2 = await SendAsync(command2);

        await SendAsync(new DeleteCartItemCommand { CartKey = 1.ToString(), ItemId = itemId });

        var cart = await FindAsync<Cart>(x => x.CartKey == command.CartKey);

        cart.Should().NotBeNull();

        cart!.Items.Where(x => x.Name == command.Name).Should().BeEmpty();
        cart!.Items.FirstOrDefault(x => x.Name == command2.Name).Should().NotBeNull();
    }
}
