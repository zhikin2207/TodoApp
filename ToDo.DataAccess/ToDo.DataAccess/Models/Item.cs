using System;
using System.Collections.Generic;

namespace ToDo.DataAccess.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public  string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime DueDate { get; set; }
        public Category Caregory { get; set; }
        public bool Status { get; set; }

        public IEnumerable<TagItem> TagItem { get; set; }
    }
}
