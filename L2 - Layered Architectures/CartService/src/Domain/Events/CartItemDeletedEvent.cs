namespace CartService.Domain.Events;

public class CartItemDeletedEvent : BaseEvent
{
    public CartItemDeletedEvent(CartItem item)
    {
        Item = item;
    }

    public CartItem Item { get; }
}
