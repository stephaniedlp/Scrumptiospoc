using Scrumptiospoc.Models;
using System.Collections.ObjectModel;

namespace Scrumptiospoc.Interfaces
{
    public interface IInventoryInterface
    {
        public Inventory SelectedInventory { get; set;}
        public Task<bool> AssignProduct(Product product);
        public Task<bool> IsAdded(Product product);
    }
}
