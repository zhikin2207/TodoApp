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
    public class CategoryController : ControllerBase
    {
        private readonly IGenericRepository<Category> categoryRepository;

        public CategoryController(IGenericRepository<Category> newCategoryRepository)
        {
            categoryRepository = newCategoryRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            return categoryRepository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Category> FindByKey(Guid id)
        {
           return categoryRepository.GetAll().Where( i => i.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public void Create([FromBody] Category value)
        {
            categoryRepository.Add(value);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            categoryRepository.Delete(categoryRepository.GetAll().Where( i => i.Id == id).FirstOrDefault());
        }
    }
}