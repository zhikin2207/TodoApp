using System;
using System.Collections.Generic;
using ToDo.DataAccess.Models;

namespace ToDo.DataAccess
{
    public interface IGenericRepository<T>
    {
        void Add(T entity);
        IEnumerable<T> GetAll();
        bool Delete(T entity);
        void Edit(T entity);
    }
}
