using CatalogService.Domain.Entities;

namespace CatalogService.Application.Common.Models;

public class LookupDto
{
    public int Id { get; init; }

    public string? Name { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, LookupDto>();
            CreateMap<Category, LookupDto>();
        }
    }
}
