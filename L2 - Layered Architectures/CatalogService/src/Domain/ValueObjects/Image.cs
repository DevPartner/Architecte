namespace CatalogService.Domain.ValueObjects;
public class Image : ValueObject
{
    public Uri Url { get; set; }
    public string AltText { get; set; }

    public Image(Uri url, string altText)
    {
        Url = url;
        AltText = altText;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Url;
        yield return AltText;
    }
}
