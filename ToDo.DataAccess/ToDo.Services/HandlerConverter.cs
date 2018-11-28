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
                Priority = item.Priority,
                Status = item.Status,
                TagItem = item.TagItem.Select(ConvertToTagItemDTO)
            };
        }

        public static TagItemDTO ConvertToTagItemDTO(TagItem tagItem)
        {
            return new TagItemDTO
            {
                Item = ConvertToItemDTO(tagItem.Item),
                ItemId = tagItem.ItemId,
                Tag = ConvertToTagDTO(tagItem.Tag),
                TagId = tagItem.TagId
            };
        }

        public static TagItem ConvertToTagItem(TagItemDTO tagItem)
        {
            return new TagItem
            {
                Item = ConvertToItem(tagItem.Item),
                ItemId = tagItem.ItemId,
                Tag = ConvertToTag(tagItem.Tag),
                TagId = tagItem.TagId
            };
        }

        private static TagDTO ConvertToTagDTO(Tag tag)
        {
            return new TagDTO
            {
                Id = tag.Id,
                Name = tag.Name,
                Color = tag.Color,
                TagItem = tag.TagItem.Select(ConvertToTagItemDTO)
            };
        }

        private static Tag ConvertToTag(TagDTO tag)
        {
            return new Tag
            {
                Id = tag.Id,
                Name = tag.Name,
                Color = tag.Color,
                TagItem = tag.TagItem.Select(ConvertToTagItem)
            };
        }

        public static Item ConvertToItem(ItemDTO item)
        {
            return new Item
            {
                Title = item.Title,
                Category = ConvertToCategory(item.Category),
                Id = item.Id,
                Description = item.Description,
                DueDate = item.DueDate,
                Priority = item.Priority,
                Status = item.Status,
                TagItem = item.TagItem.Select(ConvertToTagItem)
            };
        }
    }
}
