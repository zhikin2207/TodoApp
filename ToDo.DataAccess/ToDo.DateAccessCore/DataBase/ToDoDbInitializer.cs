using System;
using System.Linq;
using ToDo.DataAccess.Models;

namespace ToDo.DataAccess.DataBase
{
    public class ToDoDbInitializer
    {
        public static void Initialize(ToDoDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Items.Any())
            {
                return; 
            }

            var cat1 = new Category { Name = "Cat1", Parent = null };
            var cat2 = new Category { Name = "Cat11", Parent = cat1 };
            var cat3 = new Category { Name = "Cat12", Parent = cat1 };

            var tag1 = new Tag { Name = "Tag1", Color = "Yellow" };
            var tag2 = new Tag { Name = "Tag2", Color = "Red" };

            var item1 = new Item { Title = "Item1", Description = "Text", Priority = Priority.High, Caregory = cat1, DueDate = DateTime.Now, Status = true };
            var item2 = new Item { Title = "Item2", Description = "Text", Priority = Priority.Low, Caregory = cat2, DueDate = DateTime.Now, Status = true };
            var item3 = new Item { Title = "Item3", Description = "Text", Priority = Priority.Medium, Caregory = cat3, DueDate = DateTime.Now, Status = false };
            var item4 = new Item { Title = "Item4", Description = "Text", Priority = Priority.SuperLow, Caregory = cat3, DueDate = DateTime.Now, Status = false };

            var con1 = new TagItem { Item = item1, Tag = tag1 };
            var con2 = new TagItem { Item = item1, Tag = tag2 };
            var con3 = new TagItem { Item = item2, Tag = tag1 };
            var con4= new TagItem { Item = item2, Tag = tag2 };

            context.Items.AddRange(item1, item2, item3, item3);
            context.Categories.AddRange(cat1, cat2, cat3);
            context.Tags.AddRange(tag1, tag2);
            context.TagsItems.AddRange(con1, con2, con3, con4);

            context.SaveChanges();
        }
    }
}
