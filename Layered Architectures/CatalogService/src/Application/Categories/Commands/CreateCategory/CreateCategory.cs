using CatalogService.Application.Common.Interfaces;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand : IRequest<int>
{
    public required string Name { get; set; }
    public Uri? Image { get; set; }
    public int? ParentCategoryId { get; set; } = null;
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new Category
        {
            Name = request.Name,
            Image = request.Image,
            ParentCategoryId = request.ParentCategoryId
        };

        _context.Categories.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        //entity.AddDomainEvent(new ProductCreatedEvent(entity));
        return entity.Id;
    }
}
