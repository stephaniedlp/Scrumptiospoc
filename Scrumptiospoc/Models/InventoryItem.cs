namespace Scrumptiospoc.Models
{
    public class InventoryItem
    {
        public Guid Id { get; set; }
        public Inventory Inventory { get; set; }
        public Guid InventoryId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int MaximumItem { get; set; }
        public int MinimumItem { get; set; }
        
        public InventoryItem(Inventory inventory, Product prod)
        {
            Id = Guid.NewGuid();  
            Inventory = inventory;
            Product = prod;          
        }     
    }
}
