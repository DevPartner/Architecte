namespace CatalogService.Domain.Entities;

public class Category : BaseAuditableEntity
{
    public required string Name { get; set; }
    public Uri? Image { get; set; }
    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public ICollection<Category> Children { get; set; } = new List<Category>();
}
