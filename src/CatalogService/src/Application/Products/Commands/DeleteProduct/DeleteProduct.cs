﻿using CatalogService.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest
{
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products
            .FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Products.Remove(entity);

        //entity.AddDomainEvent(new ProductDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
