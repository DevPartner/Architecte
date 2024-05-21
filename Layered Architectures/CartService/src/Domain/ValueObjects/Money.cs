namespace CartService.Domain.ValueObjects;

public class Money : ValueObject
{
    public decimal Price { get; set; }
    public string Currency { get; set; } = string.Empty;

    public Money(decimal price, string currency)
    {
        Price = price;
        Currency = currency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Price;
        yield return Currency;
    }
}
