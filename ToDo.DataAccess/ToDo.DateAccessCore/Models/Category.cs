using System;

namespace ToDo.DataAccess.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Category Parent { get; set; }
    }
}