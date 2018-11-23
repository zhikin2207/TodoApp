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
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository repoItem

        public ItemController(IItemRepository itemRepository)
        {
            repoItem = itemRepository;
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