using CleanArchitecture.Application.Products.Queries.GetProduct;

namespace CatalogService.Web.Models;

public class PaginatedHateosList<T>
{
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public int TotalPages { get; private set; }
    public Dictionary<string, LinkDto> Links { get; private set; }
    public IList<T> Items { get; private set; }


    public PaginatedHateosList(List<T> items, Dictionary<string, LinkDto> links, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = count;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        Links = links;
        Items = items;
    }
}
