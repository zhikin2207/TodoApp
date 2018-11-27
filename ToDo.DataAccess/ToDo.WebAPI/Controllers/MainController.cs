using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.DataAccess;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories;
using ToDo.WebAPI.ViewModels;

namespace ToDo.WebAPI.Controllers
{
    [Route("ProfessionalSearch")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public MainController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemViewModel>> GetAll()
        {
            return _itemRepository.GetAll().Select(i => ConvertToItemViewModel(i)).ToList();
        }

        [HttpGet("{categoryString}/tags/{tagString}")]
        public ItemViewModel FindByKey(string categoryString, string tagString)
        {
            string[] categoryNames = categoryString.Split('-');
            string[] tagNames = tagString.Split('-');

            var item = _itemRepository.GetAll().Where(i => Array.IndexOf(categoryNames, i.Category.Name) != -1 && i.TagItem.Any(ti => tagNames.Contains<string>(ti.Tag.Name))).FirstOrDefault();

            return ConvertToItemViewModel(item);
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

        public ItemViewModel ConvertToItemViewModel(Item item)
        {
            return new ItemViewModel { Title = item.Title, Category = (item.Category == null) ? "null" : item.Category.Name,
                Id = item.Id, Description  = item.Description, DueDate = item.DueDate,
                Priority = item.Priority, Status = item.Status, TagNames = item.TagItem.Select(t => t.Tag.Name)}; 
        }
    }
}