using CartService.Application.Common.Interfaces;
using CartService.Domain.Entities;
using CartService.Domain.Events;
using CartService.Domain.ValueObjects;

namespace CartService.Application.CartItems.Commands.CreateItem;

public record CreateCartItemCommand : IRequest<int>
{
    public int CartId { get; init; }
    public string? Name { get; init; }
    public Image? Image { get; init; }
    public Money? Price { get; init; }
    public decimal Quantity { get; init; }
}

public class CreateCartItemCommandHandler : IRequestHandler<CreateCartItemCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IRepository<Cart> _repository;

    public CreateCartItemCommandHandler(IApplicationDbContext context, IRepository<Cart> repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task<int> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {
        var cart = await _repository.FirstOrDefaultAsync(x => x.Id == request.CartId, cancellationToken);

        if (cart == null)
        {
            // if cart is null, create a new cart
            cart = new Cart { Id = request.CartId };
            await _repository.InsertAsync(cart, cancellationToken);
        }

        var entity = new CartItem
        {
            Name = request.Name!,
            Price = request.Price!,
            Image = request.Image,
            Quantity = request.Quantity
        };

        entity.AddDomainEvent(new CartItemCreatedEvent(entity));


        // add or update cartItem in the cart's Items collection
        var existingCartItem = cart.Items.FirstOrDefault(item => item.Name == entity.Name && item.Price == item.Price);

        if (existingCartItem != null)
        {
            // update existing cartItem
            existingCartItem.Quantity += entity.Quantity;
            existingCartItem.Image = entity.Image;
        }
        else
        {
            // add new cartItem
            (cart.Items as List<CartItem>)?.Add(entity);
        }

        await _repository.UpdateAsync(cart, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
