using System.Collections.ObjectModel;

namespace Scrumptiospoc.Models
{
    public class Inventory : BaseModel
    {

        public Inventory(Location location)
        {
            Id = Guid.NewGuid();
            Items = new();            
            Location = location;

        }

        public Guid Id { get; set; }
        private Location _location;
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
        private ObservableCollection<InventoryItem> _items = new();
        public ObservableCollection<InventoryItem> Items
        {
            get => _items;
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged(nameof(Items));
                }
            }
        }

     
   
       
        
    }
}
