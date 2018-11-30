using AutoMapper;
using ToDo.DataAccess.Models;
using ToDo.Services.DTOs;

namespace ToDo.Services.Configuration
{
    public class MappingServicesProfile : Profile
    {
        public MappingServicesProfile()
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<TagDTO, Tag>().ReverseMap();
            CreateMap<ItemDTO, Item>().ReverseMap();
        }
    }
}
