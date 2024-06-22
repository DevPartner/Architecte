using CatalogService.Application.Categories.Queries.GetCategory;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Application.Common.Mappings;
using CatalogService.Application.Common.Models;

namespace CatalogService.Application.Categories.Queries.GetCategoriesWithPagination;

public record GetCategoriesWithPaginationQuery : IRequest<PaginatedList<CategoryDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCategoriesWithPaginationQueryHandler : IRequestHandler<GetCategoriesWithPaginationQuery, PaginatedList<CategoryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoriesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CategoryDto>> Handle(GetCategoriesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .OrderBy(x => x.Name)
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
