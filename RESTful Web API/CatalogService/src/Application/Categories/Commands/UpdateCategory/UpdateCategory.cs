using CatalogService.Application.Common.Interfaces;
using CatalogService.Application.Common.Validation;

namespace CatalogService.Application.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand : IRequest
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public Uri? Image { get; set; }
    public int? ParentCategoryId { get; set; } = null;
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories
            .FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.Image = request.Image;
        entity.ParentCategoryId = request.ParentCategoryId;

        await _context.SaveChangesAsync(cancellationToken);
        //entity.AddDomainEvent(new ProductUpdatedEvent(entity));
    }
}
