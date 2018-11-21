namespace ToDo.DataAccess.Models
{
    public class ItemCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemCategory Parent{ get; set; }
    }
}