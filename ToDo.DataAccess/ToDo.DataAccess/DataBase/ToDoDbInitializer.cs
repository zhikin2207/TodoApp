using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ToDo.DataAccess.Models;

namespace ToDo.DataAccess.DataBase
{
    public class ToDoDbInitializer
    {
        public static void Initialize(ToDoDbContext context)//SchoolContext is EF context
        {

            context.Database.EnsureCreated();//if db is not exist ,it will create database .but ,do nothing .

            if (context.Items.Any())
            {
                return;   // DB has been seeded
            }


            var cat1 = new ItemCategory { Name = "Cat1", Parent = null };
            var cat2 = new ItemCategory { Name = "Cat11", Parent = cat1 };
            var cat3 = new ItemCategory { Name = "Cat12", Parent = cat1 };

            var tag1 = new Tag { Name = "Tag1", Color = "Yellow" };
            var tag2 = new Tag { Name = "Tag2", Color = "Red" };

            var item1 = new Item { Title = "Item1", Description = "Text", Priority = ItemPriority.High, Caregory = cat1, DueDate = DateTime.Now, Status = "OK" };
            var item2 = new Item { Title = "Item2", Description = "Text", Priority = ItemPriority.Low, Caregory = cat2, DueDate = DateTime.Now, Status = "Bad" };
            var item3 = new Item { Title = "Item3", Description = "Text", Priority = ItemPriority.Medium, Caregory = cat3, DueDate = DateTime.Now, Status = "Finished" };
            var item4 = new Item { Title = "Item4", Description = "Text", Priority = ItemPriority.SuperLow, Caregory = cat3, DueDate = DateTime.Now, Status = "OK" };

            var con1 = new Tag_Item { Item = item1, Tag = tag1 };
            var con2 = new Tag_Item { Item = item1, Tag = tag2 };
            var con3 = new Tag_Item { Item = item2, Tag = tag1 };
            var con4= new Tag_Item { Item = item2, Tag = tag2 };

            context.Items.AddRange(item1, item2, item3, item3);
            context.Categories.AddRange(cat1, cat2, cat3);
            context.Items.AddRange(item1, item2, item3, item3);
            context.Items.AddRange(item1, item2, item3, item3);

            context.SaveChanges();
        }
    }
}
