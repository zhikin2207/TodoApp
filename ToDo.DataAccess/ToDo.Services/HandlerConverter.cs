using System.Collections.Generic;
using System.Linq;
using ToDo.DataAccess.Models;
using ToDo.Services.DTOs;

namespace ToDo.Services
{
    class HandlerConverter
    {
        public static CategoryDTO ConvertToCategoryDTO(Category category)
        {            
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Parent = (category.Parent == null)? null
                    : ConvertToCategoryDTO(category.Parent)
            };
        }

        public static Category ConvertToCategory(CategoryDTO category)
        {
            return new Category
            {
                Id = category.Id,
                Name = category.Name,
                Parent = (category.Parent == null) ? null
                   : ConvertToCategory(category.Parent)
            };
        }

        public static ItemDTO ConvertToItemDTO(Item item)
        {
            return new ItemDTO
            {
                Title = item.Title,
                Category = ConvertToCategoryDTO(item.Category),
                Id = item.Id,
                Description = item.Description,
                DueDate = item.DueDate,
                Priority = (PriorityDTO)item.Priority,
                Status = item.Status,
                Tags = item.TagItem.Select(t => ConvertToTagDTO(t.Tag))
            };
        }

        public static Item ConvertToItem(ItemDTO item, IEnumerable<TagDTO> tags)
        {
            var itemToReturn = new Item
            {
                Title = item.Title,
                Category = ConvertToCategory(item.Category),
                Id = item.Id,
                Description = item.Description,
                DueDate = item.DueDate,
                Priority = (Priority)item.Priority,
                Status = item.Status
            };

            var tagItem = new List<TagItem>();

            foreach(var tag in tags)
            {
                tagItem.Add(
                    new TagItem {
                        Item = itemToReturn,
                        ItemId = itemToReturn.Id,
                        Tag = ConvertToTag(tag, itemToReturn),
                        TagId = tag.Id}
                    );
            }

            itemToReturn.TagItem = tagItem;

            return itemToReturn;
        }

        private static TagDTO ConvertToTagDTO(Tag tag)
        {
            return new TagDTO
            {
                Id = tag.Id,
                Name = tag.Name,
                Color = tag.Color,
               Items = tag.TagItem.Select(t => ConvertToItemDTO(t.Item))
            };
        }

        private static Tag ConvertToTag(TagDTO tag, Item owner)
        {
            var tagToReturn = new Tag
            {
                Id = tag.Id,
                Name = tag.Name,
                Color = tag.Color
            };

            var tagItem = new List<TagItem>();

            tagItem.Add(
                new TagItem {
                    Item = owner,
                    ItemId = owner.Id,
                    Tag = tagToReturn,
                    TagId = tagToReturn.Id }
                );

            tagToReturn.TagItem = tagItem;

            return tagToReturn;
        }
    }
}
