using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.DataAccess;
using ToDo.DataAccess.DataBase;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories.Interfaces;

namespace ToDo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IGenericRepository<Item> itemRepository;

        public ItemController(IGenericRepository<Item> newItemRepository)
        {
            itemRepository = itemRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAll()
        {
            return itemRepository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Item> FindByKey(Guid id)
        {
            return itemRepository.GetAll().Where( i => i.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public void Create([FromBody] Item value)
        {
            itemRepository.Add(value);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            itemRepository.Delete(itemRepository.GetAll().Where( i => i.Id == id).FirstOrDefault());
        }
    }
}