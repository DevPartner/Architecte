using CatalogService.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(v => v.Name)
            .MaximumLength(50)
            .NotEmpty()
            .MustAsync(BeUniqueName)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .AllAsync(l => l.Name != name, cancellationToken);
    }
}
