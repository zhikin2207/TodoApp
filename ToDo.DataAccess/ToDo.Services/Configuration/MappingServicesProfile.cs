using AutoMapper;
using ToDo.DataAccess.Models;
using ToDo.Services.DTOs;

namespace ToDo.Services.Configuration
{
    public class MappingServicesProfile : Profile
    {
        public MappingServicesProfile()
        {
            CreateMap<CategoryDTO, Category>()
                               .ForMember<Category>(c => c.Parent, opt => opt.MapFrom(c =>
                        (c.Parent == null) ? null
                        : Mapper.Map<CategoryDTO, Category>(c.Parent)))
                 .ReverseMap();

            CreateMap<TagDTO, Tag>()
             .ReverseMap();

            CreateMap<ItemDTO, Item>()
                .ReverseMap();
        }
    }
}
