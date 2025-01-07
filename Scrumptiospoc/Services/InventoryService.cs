using Scrumptiospoc.Components.Pages;
using Scrumptiospoc.Interfaces;
using Scrumptiospoc.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Scrumptiospoc.Services
{
    public class InventoryService : IInventoryInterface
    {
        public Models.Inventory selectedInventory;


        public async Task SetSelectedInventory(Models.Inventory inv)
        {
            selectedInventory = inv;
        }

        public async Task<Models.Inventory> GetSelectedInventory()
        {
            return selectedInventory;
        }

        public async Task<bool> IsAdded(Product product)
        {
            bool isthere = selectedInventory.Items.Any(w => w.Product.Id == product.Id);
            return isthere;
        }


        public async Task<bool> AssignProduct(Product product)
        {
            InventoryItem inv = new(selectedInventory, product);

            if (!await IsAdded(product))
            {
                selectedInventory.Items.Add(inv);
                return true;
            }
            else
            {
                return false;
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
