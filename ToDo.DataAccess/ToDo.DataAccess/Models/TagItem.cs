using System;

namespace ToDo.DataAccess.Models
{
    public class TagItem
    {
        public Guid ItemId { get; set; }
        public Item Item { get; set; }

        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
