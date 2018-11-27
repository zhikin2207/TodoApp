using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.DataAccess.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public  string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Status { get; set; }
        public Priority Priority { get; set; }

        public Category Category { get; set; }
        public IEnumerable<TagItem> TagItem { get; set; }
    }
}
