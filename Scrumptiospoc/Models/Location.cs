namespace Scrumptiospoc.Models
{
    public class Location :BaseModel
    {
        public Guid Id { get; set; }
        private string _name =string.Empty;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name)); // Notify UI about the change
                }
            }
        }
        private string _address= string.Empty;
        public string Address {  
        
            get => _address;
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged(nameof(Address)); // Notify UI about the change
                }
            }
        }
        public double Latitude { get; set; }= 0;
        public double Longitude { get; set; }= 0;
        public bool IsActive { get; set; }
        public bool IsSlow { get; set; }
        public bool IsArchived { get; set; }
        public Inventory Inventory { get; set; }
        public Location( )
        {
            Id = Guid.NewGuid();
            Inventory= new(this);
        }
        

    }
}
