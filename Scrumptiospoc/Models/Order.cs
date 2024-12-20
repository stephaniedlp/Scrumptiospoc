using System.Collections.ObjectModel;

namespace Scrumptiospoc.Models
{
    public class Order: BaseModel
    {
        public Guid Id { get; set; }
        public string DriverId { get; set; }
        public int AssignedOrderId { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Message> Messages { get; set; }   
        public DateTime CreationDate { get; set; }
        public DateTime AcceptedDate { get; set; }
        public DateTime RejectedDate { get; set; }
        public DateTime FinishedDate { get; set; }
        public DateTime CacelationDate { get; set; }
        public DateTime DeliveredDate { get; set; }
        public Location Location { get; set; }
        public Guid LocationId { get; set; }
        public Status Status { get; set; }

    }

    public enum Status
    {
        Created,
        Accepted,
        Rejected,
        Canceled,
        Finished,
        Delivered
    }
  
}
