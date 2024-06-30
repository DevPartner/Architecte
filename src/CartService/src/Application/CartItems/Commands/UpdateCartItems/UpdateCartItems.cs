using CartService.Application.Common.Interfaces;
using CartService.Domain.Entities;
using CartService.Domain.ValueObjects;

namespace CartService.Application.CartItems.Commands.UpdateCartItems;

public record UpdateCartItemsCommand : IRequest
{
    public required string Name { get; init; }
    public required Money Price { get; init; }
    public string? ProductKey { get; set; }
}

public class UpdateCartItemsCommandHandler : IRequestHandler<UpdateCartItemsCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IRepository<Cart> _repository;

    public UpdateCartItemsCommandHandler(IApplicationDbContext context, IRepository<Cart> repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task Handle(UpdateCartItemsCommand request, CancellationToken cancellationToken)
    {
        var carts = await _repository.GetAsync(cancellationToken);
        var cartsArray = carts.ToArray();
        var index = 0;
        while (index < cartsArray.Length && !cancellationToken.IsCancellationRequested)
        {
            var cart = cartsArray[index];
            var existingCartItem = cart.Items.FirstOrDefault(item => item.ProductKey == request.ProductKey);

            if (existingCartItem != null)
            {
                // Update the existing cart item
                existingCartItem.Name = request.Name;
                existingCartItem.Price = request.Price;

                await _repository.UpdateAsync(cart, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            index++;
        }
    }
}
