using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.DataAccess;
using ToDo.DataAccess.DataBase;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories.Classes;
using ToDo.DataAccess.Repositories.Interfaces;

namespace ToDo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly ToDoDbContext _context;
        private readonly IItemRepository repoItem;
        private readonly ICategoryRepository repoCat;
        private readonly ITagRepository repoTag;

        public MainController(IItemRepository itemRepository, ICategoryRepository catRepository, ITagRepository tagRepository)
        {
            repoItem = itemRepository;
            repoCat = catRepository;
            repoTag = tagRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAll()
        {
            return repoItem.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Item> FindByKey(Guid id)
        {
            return repoItem.GetById(id);
        }

        [HttpPost]
        public void Create([FromBody] Item value)
        {
            repoItem.Add(value);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            repoItem.Delete(repoItem.GetById(id));
        }
    }
}