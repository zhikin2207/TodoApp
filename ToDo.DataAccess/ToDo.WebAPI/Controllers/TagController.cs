using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories.Interfaces;

namespace ToDo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository repoTag;

        public TagController(ITagRepository TagRepository)
        {
            repoTag = TagRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tag>> GetAll()
        {
            return repoTag.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Tag> FindByKey(Guid id)
        {
            return repoTag.GetById(id);
        }

        [HttpPost]
        public void Create([FromBody] Tag value)
        {
            repoTag.Add(value);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            repoTag.Delete(repoTag.GetById(id));
        }
    }
}