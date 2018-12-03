using AutoMapper;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories.CustomRepositories;
using ToDo.Services.Configuration;
using ToDo.Services.DTOs;
using ToDo.Services.Handlers.HandlerInterfaces;

namespace ToDo.Services.Handlers
{
    public class ItemHandler : IItemHandler
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;

        public ItemHandler(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository  = itemRepository;
            _mapper = mapper;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Create(ItemDTO itemDTO, IEnumerable<TagDTO> tagDTOs)
        {
            _logger.Info("Trying to add an item to repository.");

            var item = _mapper.Map<Item>(itemDTO);
            _logger.Debug("Created item without tags.");

            var tags = _mapper.Map<IEnumerable<Tag>>(tagDTOs);


            item.TagItem = tags.Select(t => new TagItem
            {
                Item = item,
                ItemId = item.Id,
                Tag = t,
                TagId = t.Id
            });

            _itemRepository.Add(item);
            _logger.Info("Item was added. Id {ItemId}", item.Id);
        }

        public void Delete(Guid id)
        {
            _logger.Info("Trying to delete an item from repository.");

            var itemToDelete = _itemRepository
                .GetItemsWithCategoryAndTags()
                .FirstOrDefault(i => i.Id == id);

            _itemRepository.Delete(itemToDelete);

            _logger.Info("Item was deleted {ItemId}", id);
        }

        public IEnumerable<ItemDTO> GetAll()
        {
            _logger.Info("Getting all of items form the repository.");

            var items = _itemRepository
               .GetItemsWithCategoryAndTags()
               .Select(_mapper.Map<Item, ItemDTO>);

            _logger.Info("All items were gotten.");

            return items;
        }

        public IEnumerable<ItemDTO> Search(string category, string[] tags)
        {
            _logger.Info("Searching in the repository for {category} and {tags}.", category, tags);

            return _itemRepository
               .GetItemsWithCategoryAndTags()
               .Where(i => string.Equals(
                   i.Category.Name,
                   category,
                   StringComparison.CurrentCultureIgnoreCase))
               .Where(i => i.TagItem
                   .Select(ti => ti.Tag.Name)
                   .Intersect(tags)
                   .Any())
               .Select(_mapper.Map<ItemDTO>);
        }

        public StatisticDTO GetAdultItems()
        {
            _logger.Info("Getting adult items from the repository.");

            var priorityCounts = Enum
                .GetValues(typeof(Priority))
                .Cast<Priority>()
                .Select(v => v.ToString())
                .ToDictionary(k => k, v => 0);

            var suitableItems = _itemRepository
                .GetItemsWithCategoryAndTags()
                .Where(IsItemAdult)
                .OrderBy(i => i.DueDate)
                .Select(_mapper.Map<Item, ItemDTO>);

            _logger.Debug("Items were gotten.");

            foreach (var item in suitableItems)
            {
                var key = item.Priority.ToString();

                priorityCounts[key]++;
            }

            _logger.Debug("Priorities were counted.");

            var statistic = new StatisticDTO
            {
                Items = suitableItems,
                PriorityCounts = priorityCounts
            };

            _logger.Info("Adult items were gotten.");

            return statistic;
        }

        public bool IsItemAdult(Item i)
        {
            var result = (ForbiddenItems.Titles.Contains(i.Title)
                || ForbiddenItems.Categories.Contains(i.Category.Name)
                || ForbiddenItems.DescriptionWords
                    .Select(w => i.Description.Contains(w))
                    .Any(b => b == true))
                && !i.Status;

            _logger.Debug("Item is adult. Id: {ItemId}", i.Id);

            return result;
        }
    }
}
