namespace CartService.Domain.ValueObjects;
public class Image : ValueObject
{
    public string Url { get; set; }
    public string AltText { get; set; }

    public Image(string url, string altText)
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
