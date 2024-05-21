using CartService.Domain.ValueObjects;

namespace CartService.Domain.Entities;

public class CartItem : BaseAuditableEntity
{
    public required string Name { get; set; }
    public Image? Image { get; set; }
    public required Money Price { get; set; }
    public decimal Quantity { get; set; }
}
