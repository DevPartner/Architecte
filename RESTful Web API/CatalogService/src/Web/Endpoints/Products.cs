using AutoMapper;
using CatalogService.Application.Common.Models;
using CatalogService.Domain.Entities;
using CatalogService.Web.Helpers;
using CatalogService.Web.Models;
using CleanArchitecture.Application.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Products.Commands.DeleteProduct;
using CleanArchitecture.Application.Products.Commands.UpdateProduct;
using CleanArchitecture.Application.Products.Queries.GetProduct;
using CleanArchitecture.Application.Products.Queries.GetProductsWithPagination;
using Microsoft.AspNetCore.Routing;
namespace CatalogService.Web.Endpoints;

public class Products : EndpointGroupBase
{
    /*private readonly IMapper _mapper;
    private readonly ILinkGenerator _linkGenerator;

    public Products(IMapper mapper, ILinkGenerator linkGenerator)
    {
        _mapper = mapper;
        _linkGenerator = linkGenerator;
    }*/
    /*
    public static void MapEndpoints(WebApplication app)
    {
        app.Lifetime.ApplicationStarted.Register(() =>
        {
            using (var scope = app.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;

                var mapper = scopedServices.GetRequiredService<IMapper>();
                var linkGenerator = scopedServices.GetRequiredService<ILinkGenerator>();

                var products = new Products(mapper, linkGenerator);

                app.MapGroup(products)
                    .MapGet(products.GetProduct, "{id}")
                    .MapGet(products.GetProductsWithPagination)
                    .MapPost(products.CreateProduct)
                    .MapPut(products.UpdateProduct, "{id}")
                    .MapDelete(products.DeleteProduct, "{id}");
            }
        });
    }
    */
    public override void Map(WebApplication app)
    {

        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetProduct, "{id}")
            .MapGet(GetProductsWithPagination)
            .MapPost(CreateProduct)
            .MapPut(UpdateProduct, "{id}")
            .MapDelete(DeleteProduct, "{id}");
    }

    public async Task<IResult> GetProduct(HttpContext context, ISender sender, [AsParameters] GetProductQuery query)
    {
        var product = await sender.Send(query);
        if (product == null)
        {
            return Results.NotFound();
        }
        
        var linkGenerator = context.RequestServices.GetRequiredService<ILinkGenerator>();
        var mapper = context.RequestServices.GetRequiredService<IMapper>();
        var responseModel = mapper.Map<ProductHaleosDto>(product);
        var basePath = GetBasePath(context.Request.Path.Value!, query.Id);
        var links = linkGenerator.GetLinks(basePath, query.Id);
        responseModel.Links = links;

        return Results.Ok(responseModel);
    }
    private string GetBasePath(string path, int id)
    {
        // Check if path ends with the ID and remove it
        if (path.EndsWith($"/{id}"))
        {
            path = path.Substring(0, path.LastIndexOf($"/{id}"));
        }

        // Ensure the path does not end with a slash
        return path.TrimEnd('/');
    }

    public async Task<IResult> GetProductsWithPagination(HttpContext context, ISender sender, [AsParameters] GetProductsWithPaginationQuery query)
    {
        var products = await sender.Send(query);

        var linkGenerator = context.RequestServices.GetRequiredService<ILinkGenerator>();
        var mapper = context.RequestServices.GetRequiredService<IMapper>();
        var basePath = context.Request.Path.Value!; // Use base path for pagination links
        var productHaleos = products.Items.Select(x =>
            {
                var p = mapper.Map<ProductHaleosDto>(x);
                p.Links = linkGenerator.GetLinks(basePath, p.Id);
                return p;
            }).ToList();


        var paginationLinks = linkGenerator.GetPaginationLinks(basePath, query.PageNumber, query.PageSize, products.TotalPages);

        var response = new PaginatedHateosList<ProductHaleosDto>(productHaleos, paginationLinks, products.TotalCount, products.PageNumber, query.PageSize);

        return Results.Ok(response);
    }

    public async Task<IResult> CreateProduct(ISender sender, CreateProductCommand command)
    {
        var productId = await sender.Send(command);
        return Results.Created($"/api/products/{productId}", productId);
    }

    public async Task<IResult> UpdateProduct(ISender sender, int id, UpdateProductCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteProduct(ISender sender, int id)
    {
        await sender.Send(new DeleteProductCommand(id));
        return Results.NoContent();
    }
}
