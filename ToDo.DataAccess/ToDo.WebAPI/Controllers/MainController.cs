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
            return _itemRepository
                .GetAll()
                .Select(ConvertToItemViewModel)
                .ToList();
        }

        [HttpGet("{categoryString}/tags/{tagString}")]
        public IEnumerable<ItemViewModel> Search(string category, string tagString)
        {
            string[] tags = tagString.Split('-');

            var items = _itemRepository
                .GetAll()
                .Where(i => string.Equals(
                    i.Category.Name,
                    category,
                    StringComparison.CurrentCultureIgnoreCase))
                .Where(i => i.TagItem
                    .Select(ti => ti.Tag.Name)
                    .Intersect(tags)
                    .Any())
                .Select(ConvertToItemViewModel)
                .ToList();

            return items;
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
            return new ItemViewModel
            {
                Title = item.Title,
                Category = item.Category.Name,
                Id = item.Id,
                Description = item.Description,
                DueDate = item.DueDate,
                Priority = item.Priority,
                Status = item.Status,
                TagNames = item.TagItem.Select(t => t.Tag.Name)
            };
        }
    }
}