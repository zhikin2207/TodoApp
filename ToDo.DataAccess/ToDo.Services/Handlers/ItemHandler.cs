using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories.CustomRepositories;
using ToDo.Services.DTOs;
using ToDo.Services.Handlers.HandlerInterfaces;

namespace ToDo.Services.Handlers
{
    public class ItemHandler : IItemHandler
    {
        private readonly IItemRepository _itemRepository;

        private ItemDTO ConvertToItemDisplayViewModel(Item item)
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
                TagItem = item.TagItem
            };
        }

        private Item ConvertToItem(ItemDTO item)
        {
            return new Item
            {
                Title = item.Title,
                Category = item.Category,
                Id = item.Id,
                Description = item.Description,
                DueDate = item.DueDate,
                Priority = item.Priority,
                Status = item.Status,
                TagItem = item.TagItem
            };
        }

        public ItemHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public void Create(ItemDTO value)
        {
            _itemRepository.Add(ConvertToItem(value));
        }

        public void Delete(Guid id)
        {
            _itemRepository
                .Delete(
                    _itemRepository
                    .GetAll()
                    .Where(i => i.Id == id)
                    .FirstOrDefault());
        }

        public IEnumerable<ItemDTO> GetAll()
        {
            return _itemRepository
              .GetAll()
              .Select(ConvertToItemDisplayViewModel);
        }

        public IEnumerable<ItemDTO> Search(string category, string[] tags)
        {
            return _itemRepository
               .GetAll()
               .Where(i => string.Equals(
                   i.Category.Name,
                   category,
                   StringComparison.CurrentCultureIgnoreCase))
               .Where(i => i.TagItem
                   .Select(ti => ti.Tag.Name)
                   .Intersect(tags)
                   .Any())
               .Select(ConvertToItemDisplayViewModel);
        }

        public StatisticDTO GetAdultItems()
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();

            foreach(var p in Enum.GetValues(typeof(Priority))
                                        .Cast<Priority>()
                                        .Select(v => v.ToString()))
                                        
            {
                counts.Add(p, 0);
            }

            var suitableItems = _itemRepository
                    .GetAll()
                    .Where(i => i.Title.Contains("xxx")
                            || i.Title.Contains("adult")
                            || string.Equals(
                               i.Category.Name,
                               "adult",
                               StringComparison.CurrentCultureIgnoreCase)
                            || i.Description.Contains("xxx"))
                    .Where(i => i.Status == false)
                    .OrderBy(i => i.DueDate)
                    .Select(ConvertToItemDisplayViewModel);

            foreach(var i in suitableItems)
            {
                counts[i.Priority.ToString()]++;
            }

            return new StatisticDTO
            {
                items = suitableItems,
                priorityCounts = counts
            };
        }
    }
}
