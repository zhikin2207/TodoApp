using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.DataAccess;
using ToDo.DataAccess.DataBase;
using ToDo.DataAccess.Models;

namespace ToDo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly ToDoDbContext _context;
        private readonly GenericRepository<Item> repoItem;
        private readonly GenericRepository<Category> repoCat;
        private readonly GenericRepository<Tag> repoTag;

        public MainController(ToDoDbContext context)
        {
            _context = context;
            repoItem = new GenericRepository<Item>(_context);
            repoCat = new GenericRepository<Category>(_context);
            repoTag = new GenericRepository<Tag>(_context);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAll()
        {
            return _context.Items.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Item> Get(Guid id)
        {
            return repoItem.GetById(id);
        }

        [HttpPost]
        public void Post([FromBody] Item value)
        {
            repoItem.Add(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}