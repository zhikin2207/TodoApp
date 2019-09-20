using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;
using ToDo.DataAccess;
using ToDo.DataAccess.DataBase;
using ToDo.DataAccess.Models;
using ToDo.DataAccess.Repositories;
using ToDo.DataAccess.Repositories.CustomRepositories;
using ToDo.Services.Configuration;
using ToDo.Services.DTOs;
using ToDo.Services.Handlers;
using ToDo.Services.Handlers.HandlerInterfaces;
using ToDo.WebAPI.Configuration;

namespace ToDo.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private ILogger _logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _logger = LogManager.GetCurrentClassLogger();

            _logger.Info("The application has started.");
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _logger.Info("Setting options of the application..");

            services.AddDbContext<ToDoDbContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                }));

            services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddScoped<IGenericRepository<Tag>, GenericRepository<Tag>>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemHandler, ItemHandler>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<MappingWebProfile>();
                mc.AddProfile<MappingServicesProfile>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddMvcOptions(o => { o.Filters.Add(new ErrorHandlingFilter()); })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            _logger.Info("Options were set.");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
