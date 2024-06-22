using CatalogService.Application.Common.Validation;

namespace CleanArchitecture.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(50)
            .NotEmpty()
            .Must(PlainTextExtention.BePlainText).WithMessage("Name must not contain HTML tags");
        
        RuleFor(v => v.Amount)
            .GreaterThan(0).WithMessage("Amount must be a positive number.");
    }
}
