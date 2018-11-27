using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Services.DTOs
{
    public class StatisticDTO
    {
        public IEnumerable<ItemDTO> items { get; set; }

        public Dictionary<string, int> priorityCounts { get; set; }
    }
}
