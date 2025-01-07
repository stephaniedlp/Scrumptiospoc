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
            Orders = new();
            
        }

        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                if (_orders != value) {

                    _orders = value;
                    OnPropertyChanged(nameof(Orders));
                    NotifyStateChanged();
                }
            }
        }

        public event Action? StateChanged;

        private void NotifyStateChanged()
        {
            StateChanged?.Invoke();
        }




        public async Task<ObservableCollection<Order>> GetOrderLocation(Location location)
        {
            
            ObservableCollection<Order> orders = new ObservableCollection<Order>(Orders.Where(w => w.Location.Id.Equals(location.Id)));

            return orders;
        }

        public async Task<List<Order>> GetOrders()
        {
          

            return Orders.ToList();
        }


        public async Task AcceptOrder(Order order)
        {

            order.Status=Status.Accepted;       
            OnPropertyChanged(nameof(Orders));
            NotifyStateChanged();
        }
        public async Task RejectOrder(Order order)
        {
            Order selectedOrder = Orders.Where(w => w.Id == order.Id).SingleOrDefault();
            selectedOrder.RejectedDate = DateTime.Now;
            selectedOrder.Status=Status.Rejected;
            OnPropertyChanged(nameof(Orders));
            NotifyStateChanged();
        }
        public async Task SetReadyOrder(Order order)
        {
            Order selectedOrder = Orders.Where(w => w.Id == order.Id).SingleOrDefault();
            selectedOrder.FinishedDate = DateTime.Now;
            selectedOrder.Status = Status.Finished;
            OnPropertyChanged(nameof(Orders));
            NotifyStateChanged();

        }
        public async Task Delivered(Order order)
        {
            Order selectedOrder = Orders.Where(w => w.Id == order.Id).SingleOrDefault();
            selectedOrder.DeliveredDate = DateTime.Now;
            selectedOrder.Status=Status.Delivered;
            OnPropertyChanged(nameof(Orders));
            NotifyStateChanged();
        }
        public async Task CancelOrder(Order order)
        {
            Order selectedOrder = Orders.Where(w => w.Id == order.Id).SingleOrDefault();
            selectedOrder.CacelationDate = DateTime.Now;
            selectedOrder.Status=Status.Canceled;
            OnPropertyChanged(nameof(Orders));
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
          
            Orders.Add(order1);


            OnPropertyChanged(nameof(Orders));
            NotifyStateChanged();
            return Task.CompletedTask;
        }


    

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

     
    }
}
