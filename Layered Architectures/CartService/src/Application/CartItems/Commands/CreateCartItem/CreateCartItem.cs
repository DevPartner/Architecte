using CartService.Application.Common.Interfaces;
using CartService.Domain.Entities;
using CartService.Domain.Events;

namespace CartService.Application.CartItems.Commands.CreateItem;

public record CreateCartItemCommand : IRequest<int>
{
    public int CartId { get; init; }
    public string? Name { get; init; }
    public string? ImageUrl { get; set; }
    public string? AltText { get; set; }
    public decimal Price { get; set; }

}

public class CreateCartItemCommandHandler : IRequestHandler<CreateCartItemCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IRepository<CartItem> _repository;

    public CreateCartItemCommandHandler(IApplicationDbContext context, IRepository<CartItem> repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task<int> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {

        var entity = new CartItem
        {
            CartId = request.CartId,
            Name = request.Name,
            ImageUrl = request.ImageUrl,
            AltText = request.AltText,
            Price = request.Price
        };

        entity.AddDomainEvent(new CartItemCreatedEvent(entity));

        await _repository.InsertAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
