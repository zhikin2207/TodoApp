﻿using System;
using System.Collections.Generic;
using ToDo.DataAccess.Models;
using ToDo.Services.DTOs;

namespace ToDo.WebAPI.ViewModels
{
    public class ItemDisplayViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Status { get; set; }
        public PriorityDTO Priority { get; set; }

        public string Category { get; set; }
        public IEnumerable<string> TagNames { get; set; }

    }
}
