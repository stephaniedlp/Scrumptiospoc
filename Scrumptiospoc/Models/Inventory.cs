namespace Scrumptiospoc.Models
{
    public class Inventory : BaseModel
    {
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
        public Inventory(Location location)
        {
            Id = Guid.NewGuid();
            
            Items = location.Inventory.Items;
            Location = location;

        }
        public Inventory ()
        {
            Id = Guid.NewGuid();
        }
        //public Inventory(Location location)
        //{
        //    Location = location;
        //}
        
    }
}
