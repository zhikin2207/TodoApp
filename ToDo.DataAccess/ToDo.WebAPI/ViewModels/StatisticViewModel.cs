using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Services.DTOs;

namespace ToDo.WebAPI.ViewModels
{
    public class StatisticViewModel
    {
        public IEnumerable<ItemDTO> Items { get; set; }

        public Dictionary<string, int> PriorityCounts { get; set; }
    }
}
