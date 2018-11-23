using System;
using System.Collections.Generic;

namespace ToDo.DataAccess
{
    public interface IGenericRepository<T>
    {
        void Add(T entity);
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        bool Delete(T entity);
        void Edit(T entity);
    }
}
