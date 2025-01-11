using Scrumptiospoc.Components.Pages;

namespace Scrumptiospoc.Models
{
    public class InventoryItem :BaseModel
    {
        public Guid Id { get; set; }
        private Inventory _inventory;
        public Inventory Inventory
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
        public Guid InventoryId { get; set; }
        private Product _product;
        public Product Product
        {
            get => _product;
            set
            {
                if (_product != value)
                {
                    _product = value;
                    OnPropertyChanged(nameof(Product));
                }
            }
        }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public int MaximumItem { get; set; }
        public int MinimumItem { get; set; }
        private bool _isDisabled;
        public bool IsDisabled
        {
            get => _isDisabled;
            set
            {
                if (_isDisabled != value)
                {
                    _isDisabled = value;
                    OnPropertyChanged(nameof(IsDisabled));
                }
            }
        }
        public InventoryItem(Inventory inventory, Product prod)
        {
            Id = Guid.NewGuid();  
            Inventory = inventory;
            InventoryId = inventory.Id;
            Product = prod;          
            ProductId = prod.Id;
            IsDisabled = false;
            Quantity = 10;
            
        }     
    }
}
