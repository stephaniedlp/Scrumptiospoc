using Scrumptiospoc.Interfaces;
using Scrumptiospoc.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Scrumptiospoc.Services
{
    public class InventoryService : IInventoryInterface
    {
        private ObservableCollection<InventoryItem> _inventory;
        public ObservableCollection<InventoryItem> Inventory
        {
            get => _inventory;
            set
            {
                if (_inventory != value)
                {

                    _inventory = value;
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
