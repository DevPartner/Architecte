namespace CatalogService.Web.Models;
public class LinkDto
{
    public string Href { get; set; }
    public string Method { get; set; }

    public LinkDto(string href, string method)
    {
        Href = href;
        Method = method;
    }
}
