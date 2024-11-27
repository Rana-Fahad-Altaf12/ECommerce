using AutoMapper;
using Ecommerce.DAL.Entities.Products;
using Ecommerce.Model.DTOs.Products;

namespace Ecommerce.Service.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
            .ForMember(dest => dest.SubcategoryId, opt => opt.MapFrom(src => src.Subcategory.Id));
            CreateMap<Category, CategoryDto>();
            CreateMap<Subcategory, SubcategoryDto>();
        }
    }
}
