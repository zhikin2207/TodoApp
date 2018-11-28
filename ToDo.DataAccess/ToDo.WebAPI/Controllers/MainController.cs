using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDo.Services.Handlers.HandlerInterfaces;
using ToDo.Services.ViewModels;
using ToDo.WebAPI.ViewModels;

namespace ToDo.WebAPI.Controllers
{
    [Route("ProfessionalSearch")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IItemHandler _itemHandler;

        public MainController(IItemHandler itemHandler)
        {
            _itemHandler = itemHandler;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemDisplayViewModel>> GetAll()
        {
            return _itemHandler
                .GetAll()
                .Select(ViewModelConverter.ConvertToItemDisplayViewModel)
                .ToList();
        }

        [HttpGet("{category}/tags/{tagString}")]
        public IEnumerable<ItemDisplayViewModel> Search(
            string category,
            string tagString)
        {
            string[] tags = tagString.Split('-');

            return _itemHandler
                .Search(category, tags)
                .Select(ViewModelConverter.ConvertToItemDisplayViewModel)
                .ToList(); ;
        }

        [HttpPost]
        public void Create([FromBody] ItemCreateViewModel value)
        {
            _itemHandler.Create(ViewModelConverter.ConvertToItemDTO(value), value.Tags);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _itemHandler.Delete(id);
        }

        [HttpGet("adults")]
        public ActionResult<StatisticViewModel> GetAdultItems()
        {
            return ViewModelConverter.ConvertToStatisticViewModel(
                _itemHandler.GetAdultItems());
        }
    }
}