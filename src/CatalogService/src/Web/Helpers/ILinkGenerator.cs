using CatalogService.Web.Models;

namespace CatalogService.Web.Helpers;
public interface ILinkGenerator
{
    Dictionary<string, LinkDto> GetLinks(string path, int id);
    Dictionary<string, LinkDto> GetPaginationLinks(string path, int currentPage, int pageSize, int totalPages);
}
