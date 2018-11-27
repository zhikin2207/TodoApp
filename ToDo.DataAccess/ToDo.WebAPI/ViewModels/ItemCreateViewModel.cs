using System;
using System.Collections.Generic;
using ToDo.DataAccess.Models;

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

        public Category Category { get; set; }
        public IEnumerable<TagItem> TagItem { get; set; }
    }
}
