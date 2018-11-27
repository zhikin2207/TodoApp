using System;
using System.Collections.Generic;

namespace ToDo.DataAccess.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public IEnumerable<TagItem> TagItem { get; set; }
    }
}