using System;
using System.Collections.Generic;
using ToDo.Services.DTOs;

namespace ToDo.Services.Handlers.HandlerInterfaces
{
    public interface IItemHandler
    {
        IEnumerable<ItemDTO> GetAll();
        IEnumerable<ItemDTO> Search(string category, string[] tags);
        void Create(ItemDTO value, IEnumerable<TagDTO> tags);
        void Delete(Guid id);
        StatisticDTO GetAdultItems();
    }
}
