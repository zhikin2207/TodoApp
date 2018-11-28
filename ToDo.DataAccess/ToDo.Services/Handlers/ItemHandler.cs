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

        public ItemHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public void Create(ItemDTO value)
        {
            var itemToCreate = HandlerConverter.ConvertToItem(value);

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
              .Select(HandlerConverter.ConvertToItemDTO);
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
               .Select(HandlerConverter.ConvertToItemDTO);
        }

        public StatisticDTO GetAdultItems()
        {
            Dictionary<string, int> newPriorityCounts = new Dictionary<string, int>();
            var listOfPriorities = Enum.GetValues(typeof(Priority))
                                        .Cast<Priority>()
                                        .Select(v => v.ToString());

            foreach (var p in listOfPriorities)
                                        
            {
                newPriorityCounts.Add(p, 0);
            }

            Func<Item, bool> condition = (i => (i.Title.Contains("xxx")
                            || i.Title.Contains("adult")
                            || string.Equals(
                               i.Category.Name,
                               "adult",
                               StringComparison.CurrentCultureIgnoreCase)
                            || i.Description.Contains("xxx"))
                            && !i.Status);

            var suitableItems = _itemRepository
                    .GetAll()
                    .Where(condition)
                    .OrderBy(i => i.DueDate)
                    .Select(HandlerConverter.ConvertToItemDTO);

            foreach(var i in suitableItems)
            {
                var key = i.Priority.ToString();

                newPriorityCounts[key]++;
            }

            return new StatisticDTO
            {
                Items = suitableItems,
                PriorityCounts = newPriorityCounts
            };
        }
    }
}
