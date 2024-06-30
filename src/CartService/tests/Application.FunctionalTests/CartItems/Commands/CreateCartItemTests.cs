using System.Globalization;
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
        var command = new CreateCartItemCommand { CartKey = 1.ToString() };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateCartItem()
    {
        var command = new CreateCartItemCommand { CartKey = 3.ToString(), ProductKey = "3", Name = "3", Price = new Money(1, "USD"), Quantity = 1 };

        var itemId = await SendAsync(command);

        var item = await FindAsync<Cart>(x=>x.CartKey == command.CartKey);

        item.Should().NotBeNull();

        item!.Items.Where(x => x.Name == command.Name).Should().NotBeEmpty();
    }
}
