using AutoMapper;
using System.Linq;
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

            CreateMap<Item, ItemDTO>()
                .ForMember(i => i.Tags, opt => opt.MapFrom(item => item.TagItem.Select(t => Mapper.Map<Tag, TagDTO>(t.Tag))));
            CreateMap<Item, ItemDTO>();

            CreateMap<ItemDTO, Item>();
        }
    }
}
