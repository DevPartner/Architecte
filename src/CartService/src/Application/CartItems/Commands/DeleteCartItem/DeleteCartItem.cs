using CartService.Application.Common.Interfaces;
using CartService.Domain.Entities;
using CartService.Domain.Events;

namespace CartService.Application.CartItems.Commands.DeleteItem;

public record DeleteCartItemCommand : IRequest
{
    public required string CartKey { get; init; }
    public required int ItemId { get; set; }
}

public class DeleteCartItemCommandHandler : IRequestHandler<DeleteCartItemCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IRepository<Cart> _repository;

    public DeleteCartItemCommandHandler(IApplicationDbContext context, IRepository<Cart> repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
    {
        var cart = await _repository.FirstOrDefaultAsync(x => x.CartKey == request.CartKey, cancellationToken);

        Guard.Against.NotFound(request.CartKey, cart);

        var cartItem = cart.Items.ToArray()[request.ItemId];

        Guard.Against.NotFound(request.ItemId, cartItem);

        if (cartItem != null)
        {
            (cart.Items as List<CartItem>)?.Remove(cartItem);
            cart.AddDomainEvent(new CartItemDeletedEvent(cartItem));

            await _repository.UpdateAsync(cart, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
