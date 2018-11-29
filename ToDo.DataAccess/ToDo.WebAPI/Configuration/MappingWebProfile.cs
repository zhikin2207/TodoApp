using AutoMapper;
using System.Linq;
using ToDo.Services.DTOs;
using ToDo.Services.ViewModels;
using ToDo.WebAPI.ViewModels;

namespace ToDo.WebAPI.Configuration
{
    public class MappingWebProfile : Profile
    {
        public MappingWebProfile()
        {
            CreateMap<ItemCreateViewModel, ItemDTO>()
                .ReverseMap();

            CreateMap<ItemDTO, ItemDisplayViewModel>();

            CreateMap<StatisticDTO, StatisticViewModel>()
                .ReverseMap();
        }
    }
}
