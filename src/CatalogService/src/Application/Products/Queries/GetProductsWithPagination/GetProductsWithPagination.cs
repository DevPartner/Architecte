using System.Net.Http;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Application.Common.Mappings;
using CatalogService.Application.Common.Models;
using CatalogService.Domain.Entities;
using CleanArchitecture.Application.Products.Queries.GetProduct;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CleanArchitecture.Application.Products.Queries.GetProductsWithPagination;

public record GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<ProductDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;


    public GetProductsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProductDto>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .OrderBy(x => x.Name)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        /*


        var productDtos = products.Items.Select(p => _mapper.Map<ProductHateosDto>(p)).ToList();
        foreach (var productDto in productDtos)
        {
            var links = _linkGenerator.GetProductLinks(httpContext, productDto.Id);
            productDto.Links = links;
        }
        var paginationLinks = _linkGenerator.GetProductPaginationLinks(httpContext, products.PageNumber, query.PageSize, products.TotalPages);
        var paginatedList = new PaginatedHateosList<ProductHateosDto>(productDtos, products.TotalCount, products.PageNumber, query.PageSize)
        {
            Links = paginationLinks
        };
        */
    }
}
