using Scrumptiospoc.Interfaces;
using Scrumptiospoc.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;

namespace Scrumptiospoc.Services
{
    public class OrderService : IOrderInterface
    {
        public event PropertyChangedEventHandler PropertyChanged;


        
        public OrderService()
        {
          
           
        }  

        public event Action? StateChanged;

        private void NotifyStateChanged()
        {
            StateChanged?.Invoke();
        }
        public async Task<ObservableCollection<Order>> GetOrderLocation(Location location)
        {
            ObservableCollection<Order> orders = location.Orders;
            return orders;
        }


        public async Task AddProductToOrder(InventoryItem prod, Order order)
        {
            if (prod.Quantity >= 1)
            {
                order.OrderItems.Add(prod);
                prod.Quantity -= 1;
                NotifyStateChanged();
            }
            
           
        }

        public async Task RemoveProductFromOrder(InventoryItem item, Order order)
        {
            order.OrderItems.Remove(item);
            NotifyStateChanged();
        }

        public async Task SetOrder(Order order)
        {
            order.Status = Status.IsSet;
            order.CreationDate=DateTime.Now;
            NotifyStateChanged();
        }

        public async Task AcceptOrder(Order order)
        {
            order.Status=Status.Accepted; 
            order.AcceptedDate=DateTime.Now;            
            NotifyStateChanged();
            
        }
        public async Task RejectOrder(Order order)
        {            
            order.RejectedDate = DateTime.Now;
            order.Status=Status.Rejected;            
            NotifyStateChanged();
        }
        public async Task CancelOrder(Order order)
        {
            order.CancelationDate = DateTime.Now;
            order.Status = Status.Canceled;
            NotifyStateChanged();
        }
        public async Task SetReadyOrder(Order order)
        {            
            order.FinishedDate = DateTime.Now;
            order.Status = Status.Finished;  

            if(order.Location.Orders.Count() <=20)
                order.Location.IsSlow = false;

            NotifyStateChanged();
        }
        public async Task Delivered(Order order)
        {           
            order.DeliveryDate = DateTime.Now;
            order.Status=Status.Delivered;            
            NotifyStateChanged();
        }
       


        public async Task CreateOrder(Location location)
        {
            Order order = new Order(location);
            location.Orders.Add(order);
            if (await CountOrders(location) >= 20)
                location.IsSlow = true;        
            NotifyStateChanged();            
        }


        public async Task<int> CountOrders(Location location)
        {            
            return location.Orders.Where(o => o.Status == Status.IsSet).Count();    
        }
    

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

     
    }
}
