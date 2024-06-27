using CartService.Domain.Entities;
using CartService.Domain.ValueObjects;

namespace CartService.Application.Carts.Queries.GetCart;

public class CartItemDto
{
    public int Id { get; init; }
    public required string Name { get; set; }
    public Image? Image { get; set; }
    public required Money Price { get; set; }
    public decimal Quantity { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CartItem, CartItemDto>();
        }
    }
}
