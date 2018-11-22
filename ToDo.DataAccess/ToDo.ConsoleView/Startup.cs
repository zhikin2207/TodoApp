using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDo.DataAccess.DataBase;
using Microsoft.EntityFrameworkCore;
using ToDo.DataAccess;

namespace ToDo.ConsoleView
{
    public class Startup
    {
        private IConfiguration _configuration;
               
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<ToDoDbContext>(options =>
                   options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ToDoDbContext, ToDoDbContext>();
            services.AddScoped<IDataRepository, GenericRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}