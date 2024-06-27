namespace CartService.Domain.Events;

public class CartItemCreatedEvent : BaseEvent
{
    public CartItemCreatedEvent(CartItem item)
    {
        Item = item;
    }

    public CartItem Item { get; }
}
