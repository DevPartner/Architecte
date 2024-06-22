using CatalogService.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateCategoryCommandValidator(IApplicationDbContext context)
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
