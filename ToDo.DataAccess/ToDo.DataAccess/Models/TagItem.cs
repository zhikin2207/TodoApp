using System;

namespace ToDo.DataAccess.Models
{
    public class TagItem
    {
        public Guid Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
