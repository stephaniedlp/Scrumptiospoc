using System.Collections.ObjectModel;

namespace Scrumptiospoc.Models
{
    public class InventoryItem: BaseModel
    {

        public Guid Id { get; set; }
       
        public bool IsExtinct { get; set; }

        private Product _product = new();
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

        private Location _location = new();
        public Location Location
        {
            get => _location;
            set
            {
                if (_location != value)
                {

                    _location = value;
                    OnPropertyChanged(nameof(Location));
                }
            }
        }
        public int Quantity { get; set; }
        public int MaximumItem { get; set; }
        public int MinimumItem { get; set; }
       
        public InventoryItem(Product product, Location location)
        {
            Id = location.Id;
            Product = product;
            Location = location;
            Quantity = 0;
            MaximumItem = 9999;
            MinimumItem = 0;
            Product = new();
            Location = new();
        }
        

    }
}
