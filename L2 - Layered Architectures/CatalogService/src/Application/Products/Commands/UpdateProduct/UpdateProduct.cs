using System.Diagnostics;
using System.Xml.Linq;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.ValueObjects;

namespace CleanArchitecture.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest
{
    public int Id { get; init; }
    public int CategoryId { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public Uri? Image { get; init; }
    public required Money Price { get; init; }
    public required int Amount { get; init; }

}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products
            .FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.CategoryId = request.CategoryId;
        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Image = request.Image;
        entity.Price = request.Price;
        entity.Amount = request.Amount;

        await _context.SaveChangesAsync(cancellationToken);
        //entity.AddDomainEvent(new ProductUpdatedEvent(entity));
    }
}
