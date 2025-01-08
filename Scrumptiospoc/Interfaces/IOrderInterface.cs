using Scrumptiospoc.Models;
using Scrumptiospoc.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Scrumptiospoc.Interfaces
{
    public interface IOrderInterface: INotifyPropertyChanged
    {
        event Action StateChanged;
        Task<ObservableCollection<Order>> GetOrderLocation(Location location);
   
        Task AcceptOrder(Order order);
        Task RejectOrder(Order order);
        Task SetOrder(Order order);
        Task SetReadyOrder(Order order);
        Task Delivered(Order order);
        Task CancelOrder(Order order);
        Task CreateOrder(Location location);
        Task AddProductToOrder(InventoryItem prod, Order order);
        Task RemoveProductFromOrder(InventoryItem prod, Order order); 

    }
}
