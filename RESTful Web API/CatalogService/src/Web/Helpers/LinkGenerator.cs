using CatalogService.Web.Models;

namespace CatalogService.Web.Helpers;

public class LinkGenerator : ILinkGenerator
{
    public Dictionary<string, LinkDto> GetLinks(string basePath, int id)
    {
        return new Dictionary<string, LinkDto>
            {
                { "self", new LinkDto($"{basePath}/{id}", "GET") },
                { "update", new LinkDto($"{basePath}/{id}", "PUT") },
                { "delete", new LinkDto($"{basePath}/{id}", "DELETE") },
                { "list", new LinkDto($"{basePath}", "GET") },
                { "create", new LinkDto($"{basePath}", "POST") }
            };
    }

    public Dictionary<string, LinkDto> GetPaginationLinks(string basePath, int currentPage, int pageSize, int totalPages)
    {
        var links = new Dictionary<string, LinkDto>
            {
                { "self", new LinkDto($"{basePath}?page={currentPage}&size={pageSize}", "GET") },
                { "first", new LinkDto($"{basePath}?page=1&size={pageSize}", "GET") },
                { "last", new LinkDto($"{basePath}?page={totalPages}&size={pageSize}", "GET") }
            };

        if (currentPage > 1)
        {
            links.Add("prev", new LinkDto($"{basePath}?page={currentPage - 1}&size={pageSize}", "GET"));
        }

        if (currentPage < totalPages)
        {
            links.Add("next", new LinkDto($"{basePath}?page={currentPage + 1}&size={pageSize}", "GET"));
        }

        return links;
    }
}
