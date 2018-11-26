using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDo.DataAccess;
using ToDo.DataAccess.Models;

namespace ToDo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IGenericRepository<Item> _itemRepository;

        public ItemController(IGenericRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAll()
        {
            return _itemRepository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Item> FindByKey(Guid id)
        {
            return _itemRepository.GetAll().Where( i => i.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public void Create([FromBody] Item value)
        {
            _itemRepository.Add(value);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var itemToDelete = _itemRepository.GetAll().Where(i => i.Id == id).FirstOrDefault();
            _itemRepository.Delete(itemToDelete);
        }
    }
}