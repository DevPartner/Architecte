using CatalogService.Application.Categories.Commands.CreateCategory;
using CatalogService.Application.Categories.Commands.DeleteCategory;
using CatalogService.Application.Categories.Commands.UpdateCategory;
using CatalogService.Application.Categories.Queries.GetCategoriesWithPagination;
using CatalogService.Application.Categories.Queries.GetCategory;
using CatalogService.Application.Common.Models;

namespace CatalogService.Web.Endpoints;

public class Category : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetCategory, "{id}")
            .MapGet(GetCategoriesWithPagination);
        
        app.MapGroup(this)
            .MapPost(CreateCategory)
            .RequireAuthorization("ManagerRole")
            .MapPut(UpdateCategory, "{id}")
            .MapDelete(DeleteCategory, "{id}");

    }


    public Task<CategoryDto> GetCategory(ISender sender, [AsParameters] GetCategoryQuery query)
    {
        return sender.Send(query);
    }

    public Task<PaginatedList<CategoryDto>> GetCategoriesWithPagination(ISender sender, [AsParameters] GetCategoriesWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public Task<int> CreateCategory(ISender sender, CreateCategoryCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateCategory(ISender sender, int id, UpdateCategoryCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteCategory(ISender sender, int id)
    {
        await sender.Send(new DeleteCategoryCommand(id));
        return Results.NoContent();
    }
}
