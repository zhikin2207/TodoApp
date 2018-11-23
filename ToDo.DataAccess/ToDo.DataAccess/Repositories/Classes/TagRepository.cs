using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DataAccess.DataBase;
using ToDo.DataAccess.Models;

namespace ToDo.DataAccess.Repositories.Classes
{
    public class TagRepository
    {
        private readonly ToDoDbContext _context;

        public TagRepository(ToDoDbContext context)
        {
            _context = context;
        }

        public void Add(Tag entity)
        {
            _context.Tags.Add(entity);

            _context.SaveChanges();
        }

        public bool Delete(Tag entity)
        {
            if (!_context.Tags.Contains(entity)) return false;

            _context.Tags.Remove(entity);
            _context.SaveChanges();

            return true;
        }

        public void Edit(Tag entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tag> GetAll()
        {
            return _context.Tags;
        }

        public Tag GetById(Guid guid)
        {
            return _context.Tags.FirstOrDefault(x => x.Id == guid);
        }
    }
}
