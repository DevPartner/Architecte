using CartService.Domain.ValueObjects;

namespace CartService.Domain.Entities;

public class Cart : BaseAuditableEntity
{
    public IReadOnlyCollection<CartItem> Items { get; set; } = new List<CartItem>();
}
