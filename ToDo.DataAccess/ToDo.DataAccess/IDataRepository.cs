using System.Collections.Generic;

namespace ToDo.DataAccess
{
    public interface IDataRepository
    {
        void Add<T>(T entity) where T : class;
        IEnumerable<T> GetAll<T>() where T : class;
        bool Delete<T>(T entity) where T : class;
        void Edit<T>(T entity) where T : class;
    }
}
