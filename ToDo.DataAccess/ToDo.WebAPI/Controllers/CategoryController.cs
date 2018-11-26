using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.DataAccess;
using ToDo.DataAccess.Models;

namespace ToDo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryController(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            return _categoryRepository.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Category> FindByKey(Guid id)
        {
           return _categoryRepository.GetAll().Where(i => i.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public void Create([FromBody] Category value)
        {
            _categoryRepository.Add(value);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            var itemToDelete = _categoryRepository.GetAll().Where(i => i.Id == id).FirstOrDefault();
            _categoryRepository.Delete(itemToDelete);
        }
    }
}