using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.DataAccess.Models
{
    public class Tag_Item
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }

    }
}
