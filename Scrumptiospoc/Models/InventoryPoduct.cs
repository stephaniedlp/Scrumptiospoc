namespace Scrumptiospoc.Models
{
    public class InventoryPoduct
    {
        public InventoryPoduct(Product prod, InventoryItem item)
        {
            Name = prod.Name;
            Description = prod.Description;
            Item = item;
            InventoryItemId = item.Id;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public InventoryItem Item { get; set; }
        public Guid InventoryItemId { get; set; }
    }


}
