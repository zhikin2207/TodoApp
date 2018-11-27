﻿using System;
using System.Collections.Generic;
using System.Text;
using ToDo.DataAccess.Models;

namespace ToDo.Services.DTOs
{
    public class ItemDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Status { get; set; }
        public Priority Priority { get; set; }

        public Category Category { get; set; }
        public IEnumerable<TagItem> TagItem { get; set; }
    }
}