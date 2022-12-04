using AutoMapper;
using OnlineSystem.Infrastructure.DataBase.Models;
using OnlineSystem.Service.ViewModels;

namespace OnlineSystem
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
                 CreateMap<Product , ProductViewModel>()
                .ForMember(dest => dest.CategoryName,opt => opt.MapFrom(src => (src.Category!=null && src.Category.Name !=null?src.Category.Name:null)))
                .ForMember(dest => dest.ParentCategoryName, opt => opt.MapFrom(src => 
                
                
                (
                src.Category != null && src.Category.ParentCategory != null && src.Category.ParentCategory.Name != null ? 
               
                src.Category.ParentCategory.Name : null)
                
                
                )
                
                )


                .ReverseMap();

            CreateMap<ProductViewModel, Product >();

            CreateMap<Category, CategoryViewModel>().ReverseMap();
            
        }
    }
}