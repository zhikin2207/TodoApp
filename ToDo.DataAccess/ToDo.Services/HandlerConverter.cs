using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.DataAccess.Models;
using ToDo.Services.DTOs;

namespace ToDo.Services
{
    class HandlerConverter
    {
        public HandlerConverter()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CategoryDTO, Category>()
                .ForMember<Guid>(c => c.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember<string>(c => c.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember<Category>(c => c.Parent, opt => opt.MapFrom(c =>
                        (c.Parent == null) ? null
                        : Mapper.Map<CategoryDTO, Category>(c.Parent)))
                 .ReverseMap();

                cfg.CreateMap<TagDTO, Tag>()
                .ForMember<Guid>(t => t.Id, opt => opt.MapFrom(t => t.Id))
                .ForMember<string>(t => t.Name, opt => opt.MapFrom(t => t.Name))
                .ForMember<string>(t => t.Color, opt => opt.MapFrom(t => t.Color))
                .ReverseMap();

                cfg.CreateMap<ItemDTO, Item>()
                .ForMember<Guid>(c => c.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember<string>(c => c.Title, opt => opt.MapFrom(c => c.Title))
                .ForMember<string>(c => c.Description, opt => opt.MapFrom(c => c.Description))
                .ForMember<DateTime>(c => c.DueDate, opt => opt.MapFrom(c => c.DueDate))
                .ForMember<Priority>(c => c.Priority, opt => opt.MapFrom(c => (int)c.Priority))
                .ForMember<bool>(c => c.Status, opt => opt.MapFrom(c => c.Status))
                .ReverseMap();
            });
        }

        public CategoryDTO ConvertToCategoryDTO(Category category)
        {
            return Mapper.Map<Category, CategoryDTO>(category); 
        }

        public Category ConvertToCategory(CategoryDTO category)
        {
            return Mapper.Map<CategoryDTO, Category>(category);
        }

        public ItemDTO ConvertToItemDTO(Item item)
        {
            var itemToReturn = Mapper.Map<Item, ItemDTO>(item);

            itemToReturn.Tags = item.TagItem.Select(t => ConvertToTagDTO(t.Tag));

            return itemToReturn;
        }

        public Item ConvertToItem(ItemDTO item, IEnumerable<TagDTO> tags)
        {
            var itemToReturn = Mapper.Map<ItemDTO, Item>(item);

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
            return Mapper.Map<Tag, TagDTO>(tag);
        }

        private static Tag ConvertToTag(TagDTO tag, Item owner)
        {
            var tagToReturn = Mapper.Map<TagDTO, Tag>(tag);

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
