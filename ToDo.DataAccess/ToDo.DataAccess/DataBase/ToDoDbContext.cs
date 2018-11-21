using Microsoft.EntityFrameworkCore;
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
            modelBuilder.Entity<TagItem>().HasKey(t => new { t.ItemId, t.TagId});
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagItem> TagsItems { get; set; }
    }
}
