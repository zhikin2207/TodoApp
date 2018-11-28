using AutoMapper;
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
        private readonly HandlerConverter _converter;
        private readonly IItemRepository _itemRepository;

        public ItemHandler(IItemRepository itemRepository)
        {
            _itemRepository  = itemRepository;
            _converter = new HandlerConverter();
        }

        public void Create(ItemDTO value, IEnumerable<TagDTO> tags)
        {
            var itemToCreate = _converter.ConvertToItem(value, tags);

            _itemRepository.Add(itemToCreate);
        }

        public void Delete(Guid id)
        {
            var itemToDelete = _itemRepository
                    .GetAll()
                    .Where(i => i.Id == id)
                    .FirstOrDefault();

            _itemRepository.Delete(itemToDelete);
        }

        public IEnumerable<ItemDTO> GetAll()
        {
            return _itemRepository
              .GetAll()
              .Select(_converter.ConvertToItemDTO);
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
               .Select(_converter.ConvertToItemDTO);
        }

        public StatisticDTO GetAdultItems()
        {
            Dictionary<string, int> newPriorityCounts = new Dictionary<string, int>();
            var listOfPriorities = Enum.GetValues(typeof(Priority))
                                        .Cast<Priority>()
                                        .Select(v => v.ToString());

            foreach (var priority in listOfPriorities)                                        
            {
                newPriorityCounts.Add(priority, 0);
            }

            var suitableItems = _itemRepository
                    .GetAll()
                    .Where(IsItemAdult)
                    .OrderBy(i => i.DueDate)
                    .Select(_converter.ConvertToItemDTO);

            foreach(var item in suitableItems)
            {
                var key = item.Priority.ToString();

                newPriorityCounts[key]++;
            }

            return new StatisticDTO
            {
                Items = suitableItems,
                PriorityCounts = newPriorityCounts
            };
        }

        public bool IsItemAdult(Item i)
        {
            return (i.Title.Contains("xxx")
                            || i.Title.Contains("adult")
                            || string.Equals(
                               i.Category.Name,
                               "adult",
                               StringComparison.CurrentCultureIgnoreCase)
                            || i.Description.Contains("xxx"))
                            && !i.Status;
        }
    }
}
