using System.Collections.ObjectModel;

namespace Scrumptiospoc.Models
{
    public class Order: BaseModel
    {
        public Order(Location location) {
            Guid Id = Guid.NewGuid();
            Status = Status.NotSet;
            Location = location;
            LocationId= location.Id;
            CreationDate=DateTime.Now;
            AcceptedDate = null;
            RejectedDate = null;
            FinishedDate = null;
            CancelationDate = null;
            DeliveryDate= null;
            OrderItems = new();
        }


        public Guid Id { get; set; } = Guid.NewGuid();     
        

        private ObservableCollection<InventoryItem> _items = new();
        public ObservableCollection<InventoryItem> OrderItems
        {
            get => _items;
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged(nameof(OrderItems));
                }
            }
        }
        public ObservableCollection<Message> Messages { get; set; } = new(); 
        public string UserName {  get; set; }   
        public string DriverName { get; set; }
        public bool IsDriverAssigned { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public DateTime? RejectedDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public DateTime? CancelationDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public Location Location { get; set; }
        public Guid LocationId { get; set; }
        public Platform Platform { get; set; }
        public Guid PlatformId { get; set; }
        public Status Status { get; set; }
       


    }

    public enum Status
    {
        NotSet,
        IsSet,
        Created,
        Accepted,
        Rejected,
        Canceled,
        Finished,
        Delivered
    }
  
}
