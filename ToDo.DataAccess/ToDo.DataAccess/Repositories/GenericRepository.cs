using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.DataAccess.DataBase;
using ToDo.DataAccess.Models;

namespace ToDo.DataAccess
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ToDoDbContext _context;
        protected readonly ILogger _logger;

        public GenericRepository(ToDoDbContext context)
        {
            _context = context;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _logger.Debug("{ItemId} was successfully added to the Db.", typeof(T).ToString());

            _context.SaveChanges();
        }

        public bool Delete(T entity)
        {
            if (!_context.Set<T>().Contains(entity))
            {
                _logger.Warn("Trying to delete nonexistent {EntityType}", typeof(T).ToString());
                return false;
            }

            _context.Set<T>().Remove(entity);
            _context.SaveChanges();

            _logger.Debug("{EntityType} was removed from the Db.", typeof(T).ToString());
                       
            return true;
        }

        public void Edit(T entity)
        {
            _logger.Error("Editing of {EntityType} is not available.", typeof(T).ToString());

            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            _logger.Info("Getting items from the db.");
            return _context.Set<T>();
        }
    }
}
