using System.Collections.ObjectModel;

namespace Scrumptiospoc.Models
{
    public class Platform : BaseModel
    {
        public Platform()
        {

        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string UrlToken { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;        
        public ObservableCollection<Order> Orders { get; set; } = new();
    }
}
