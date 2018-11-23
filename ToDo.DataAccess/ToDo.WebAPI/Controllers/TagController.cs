using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.DataAccess;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories.Interfaces;

namespace ToDo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IGenericRepository<Tag> tagRepository;

        public TagController(IGenericRepository<Tag> newTagRepository)
        {
            tagRepository = newTagRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tag>> GetAll()
        {
            return tagRepository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Tag> FindByKey(Guid id)
        {
            return tagRepository.GetAll().Where( i => i.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public void Create([FromBody] Tag value)
        {
            tagRepository.Add(value);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            tagRepository.Delete(tagRepository.GetAll().Where( i => i.Id == id).FirstOrDefault());
        }
    }
}