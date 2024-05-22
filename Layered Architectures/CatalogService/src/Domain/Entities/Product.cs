using CatalogService.Domain.Exceptions;

namespace CatalogService.Domain.Entities;

public class Product : BaseAuditableEntity
{
    public int CategoryId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Uri? Image { get; set; }
    public required Money Price { get; set; }

    private int _amount;
    public int Amount
    {
        get { return _amount; }
        set
        {
            if (value <= 0)
                throw new InvalidAmountException("Amount");
            _amount = value;
        }
    }

    public Category? Category { get; set; }

}
