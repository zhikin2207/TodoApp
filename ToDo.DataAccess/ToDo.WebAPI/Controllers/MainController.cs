using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDo.Services.DTOs;
using ToDo.Services.Handlers.HandlerInterfaces;
using ToDo.Services.ViewModels;
using ToDo.WebAPI.ViewModels;

namespace ToDo.WebAPI.Controllers
{
    [Route("ProfessionalSearch")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IItemHandler _itemHandler;

        public MainController(IItemHandler itemHandler, IMapper mapper)
        {
            _itemHandler = itemHandler;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemDisplayViewModel>> GetAll()
        {
            return _itemHandler
                .GetAll()
                .Select(_mapper.Map<ItemDTO, ItemDisplayViewModel>)
                .ToList();
        }

        [HttpGet("{category}/tags/{tagString}")]
        public IEnumerable<ItemDisplayViewModel> Search(
            string category,
            string tagString)
        {
            string[] tags = tagString.Split('-');

            var items = _itemHandler.Search(category, tags);

            return items
                .Select(_mapper.Map<ItemDisplayViewModel>)
                .ToList();
        }

        [HttpPost]
        public void Create([FromBody] ItemCreateViewModel item)
        {
            var itemDTO = _mapper.Map<ItemCreateViewModel, ItemDTO>(item);

            _itemHandler.Create(itemDTO, item.Tags);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _itemHandler.Delete(id);
        }

        [HttpGet("adults")]
        public ActionResult<StatisticViewModel> GetAdultItems()
        {
            var adultItems = _itemHandler.GetAdultItems();

            return _mapper.Map<StatisticDTO, StatisticViewModel>(adultItems);
        }
    }
}