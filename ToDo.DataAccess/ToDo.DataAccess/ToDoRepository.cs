using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.DataAccess.DataBase;
using ToDo.DataAccess.Models;

namespace ToDo.DataAccess
{
    public class ToDoRepository : IDataRepository
    {
        private readonly ToDoDbContext _context;

        ToDoRepository(ToDoDbContext newContext)
        {
            _context = newContext;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);

            _context.SaveChanges();
        }

        public bool Delete<TEntity>(TEntity entity) where TEntity : class
        {
            if (!_context.Set<TEntity>().Contains(entity)) return false;

            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();

            return true;
        }

        public void Edit<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>(); // TODO
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>(); 
        }
    }

    public interface IDataRepository
    {       
        void Add<TEntity>(TEntity entity) where TEntity : class;
        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;
        bool Delete<TEntity>(TEntity entity) where TEntity : class;
        void Edit<TEntity>(TEntity entity) where TEntity : class;
    }
}
