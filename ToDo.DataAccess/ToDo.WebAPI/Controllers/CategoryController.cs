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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository repoCategory;

        public CategoryController(ICategoryRepository CategoryRepository)
        {
            repoCategory = CategoryRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            return repoCategory.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Category> FindByKey(Guid id)
        {
            return repoCategory.GetById(id);
        }

        [HttpPost]
        public void Create([FromBody] Category value)
        {
            repoCategory.Add(value);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            repoCategory.Delete(repoCategory.GetById(id));
        }
    }
}