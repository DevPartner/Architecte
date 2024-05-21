using CartService.Application.Common.Interfaces;
using CartService.Application.Common.Mappings;
using CartService.Application.Common.Models;
using CartService.Domain.Entities;

namespace CartService.Application.CartItems.Queries.GetItemsWithPagination;

public record GetCartItemsWithPaginationQuery : IRequest<PaginatedList<CartItemDto>>
{
    public int CartId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCartItemsWithPaginationQueryHandler : IRequestHandler<GetCartItemsWithPaginationQuery, PaginatedList<CartItemDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IRepository<CartItem> _repository;
    public GetCartItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper, IRepository<CartItem> repository)
    {
        _context = context;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<PaginatedList<CartItemDto>> Handle(GetCartItemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var paginatedList = await _repository.GetPagedAsync(request.PageNumber, request.PageSize, x => x.CartId == request.CartId);

        return paginatedList.ProjectToListAsync<CartItemDto, CartItem>(_mapper, request.PageSize);
    }
}
