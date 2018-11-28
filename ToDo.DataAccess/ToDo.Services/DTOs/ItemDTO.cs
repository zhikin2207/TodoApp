using System;
using System.Collections.Generic;
using ToDo.DataAccess.Models;

namespace ToDo.Services.DTOs
{
    public class ItemDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Status { get; set; }
        public PriorityDTO Priority { get; set; }

        public CategoryDTO Category { get; set; }
        public IEnumerable<TagDTO> Tags{ get; set; }
    }
}
