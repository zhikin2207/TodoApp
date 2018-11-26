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
    public class TagController : ControllerBase
    {
        private readonly IGenericRepository<Tag> _tagRepository;

        public TagController(IGenericRepository<Tag> tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tag>> GetAll()
        {
            return _tagRepository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Tag> FindByKey(Guid id)
        {
            return _tagRepository.GetAll().Where( i => i.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public void Create([FromBody] Tag value)
        {
            _tagRepository.Add(value);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var itemToDelete = _tagRepository.GetAll().Where(i => i.Id == id).FirstOrDefault();
            _tagRepository.Delete(itemToDelete);
        }
    }
}