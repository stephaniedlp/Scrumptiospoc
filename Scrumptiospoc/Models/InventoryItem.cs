namespace Scrumptiospoc.Models
{
    public class InventoryItem
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int MaximumItem { get; set; }
        public int MinimumItem { get; set; }
        public InventoryItem(Product prod)
        {
            Id = Guid.NewGuid();  // Unique ID for the inventory item
            Product = prod;       // Link the inventory item to the product
            Quantity = 0;         // Initialize quantity to 0
            MaximumItem = 100;    // Default maximum quantity
            MinimumItem = 1;      // Default minimum quantity
        }

    }
}
