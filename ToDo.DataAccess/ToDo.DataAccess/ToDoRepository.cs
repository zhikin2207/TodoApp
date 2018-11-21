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

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories;
        }

        public IEnumerable<Item> GetItems()
        {
            return _context.Items;
        }

        public IEnumerable<TagItem> GetTags_Items()
        {
            return _context.TagsItems;
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
        IEnumerable<Category> GetCategories();
        IEnumerable<Tag> Tags();
        IEnumerable<TagItem> GetTagsItems();
        void Add<TEntity>(TEntity entity) where TEntity : class;
    }
}
