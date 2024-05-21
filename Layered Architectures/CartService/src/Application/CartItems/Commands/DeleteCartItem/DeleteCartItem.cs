using CartService.Application.Common.Interfaces;
using CartService.Domain.Entities;
using CartService.Domain.Events;

namespace CartService.Application.CartItems.Commands.DeleteItem;

public record DeleteCartItemCommand : IRequest
{
    public int CartId { get; init; }
    public string? Name { get; init; }
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
        var cart = await _repository.FirstOrDefaultAsync(x => x.Id == request.CartId, cancellationToken);
        Guard.Against.NotFound(request.CartId, cart);


        var cartItem = cart.Items.FirstOrDefault(item => item.Name == request.Name);

        if (cartItem != null)
        {
            (cart.Items as List<CartItem>)?.Remove(cartItem);
            cart.AddDomainEvent(new CartItemDeletedEvent(cartItem));

            await _repository.UpdateAsync(cart, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
