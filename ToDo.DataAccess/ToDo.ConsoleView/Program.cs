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
            var context = new ToDoDbContext(new DbContextOptionsBuilder<ToDoDbContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ToDoDB;Trusted_Connection=True;").Options);

            ToDoDbInitializer.Initialize(context);

            GenericRepository<Item> repoItem = new GenericRepository<Item>(context);
            GenericRepository<Category> repoCategory = new GenericRepository<Category>(context);
            GenericRepository<Tag> repoTag = new GenericRepository<Tag>(context);

            var cat = new Category { Name = "CatPro", Parent = null };
            var tag = new Tag { Name = "Tag5", Color = "Blue" };
            var item = new Item { Title = "ItemPro", Description = "Text", Priority = Priority.High, Caregory = cat, DueDate = DateTime.Now, Status = true };

            repoItem.Add(item);
            repoCategory.Delete(cat);
            repoCategory.Add(cat);
            repoTag.Add(tag);

            foreach (var i in repoItem.GetAll())
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
    }
}
