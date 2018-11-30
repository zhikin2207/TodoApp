using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;

        public ItemHandler(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository  = itemRepository;
            _mapper = mapper;
        }

        public void Create(ItemDTO itemDTO, IEnumerable<TagDTO> tagDTOs)
        {
            var item = _mapper.Map<Item>(itemDTO);
            var tags = _mapper.Map<IEnumerable<Tag>>(tagDTOs);

            item.TagItem = tags.Select(t => new TagItem
            {
                Item = item,
                ItemId = item.Id,
                Tag = t,
                TagId = t.Id
            });

            _itemRepository.Add(item);
        }

        public void Delete(Guid id)
        {
            var itemToDelete = _itemRepository
                .GetItemsWithCategoryAndTags()
                .FirstOrDefault(i => i.Id == id);

            _itemRepository.Delete(itemToDelete);
        }

        public IEnumerable<ItemDTO> GetAll()
        {
            return _itemRepository
               .GetItemsWithCategoryAndTags()
               .Select(_mapper.Map<Item, ItemDTO>);
        }

        public IEnumerable<ItemDTO> Search(string category, string[] tags)
        {
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
            var priorityCounts = Enum
                .GetValues(typeof(Priority))
                .Cast<Priority>()
                .Select(v => v.ToString())
                .ToDictionary(k => k, v => 0);

            var suitableItems = _itemRepository
                .GetItemsWithCategoryAndTags()
                .Where(IsItemAdult)
                .OrderBy(i => i.DueDate)
                .Select(_mapper.Map<Item, ItemDTO>).ToList();

            foreach(var item in suitableItems)
            {
                var key = item.Priority.ToString();

                priorityCounts[key]++;
            }

            return new StatisticDTO
            {
                Items = suitableItems,
                PriorityCounts = priorityCounts
            };
        }

        public bool IsItemAdult(Item i)
        {
            var result = (ForbiddenItems.Titles.Contains(i.Title)
                || ForbiddenItems.Categories.Contains(i.Category.Name)
                || ForbiddenItems.DescriptionWords
                    .Select(w => i.Description.Contains(w))
                    .Any(b => b == true))
                && !i.Status;

            return result;
        }
    }
}
