using CatalogService.Domain.Entities;
using CatalogService.Domain.ValueObjects;

namespace CleanArchitecture.Application.Products.Queries.GetProduct;

public class ProductDto
{
    public int Id { get; set; }
    public int CategoryId { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public Uri? Image { get; init; }
    public required Money Price { get; init; }
    public required int Amount { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
