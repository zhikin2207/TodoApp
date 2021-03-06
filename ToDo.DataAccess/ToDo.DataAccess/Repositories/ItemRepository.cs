﻿using System;
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
            return _context.Set<Item>()
                .Include(i => i.Category)
                .Include(i => i.TagItem)
                .ThenInclude(ti => ti.Tag);
        }

        public Item GetById(Guid id)
        {
            return GetAll().FirstOrDefault(i => i.Id == id);
        }
    }
}
