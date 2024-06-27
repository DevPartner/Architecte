using System.Text.RegularExpressions;

namespace CatalogService.Application.Common.Validation;
public static class PlainTextExtention
{
    public static bool BePlainText(string name)
    {
        var regex = new Regex("<.*?>");
        return !regex.IsMatch(name);
    }
}
