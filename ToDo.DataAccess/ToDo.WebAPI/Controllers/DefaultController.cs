using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.DataAccess.DataBase;
using ToDo.DataAccess.Models;

namespace ToDo.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly ToDoDbContext _context;

        public DefaultController(ToDoDbContext context)
        {
            _context = context;
            ToDoDbInitializer.Initialize(_context);
        }


        [HttpGet]
        public ActionResult<List<Item>> GetAll()
        {
            return _context.Items.ToList();
        }
    }
}