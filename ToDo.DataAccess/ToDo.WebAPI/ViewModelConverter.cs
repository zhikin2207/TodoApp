using System.Linq;
using ToDo.Services.DTOs;
using ToDo.Services.ViewModels;
using ToDo.WebAPI.ViewModels;

namespace ToDo.WebAPI
{
    public  class ViewModelConverter
    {
        public static StatisticViewModel ConvertToStatisticViewModel(StatisticDTO item)
        {
            return new StatisticViewModel
            {
                Items = item.Items,
                PriorityCounts = item.PriorityCounts
            };
        }

        public static ItemDisplayViewModel ConvertToItemDisplayViewModel(ItemDTO item)
        {
            return new ItemDisplayViewModel
            {
                Title = item.Title,
                Category = item.Category.Name,
                Id = item.Id,
                Description = item.Description,
                DueDate = item.DueDate,
                Priority = item.Priority,
                Status = item.Status,
                TagNames = item.Tags.Select(t => t.Name)
            };
        }

        public static ItemCreateViewModel ConvertToItemCreateViewModel(ItemDTO item)
        {
            return new ItemCreateViewModel
            {
                Title = item.Title,
                Category = item.Category,
                Id = item.Id,
                Description = item.Description,
                DueDate = item.DueDate,
                Priority = item.Priority,
                Status = item.Status,
                Tags = item.Tags
            };
        }

        public static ItemDTO ConvertToItemDTO(ItemCreateViewModel item)
        {
            return new ItemDTO
            {
                Title = item.Title,
                Category = item.Category,
                Id = item.Id,
                Description = item.Description,
                DueDate = item.DueDate,
                Priority = item.Priority,
                Status = item.Status,
                Tags = item.Tags
            };
        }
    }
}
