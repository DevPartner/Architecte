using CartService.Application.Carts.Queries.GetCart;
using CartService.Domain.Entities;

namespace CartService.Application.Carts.Queries.GetCarts;

public class CartDto
{
    public CartDto()
    {
        Items = Array.Empty<CartItemDto>();
    }

    public int Id { get; init; }

    public IReadOnlyCollection<CartItemDto> Items { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Cart, CartDto>();
        }
    }
}
