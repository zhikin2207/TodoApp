using System;

namespace ToDo.Services.DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public CategoryDTO Parent { get; set; }
    }
}
