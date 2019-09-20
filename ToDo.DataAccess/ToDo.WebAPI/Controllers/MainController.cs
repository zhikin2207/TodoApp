using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLog;
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
        private readonly ILogger _logger;

        public MainController(IItemHandler itemHandler, IMapper mapper)
        {
            _itemHandler = itemHandler;
            _mapper = mapper;
            _logger = LogManager.GetCurrentClassLogger();
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemDisplayViewModel>> GetAll()
        {
            _logger.Info("{method} request has been received.", "GET");

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
            _logger.Info("{method} request for searching has been received.", "GET");

            string[] tags = tagString.Split('-');

            var items = _itemHandler.Search(category, tags);

            return items
                .Select(_mapper.Map<ItemDisplayViewModel>)
                .ToList();
        }

        [HttpPost]
        public void Create([FromBody] ItemCreateViewModel item)
        {
            _logger.Info("{method} request for creating has been received.", "POST");

            var itemDTO = _mapper.Map<ItemCreateViewModel, ItemDTO>(item);

            _itemHandler.Create(itemDTO, item.Tags);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _logger.Info("{method} request to delete {ItemId} has been received.", "DELETE", id);

            _itemHandler.Delete(id);
        }

        [HttpGet("adults")]
        public ActionResult<StatisticViewModel> GetAdultItems()
        {
            _logger.Info("{method} request for getting adult items has been received.", "GET");

            var adultItems = _itemHandler.GetAdultItems();

            return _mapper.Map<StatisticDTO, StatisticViewModel>(adultItems);
        }
    }
}