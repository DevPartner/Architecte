using CartService.Application.Common.Interfaces;
using CartService.Domain.Entities;
using CartService.Domain.Events;

namespace CartService.Application.CartItems.Commands.DeleteItem;

public record DeleteCartItemCommand(int Id) : IRequest;

public class DeleteCartItemCommandHandler : IRequestHandler<DeleteCartItemCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IRepository<CartItem> _repository;

    public DeleteCartItemCommandHandler(IApplicationDbContext context, IRepository<CartItem> repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository
            .GetByIdAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        await _repository.DeleteAsync(request.Id, cancellationToken);

        entity.AddDomainEvent(new CartItemDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
