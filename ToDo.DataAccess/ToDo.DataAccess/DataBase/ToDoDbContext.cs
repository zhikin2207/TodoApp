using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.DataAccess.Models;

namespace ToDo.DataAccess.DataBase
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag_Item>().HasKey(t => new { t.ItemId, t.TagId});
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCategory> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Tag_Item> Tags_Items { get; set; }
    }
}
