using CartService.Domain.Entities;

namespace CartService.Application.CartItems.Queries.GetItemsWithPagination;

public class CartItemDto
{
    public int Id { get; init; }
    public int CartId { get; init; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public string? AltText { get; set; }
    public decimal Price { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CartItem, CartItemDto>();
        }
    }
}
