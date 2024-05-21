namespace CartService.Domain.Entities;

public class CartItem : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public string? AltText { get; set; }
    public decimal Price { get; set; }
    public int CartId { get; set; }
}
