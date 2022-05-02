using AutoMapper;
using Product.Api.Models.General;
using Product.Api.Models.Product;

namespace Product.Api.Services.MappingProfiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Cqrs.Product.GetProductGroupTypes.Response, SelectListItem>(MemberList.None)
            .ForPath(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
            .ForPath(dest => dest.Text, opt => opt.MapFrom(src => src.Text));

            CreateMap<Cqrs.Product.GetByGuid.Response, ProductViewModel>(MemberList.None)
            .ForPath(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
            .ForPath(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForPath(dest => dest.ProductGroupNk, opt => opt.MapFrom(src => src.ProductGroupNk))
            .ForPath(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForPath(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));


            CreateMap<ProductViewModel, Cqrs.Product.Create.Command>(MemberList.None)
            .ForPath(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForPath(dest => dest.ProductGroupNk, opt => opt.MapFrom(src => src.ProductGroupNk))
            .ForPath(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForPath(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<ProductViewModel, Cqrs.Product.Update.Command>(MemberList.None)
            .ForPath(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
            .ForPath(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForPath(dest => dest.ProductGroupNk, opt => opt.MapFrom(src => src.ProductGroupNk))
            .ForPath(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForPath(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<Cqrs.Product.GetProducts.Response, ProductViewModel>(MemberList.None)
            .ForPath(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
            .ForPath(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForPath(dest => dest.ProductGroupNk, opt => opt.MapFrom(src => src.ProductGroupNk))
            .ForPath(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForPath(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        }
    }
}
