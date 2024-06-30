using System.Reflection.Metadata;
using CartService.Application.CartItems.Commands.CreateItem;
using CartService.Application.CartItems.Commands.UpdateCartItems;
using CartService.Application.Common.Interfaces;
using CartService.Domain.Entities;
using CartService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.FunctionalTests;

using static Testing;

public class UpdateCartItemsTests : BaseTestFixture
{
    [Test]
    public async Task ShouldUpdateExistingCartItem_WhenProductKeyMatches()
    {
        var command = new CreateCartItemCommand { CartKey = 4.ToString(), ProductKey = "4", Name = "3", Price = new Money(1, "USD"), Quantity = 1 };

        var itemId = await SendAsync(command);

        var updateCommand = new UpdateCartItemsCommand
        {
            Name = "NewProduct",
            Price = new Money(100, "USD"),
            ProductKey = "4"
        };

        await SendAsync(updateCommand);

        var cart = await FindAsync<Cart>(x => x.CartKey == command.CartKey);

        cart.Should().NotBeNull();

        cart!.Items.FirstOrDefault().Should().NotBeNull();

        var cartItem = cart!.Items.First();
        
        cartItem.Name.Should().Be(updateCommand.Name);
        cartItem.Price.Should().Be(updateCommand.Price);
    }

    [Test]
    public async Task ShouldNotUpdateAnything_WhenNoProductKeyMatches()
    {
        var command = new CreateCartItemCommand { CartKey = 4.ToString(), ProductKey = "4", Name = "3", Price = new Money(1, "USD"), Quantity = 1 };

        var itemId = await SendAsync(command);

        var updateCommand = new UpdateCartItemsCommand
        {
            Name = "NewProduct",
            Price = new Money(100, "USD"),
            ProductKey = "5"
        };

        await SendAsync(updateCommand);

        var cart = await FindAsync<Cart>(x => x.CartKey == command.CartKey);

        cart.Should().NotBeNull();

        cart!.Items.FirstOrDefault().Should().NotBeNull();

        var cartItem = cart!.Items.First();

        cartItem.Name.Should().Be(command.Name);
        cartItem.Price.Should().Be(command.Price);
    }
}
