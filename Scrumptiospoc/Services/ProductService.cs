using Scrumptiospoc.Interfaces;
using Scrumptiospoc.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Scrumptiospoc.Services
{
    public class ProductService : IProductInterface
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Product> _products = new();
        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                if (_products != value)
                {

                    _products = value;
                    OnPropertyChanged(nameof(Products));

                }
            }
        }

        public async  Task AddProduct()
        {
            Product product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product " + Products.Count().ToString(),
                Description = "Description for Product",
                DateTime = DateTime.Now                ,
                IsDeleted = false
            };
            Products.Add(product);
        }
       

        public async Task ArchiveProduct(Product product )
        {
            product.IsDeleted = true; 
            
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
