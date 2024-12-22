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
        public List<InventoryItem> Items { get; set; }
     
   
       
        
    }
}
