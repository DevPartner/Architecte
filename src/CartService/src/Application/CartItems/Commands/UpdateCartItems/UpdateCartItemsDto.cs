using CartService.Domain.ValueObjects;

namespace CartService.Application.CartItems.Commands.UpdateCartItems;

public class UpdateCartItemsDto
{
    public required string Name { get; init; }
    public required Money Price { get; init; }
    public int Id { get; set; }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateCartItemsDto, UpdateCartItemsCommand>()
                .ForMember(dest => dest.ProductKey, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
