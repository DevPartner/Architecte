using AutoMapper;
using CatalogService.Domain.ValueObjects;
using CleanArchitecture.Application.Products.Queries.GetProduct;

namespace CatalogService.Web.Models;

public class ProductHaleosDto
{
    public int Id { get; set; }
    public int CategoryId { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public Uri? Image { get; init; }
    public required Money Price { get; init; }
    public required int Amount { get; init; }
    public IDictionary<string, LinkDto>? Links { get; set; }


    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ProductDto, ProductHaleosDto>();
        }
    }
}
