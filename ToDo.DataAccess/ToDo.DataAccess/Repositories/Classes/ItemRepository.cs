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
    public class ItemRepository : IItemRepository
    {
        private readonly ToDoDbContext _context;

        public ItemRepository(ToDoDbContext context)
        {
            _context = context;
        }

        public void Add(Item entity)
        {
            _context.Items.Add(entity);

            _context.SaveChanges();
        }

        public bool Delete(Item entity)
        {
            if (!_context.Items.Contains(entity)) return false;

            _context.Items.Remove(entity);
            _context.SaveChanges();

            return true;
        }

        public void Edit(Item entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetAll()
        {
            return _context.Items;
        }

        public Item GetById(Guid guid)
        {
             return _context.Items.FirstOrDefault(x => x.Id == guid);
        }
    }
}
