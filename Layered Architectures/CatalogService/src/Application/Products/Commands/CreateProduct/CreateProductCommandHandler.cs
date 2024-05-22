
using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;

namespace CleanArchitecture.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Product
        {
            CategoryId = request.CategoryId,
            Name = request.Name,
            Description = request.Description,
            Image = request.Image,
            Price = request.Price,
            Amount = request.Amount
        };

        _context.Products.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        //entity.AddDomainEvent(new ProductCreatedEvent(entity));
        return entity.Id;
    }
}
