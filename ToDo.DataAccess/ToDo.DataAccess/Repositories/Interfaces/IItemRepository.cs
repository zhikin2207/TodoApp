using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.Models;

namespace ToDo.DataAccess.Repositories.Interfaces
{
    public interface IItemRepository : IGenericRepository<Item>
    {
        //new Item GetById(Guid id);
    }
}
