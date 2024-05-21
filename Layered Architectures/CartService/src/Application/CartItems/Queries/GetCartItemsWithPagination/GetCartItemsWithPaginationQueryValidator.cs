namespace CartService.Application.CartItems.Queries.GetItemsWithPagination;

public class GetCartItemsWithPaginationQueryValidator : AbstractValidator<GetCartItemsWithPaginationQuery>
{
    public GetCartItemsWithPaginationQueryValidator()
    {
        RuleFor(x => x.CartId)
            
            .NotEmpty().WithMessage("CartId is required.");

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
