using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories;
using ToDo.Services.ViewModels;
using ToDo.WebAPI.ViewModels;

namespace ToDo.Services.Handlers
{
    public interface IItemHandler
    {
        List<ItemDisplayViewModel> GetAll();
        IEnumerable<ItemDisplayViewModel> Search(string category, string[] tags);
        void Create(ItemCreateViewModel value);
        void Delete(Guid id);
    }

    public class ItemHandler : IItemHandler
    {
        private readonly IItemRepository _itemRepository;

        public ItemHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        private ItemDisplayViewModel ConvertToItemDisplayViewModel(Item item)
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
                TagNames = item.TagItem.Select(t => t.Tag.Name)
            };
        }

        private Item ConvertToItem(ItemCreateViewModel item)
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

        public void Create(ItemCreateViewModel value)
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

        public List<ItemDisplayViewModel> GetAll()
        {
            return _itemRepository
              .GetAll()
              .Select(ConvertToItemDisplayViewModel)
              .ToList();
        }

        public IEnumerable<ItemDisplayViewModel> Search(string category, string[] tags)
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
               .Select(ConvertToItemDisplayViewModel)
               .ToList();
        }

    }
}
