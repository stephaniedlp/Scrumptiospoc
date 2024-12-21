using Scrumptiospoc.Interfaces;
using Scrumptiospoc.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Scrumptiospoc.Services
{
    public class InventoryService : IInventoryInterface
    {
        private ObservableCollection<InventoryItem> _inventoryItem = new();
        public ObservableCollection<InventoryItem> InventoryItem
        {
            get => _inventoryItem;
            set
            {
                if (_inventoryItem != value)
                {

                    _inventoryItem = value;
                    OnPropertyChanged(nameof(Inventory));
                }
            }
        }
        public void DecreaseQuantity(InventoryItem inventoryItem)
        {
            if (inventoryItem.Quantity > 0)
            {
                inventoryItem.Quantity--;
            }
            OnPropertyChanged(nameof(Inventory));
        }

        public void IncreaseQuantity(InventoryItem inventoryItem)
        {
            inventoryItem.Quantity++;
            OnPropertyChanged(nameof(Inventory));
        }

        public async void AddInventoryItem(Location location, List<Product> products)
        {
            // Ensure Items is initialized
            if (location.Inventory == null)
            {
                location.Inventory = new Inventory(location) { Items = new List<InventoryItem>() };
            }
            // Filter products that are not already in the location's inventory
            var productsToAdd = products
                .Where(product => !IsProductInThisLocation(location, product))
                .ToList();


            // Si el producto NO está en el inventario
            if (productsToAdd.Any())
            {
                foreach (var product in productsToAdd)
                {
                    var newItem = new InventoryItem(product, location)
                    {
                        Quantity = 0 // Start with a quantity of 1
                    };
                    
                    location.Inventory.Items.Add(newItem);
                }
            }

            OnPropertyChanged(nameof(location.Inventory.Items));
        }

        public bool IsProductInThisLocation(Location location, Product product)
        {            
            bool productExistinthisLocation = InventoryItem.Any(w => w.Product.Id == product.Id);
            return productExistinthisLocation;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
