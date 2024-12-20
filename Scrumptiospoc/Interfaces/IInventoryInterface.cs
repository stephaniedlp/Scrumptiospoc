using Scrumptiospoc.Models;

namespace Scrumptiospoc.Interfaces
{
    public interface IInventoryInterface
    {
        public void AddInventoryItem(Location location, Product product);
        public void  IncreaseQuantity(InventoryItem inventoryItem);
        public void DecreaseQuantity(InventoryItem inventoryItem);
    }
}
