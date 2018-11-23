using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.DataBase;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories.Interfaces;

namespace ToDo.DataAccess.Repositories.Classes
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ToDoDbContext _context;

        public CategoryRepository(ToDoDbContext context)
        {
            _context = context;
        }

        public void Add(Category entity)
        {
            _context.Categories.Add(entity);

            _context.SaveChanges();
        }

        public bool Delete(Category entity)
        {
            if (!_context.Categories.Contains(entity)) return false;

            _context.Categories.Remove(entity);
            _context.SaveChanges();

            return true;
        }

        public void Edit(Category entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories;
        }

        public Category GetById(Guid guid)
        {
            return _context.Categories.FirstOrDefault(x => x.Id == guid);
        }
    }
}
