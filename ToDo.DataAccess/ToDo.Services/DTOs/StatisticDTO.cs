using System.Collections.Generic;

namespace ToDo.Services.DTOs
{
    public class StatisticDTO
    {
        public IEnumerable<ItemDTO> Items { get; set; }

        public Dictionary<string, int> PriorityCounts { get; set; }
    }
}
