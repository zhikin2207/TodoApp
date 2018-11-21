using System.Collections.Generic;

namespace ToDo.DataAccess.Models
{
    public class Tag
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string Color{ get; set; }

        public IEnumerable<Tag_Item> TagItem { get; set; }
    }
}