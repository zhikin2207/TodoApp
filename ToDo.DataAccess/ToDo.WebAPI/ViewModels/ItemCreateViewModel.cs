using System;
using System.Collections.Generic;
using ToDo.DataAccess.Models;
using ToDo.Services.DTOs;

namespace ToDo.Services.ViewModels
{
    public class ItemCreateViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Status { get; set; }
        public Priority Priority { get; set; }

        public CategoryDTO Category { get; set; }
        public IEnumerable<TagItemDTO> TagItem { get; set; }
    }
}
