using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.DataAccess.DataBase;

namespace ToDo.DataAccess
{
    public class GenericRepository<T> : IDataRepository<T> where T : class
    {
        private readonly ToDoDbContext _context;

        public GenericRepository(ToDoDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);

            _context.SaveChanges();
        }

        public bool Delete(T entity)
        {
            if (!_context.Set<T>().Contains(entity)) return false;

            _context.Set<T>().Remove(entity);
            _context.SaveChanges();

            return true;
        }

        public void Edit(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>(); 
        }
    }
}
