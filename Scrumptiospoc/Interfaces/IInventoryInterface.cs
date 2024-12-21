using Scrumptiospoc.Models;
using System.Collections.ObjectModel;

namespace Scrumptiospoc.Interfaces
{
    public interface IInventoryInterface
    {
        public ObservableCollection<InventoryItem> InventoryItem { get; set; }
        public void AddInventoryItem(Location location, List<Product> product);
        public void  IncreaseQuantity(InventoryItem inventoryItem);
        public void DecreaseQuantity(InventoryItem inventoryItem);
        public bool IsProductInThisLocation(Location location, Product product);
    }
}
