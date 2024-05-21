using CartService.Application.CartItems.Commands.CreateItem;
using CartService.Application.CartItems.Commands.DeleteItem;

namespace CartService.Web.Endpoints;

public class CartItems : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateCartItem)
            .MapDelete(DeleteCartItem);
    }

    public Task<int> CreateCartItem(ISender sender, CreateCartItemCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> DeleteCartItem(ISender sender, [AsParameters] DeleteCartItemCommand command)
    {
        await sender.Send(command);
        return Results.NoContent();
    }
}
