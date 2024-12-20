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




        public async Task<List<Order>> GetOrderLocation(Location location)
        {
            // Assuming Location class has an overridden Equals method for proper comparison


            return Orders.Where(w => w.Location.Address.Equals(location.Address)).ToList();
        }

        public async Task<List<Order>> GetOrders()
        {
            // Assuming Location class has an overridden Equals method for proper comparison


            return Orders.ToList();
        }


        public async Task AcceptOrder(Order order)
        {
            Order selectedOrder = Orders.Where(w => w.Id == order.Id).SingleOrDefault();
            selectedOrder.AcceptedDate = DateTime.Now;
            selectedOrder.Status=Status.Accepted;       
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
                Products = new ObservableCollection<Product>
                {
                    new Product { Name = "Burrito", Description = "No salsa, no avocado" },
                    new Product { Name = "Falafel", Description = "Warm wrap" ,DateTime=DateTime.UtcNow}
                },
                Messages = new ObservableCollection<Message>
                {
                    new Message { Description = "Order confirmed", DateTime = DateTime.Now.AddMinutes(-30) },
                    new Message { Description = "On the way", DateTime = DateTime.Now.AddMinutes(-15) }
                },
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
