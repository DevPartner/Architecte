using CatalogService.Application.Common.Interfaces;
using CleanArchitecture.Application.Products.Queries.GetProduct;

namespace CatalogService.Application.Categories.Queries.GetCategory;

public record GetCategoryQuery(int Id) : IRequest<CategoryDto>
{
}

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoryQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories
            .FindAsync([request.Id], cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        return _mapper.Map<CategoryDto>(entity);
    }
}
