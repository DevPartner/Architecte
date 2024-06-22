using CartService.Domain.Entities;

namespace CartService.Application.Common.Models;

public class LookupDto
{
    public int Id { get; init; }

    public string? Name { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CartItem, LookupDto>();
        }
    }
}
