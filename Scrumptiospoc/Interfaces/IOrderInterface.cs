using Scrumptiospoc.Models;
using Scrumptiospoc.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Scrumptiospoc.Interfaces
{
    public interface IOrderInterface: INotifyPropertyChanged
    {
        event Action StateChanged;
        Task<List<Order>> GetOrderLocation(Location location);
        Task<List<Order>> GetOrders();
        Task AcceptOrder(Order order);
        Task RejectOrder(Order order);
        Task SetReadyOrder(Order order);
        Task Delivered(Order order);
        Task CancelOrder(Order order);
        Task CreateOrder(Location location);

        
        
        public ObservableCollection<Order> Orders
        {
            get; set;
        }

     
    }
}
