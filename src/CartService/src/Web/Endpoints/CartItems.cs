using CartService.Application.CartItems.Commands.CreateItem;
using CartService.Application.CartItems.Commands.DeleteItem;

namespace CartService.Web.Endpoints;

public class CartItems : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {

        app.MapGroup(this)
            .MapPost(CreateCartItem)
            .MapDelete(DeleteCartItem, "{cartKey}/{itemId}");
    }

    public async Task<IResult> CreateCartItem(ISender sender, CreateCartItemCommand command)
    {
        var itemId = await sender.Send(command);
        return Results.Ok(itemId);

    }

    public async Task<IResult> DeleteCartItem(ISender sender, string cartKey, int itemId)
    {

        var command = new DeleteCartItemCommand { CartKey = cartKey, ItemId = itemId };
        await sender.Send(command);
        return Results.Ok();
    }
}
