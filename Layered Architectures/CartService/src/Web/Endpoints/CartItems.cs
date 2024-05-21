using CartService.Application.Common.Models;
using CartService.Application.CartItems.Queries.GetItemsWithPagination;
using CartService.Application.CartItems.Commands.CreateItem;
using CartService.Application.CartItems.Commands.DeleteItem;

namespace CartService.Web.Endpoints;

public class CartItems : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetCartItemsWithPagination)
            .MapPost(CreateCartItem)
            .MapDelete(DeleteCartItem, "{id}");
    }

    public Task<PaginatedList<CartItemDto>> GetCartItemsWithPagination(ISender sender, [AsParameters] GetCartItemsWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public Task<int> CreateCartItem(ISender sender, CreateCartItemCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> DeleteCartItem(ISender sender, int id)
    {
        await sender.Send(new DeleteCartItemCommand(id));
        return Results.NoContent();
    }
}
