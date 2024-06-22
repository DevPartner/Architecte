using CatalogService.Domain.ValueObjects;

namespace CleanArchitecture.Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<int>
{
    public int CategoryId { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public Uri? Image { get; init; }
    public required Money Price { get; init; }
    public required int Amount { get; init; }
}
