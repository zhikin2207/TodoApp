using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.WebAPI.Configuration
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public ErrorHandlingFilter()
        {
           _logger = LogManager.GetCurrentClassLogger();
        }

        public override void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(new { Status = 500, Error = context.Exception.Message });

            _logger.Error("EX: {Message}, {StackTrace}", context.Exception.Message, context.Exception.StackTrace);

            context.ExceptionHandled = true;
        }
    }
}
