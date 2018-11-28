using System;

namespace ToDo.Services.DTOs
{
    public class TagItemDTO
    {
        public Guid ItemId { get; set; }
        public ItemDTO Item { get; set; }

        public Guid TagId { get; set; }
        public TagDTO Tag { get; set; }
    }
}
