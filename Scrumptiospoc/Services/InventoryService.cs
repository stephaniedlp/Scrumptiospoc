using Scrumptiospoc.Components.Pages;
using Scrumptiospoc.Interfaces;
using Scrumptiospoc.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Scrumptiospoc.Services
{
    public class InventoryService : IInventoryInterface
    {
        public Models.Inventory SelectedInventory { get; set; }

        public async Task<bool> IsAdded(Product product)
        {
            bool isthere =  SelectedInventory.Items.Any(w => w.Product.Id == product.Id);
            return isthere;
        }


        public async Task<bool> AssignProduct(Product product)
        {
            InventoryItem inv = new(SelectedInventory, product);

            if (!await IsAdded(product))
            {
                SelectedInventory.Items.Add(inv);
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
