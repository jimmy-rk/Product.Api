using AutoMapper;
using Product.Api.Models.General;

namespace Product.Api.Services.MappingProfiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Cqrs.Product.GetProductGroupTypes.Response, SelectListItem>(MemberList.None)
            .ForPath(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
            .ForPath(dest => dest.Text, opt => opt.MapFrom(src => src.Text));
        }
    }
}
