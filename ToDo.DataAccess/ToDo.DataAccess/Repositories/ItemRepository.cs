using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.DataAccess.DataBase;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories.CustomRepositories;
using Microsoft.EntityFrameworkCore;

namespace ToDo.DataAccess.Repositories
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(ToDoDbContext context) : base(context)
        {
        }

        public IEnumerable<Item> GetItemsWithCategoryAndTags()
        {
            _logger.Info("Getting items with category and tags from the db");

            return _context.Set<Item>()
                .Include(i => i.Category)
                .Include(i => i.TagItem)
                .ThenInclude(ti => ti.Tag);
        }

        public Item GetById(Guid id)
        {
            _logger.Info("Looking in the db for item {ItemId}", id);

            return GetAll().FirstOrDefault(i => i.Id == id);
        }
    }
}
