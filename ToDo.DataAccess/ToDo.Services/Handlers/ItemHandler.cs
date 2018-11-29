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
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;

        public ItemHandler(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository  = itemRepository;
            _mapper = mapper;
        }

        public void Create(ItemDTO itemDTO, IEnumerable<TagDTO> tagDTOs)
        {
            var item = _mapper.Map<ItemDTO, Item>(itemDTO);
            var tags = _mapper.Map<IEnumerable<TagDTO>, IEnumerable<Tag>>(tagDTOs);
            var tagItem = new List<TagItem>();

            foreach (var tag in tags)
            {
                tagItem.Add(
                    new TagItem
                    {
                        Item = item,
                        ItemId = item.Id,
                        Tag = tag,
                        TagId = tag.Id
                    }
                    );
            }

            item.TagItem = tagItem;

            _itemRepository.Add(item);
        }

        public void Delete(Guid id)
        {
            var itemToDelete = _itemRepository
                    .GetItemsWithCategoryAndTags()
                    .Where(i => i.Id == id)
                    .FirstOrDefault();

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
               .Select(_mapper.Map<Item, ItemDTO>);
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
                    .GetItemsWithCategoryAndTags()
                    .Where(IsItemAdult)
                    .OrderBy(i => i.DueDate)
                    .Select(_mapper.Map<Item, ItemDTO>);

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
