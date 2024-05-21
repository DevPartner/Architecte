using CartService.Application.CartItems.Commands.CreateItem;
using CartService.Application.Common.Exceptions;
using CartService.Domain.Entities;
using CleanArchitecture.Application.FunctionalTests;

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
        var command = new CreateCartItemCommand { CartId = 1, AltText = "alt", Name = "1", Price = 1 }; 

        var itemId = await SendAsync(command);

        var item = await FindAsync<CartItem>(itemId);

        item.Should().NotBeNull();
        item!.CartId.Should().Be(command.CartId);
        item.Name.Should().Be(command.Name);
        item.AltText.Should().Be(command.AltText);
        item.Price.Should().Be(command.Price);
    }
}
