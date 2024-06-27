using CartService.Application.CartItems.Commands.CreateItem;
using CartService.Application.Common.Exceptions;
using CartService.Domain.Entities;
using CartService.Domain.ValueObjects;

namespace CleanArchitecture.Application.FunctionalTests;

using static Testing;

public class CreateCartItemTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateCartItemCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateCartItem()
    {
        var command = new CreateCartItemCommand { CartId = 1, Name = "1", Price = new Money(1, "USD"), Quantity = 1 };

        var itemId = await SendAsync(command);

        var item = await FindAsync<Cart>(command.CartId);

        item.Should().NotBeNull();
        item.Items.Where(x=>x.Name == command.Name).Should().NotBeEmpty();
    }
}
