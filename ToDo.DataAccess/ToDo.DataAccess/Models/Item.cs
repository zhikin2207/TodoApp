using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.DataAccess.Models
{
    public enum ItemPriority
    {
        SuperHigh,
        High,
        Medium,
        Low,
        SuperLow        
    }

    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public  string Description { get; set; }
        public ItemPriority Priority { get; set; }
        public DateTime DueDate { get; set; }
        public ItemCategory Caregory { get; set; }
        public string Status { get; set; }

        public IEnumerable<Tag_Item> TagItem { get; set; }

    }
}
