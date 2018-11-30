using System;
using System.Collections.Generic;
using System.Text;
using ToDo.DataAccess.Models;

namespace ToDo.DataAccess.Repositories.CustomRepositories
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        IEnumerable<Item> GetItemsWithCategoryAndTags();
    }
}
