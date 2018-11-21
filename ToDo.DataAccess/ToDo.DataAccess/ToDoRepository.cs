using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.DataAccess.DataBase;
using ToDo.DataAccess.Models;

namespace ToDo.DataAccess
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoDbContext _context;

        ToDoRepository(ToDoDbContext newContext)
        {
            _context = newContext;
        }

        public IEnumerable<ItemCategory> GetCategories()
        {
            return _context.Categories;
        }

        public IEnumerable<Item> GetItems()
        {
            return _context.Items;

        }

        public IEnumerable<Tag_Item> GetTags_Items()
        {
            return _context.Tags_Items;

        }

        public IEnumerable<Tag> Tags()
        {
            return _context.Tags;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {

            _context.Set<TEntity>().Add(entity);

            _context.SaveChanges();

        }
    }

    public interface IToDoRepository
    {
        IEnumerable<Item> GetItems();
        IEnumerable<ItemCategory> GetCategories();
        IEnumerable<Tag> Tags();
        IEnumerable<Tag_Item> GetTags_Items();
        void Add<TEntity>(TEntity entity) where TEntity : class;
    }
}
