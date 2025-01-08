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
        public async Task SetReadyOrder(Order order)
        {
            
            order.FinishedDate = DateTime.Now;
            order.Status = Status.Finished;
            
            NotifyStateChanged();

        }
        public async Task Delivered(Order order)
        {
           
            order.DeliveredDate = DateTime.Now;
            order.Status=Status.Delivered;
            
            NotifyStateChanged();
        }
        public async Task CancelOrder(Order order)
        {
            
            order.CacelationDate = DateTime.Now;
            order.Status=Status.Canceled;
            
            NotifyStateChanged();
        }


        public Task CreateOrder(Location location)
        {
            Order order1 = new Order
            {
                Id = Guid.NewGuid(),
                DriverId = "Driver1",
                AssignedOrderId = 101,
                OrderItems = new ObservableCollection<InventoryItem>(),                
                CreationDate = DateTime.Now,
                AcceptedDate = DateTime.Now,
                FinishedDate = DateTime.Now,
                RejectedDate = DateTime.Now,
                DeliveredDate = DateTime.Now,
                Location = location,
                Status = Status.Created
            };

            location.Orders.Add(order1);


            
            NotifyStateChanged();
            return Task.CompletedTask;
        }


    

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

     
    }
}
