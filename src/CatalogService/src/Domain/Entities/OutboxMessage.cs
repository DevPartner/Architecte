namespace CatalogService.Domain.Entities;
public class OutboxMessage : BaseAuditableEntity
{
    public int ItemId { get; set; }
    public required string Type { get; set; }
    public required string Data { get; set; }
    public bool Processed { get; set; } = false;
}
