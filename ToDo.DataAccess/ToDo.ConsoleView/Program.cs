using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using System;
using ToDo.DataAccess.DataBase;
using ToDo.DataAccess.Models;
using ToDo.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ToDo.ConsoleView
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            //Item item = new Item();
            //Category category = new Category();
            //Tag tag = new Tag();
            //TagItem tagItem = new TagItem();

            //FillObjects(item, category, tag, tagItem);


            var optionsBuilder = new DbContextOptionsBuilder<ToDoDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ToDoDB;Trusted_Connection=True;");

            GenericRepository repo = new GenericRepository(new ToDoDbContext(optionsBuilder.Options));

            //repo.Add<Item>(item);
            //repo.Add<Category>(category);
            //repo.Add<Tag>(tag);
            //repo.Add<TagItem>(tagItem);

            var cat = new Category { Name = "CatPro", Parent = null };

            var tag = new Tag { Name = "Tag5", Color = "Blue" };

            var item = new Item { Title = "ItemPro", Description = "Text", Priority = Priority.High, Caregory = cat, DueDate = DateTime.Now, Status = true };

            var con = new TagItem { Item = item, Tag = tag };

            repo.Add<Item>(item);
            //repo.Add<Category>(cat);
            repo.Add<Tag>(tag);
            repo.Add<TagItem>(con);

            foreach (var i in repo.GetAll<Item>())
            {
                Console.WriteLine(i.Title);
            }
        }

        static public void FillObjects(Item item, Category category, Tag tag, TagItem tagItem)
        {
            Console.WriteLine("Title: ");
            item.Title = Console.ReadLine();

            Console.WriteLine("Descroption: ");
            item.Description = Console.ReadLine();

            Console.WriteLine("Due date: Today.");
            item.DueDate = DateTime.Now;

            Console.WriteLine("Status (T/F): ");

            if (Console.ReadLine() == "T")
            {
                item.Status = true;
            }
            else item.Status = false;

            Console.WriteLine("Priority (1-5): ");
            item.Priority = (Priority)Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Category name: ");
            category.Name = Console.ReadLine();

            Console.WriteLine("Tag: ");
            tag.Name = Console.ReadLine();
            tag.Color = Console.ReadLine();

            tagItem = new TagItem { Item = item, Tag = tag };
            item.Caregory = category;
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
