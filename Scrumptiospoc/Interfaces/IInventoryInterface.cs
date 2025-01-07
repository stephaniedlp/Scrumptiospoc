using Scrumptiospoc.Models;
using System.Collections.ObjectModel;

namespace Scrumptiospoc.Interfaces
{
    public interface IInventoryInterface
    {
        public Task SetSelectedInventory(Inventory inventory);
        public Task<Inventory> GetSelectedInventory();
        public Task<bool> AssignProduct(Product product);
        public Task<bool> IsAdded(Product product);
    }
}
