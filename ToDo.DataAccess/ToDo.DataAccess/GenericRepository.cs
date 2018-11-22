using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.DataAccess.DataBase;

namespace ToDo.DataAccess
{
    public class GenericRepository : IDataRepository
    {
        private readonly ToDoDbContext _context;

        public GenericRepository(ToDoDbContext context)
        {
            _context = context;
            ToDoDbInitializer.Initialize(_context);
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);

            _context.SaveChanges();
        }

        public bool Delete<T>(T entity) where T : class
        {
            if (!_context.Set<T>().Contains(entity)) return false;

            _context.Set<T>().Remove(entity);
            _context.SaveChanges();

            return true;
        }

        public void Edit<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>(); 
        }
    }
}
