using CartService.Application.Carts.Queries.GetCarts;

namespace CartService.Web.Endpoints;

public class Cart : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetCart, "{cartKey}");
    }

    public Task<CartDto> GetCart(ISender sender, string cartKey)
    {
        return sender.Send(new GetCartQuery(cartKey));
    }
}
