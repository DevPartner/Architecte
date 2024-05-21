using CartService.Application.Common.Models;
using CartService.Application.CartItems.Queries.GetItemsWithPagination;
using CartService.Domain.Entities;

namespace CartService.Application.Common.Mappings;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration) where TDestination : class
        => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();

    public static PaginatedList<TDestination> ProjectToListAsync<TDestination, TSource>(this PaginatedList<TSource> paginatedList, IMapper mapper, int pageSize)
    {
        var mappedItems = mapper.Map<List<TDestination>>(paginatedList.Items);
        return new PaginatedList<TDestination>(mappedItems, paginatedList.TotalCount, paginatedList.PageNumber, pageSize);
    }

}
