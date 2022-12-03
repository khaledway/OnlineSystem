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
                .ForMember(dest => dest.CategoryName,opt => opt.MapFrom(src => (src.Category!=null && src.Category.Name !=null?src.Category.Name:null)));

        }
    }
}