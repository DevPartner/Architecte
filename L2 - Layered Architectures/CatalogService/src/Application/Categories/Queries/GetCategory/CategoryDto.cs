using CatalogService.Domain.Entities;
using CleanArchitecture.Application.Products.Queries.GetProduct;

namespace CatalogService.Application.Categories.Queries.GetCategory;

public class CategoryDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public Uri? Image { get; set; }
    public int? ParentCategoryId { get; set; } = null;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
