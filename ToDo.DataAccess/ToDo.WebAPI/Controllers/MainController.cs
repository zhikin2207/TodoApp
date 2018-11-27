using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.DataAccess;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories;
using ToDo.Services.Handlers;
using ToDo.Services.Handlers.HandlerInterfaces;
using ToDo.Services.ViewModels;
using ToDo.Services;
using ToDo.WebAPI.ViewModels;
using ToDo.Services.DTOs;

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
            return _itemHandler.GetAll().Select(CustomConverter.ConvertToItemDisplayViewModel).ToList();
        }

        [HttpGet("{category}/tags/{tagString}")]
        public IEnumerable<ItemDisplayViewModel> Search(string category, string tagString)
        {
            string[] tags = tagString.Split('-');

            return _itemHandler.Search(category, tags).Select(CustomConverter.ConvertToItemDisplayViewModel).ToList(); ;
        }

        [HttpPost]
        public void Create([FromBody] ItemCreateViewModel value)
        {
            _itemHandler.Create(CustomConverter.ConvertToItemDTO(value));
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _itemHandler.Delete(id);
        }

       
    }
}