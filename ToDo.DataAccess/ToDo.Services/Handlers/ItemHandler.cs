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
    }
}
