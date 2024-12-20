using Scrumptiospoc.Models;
using System.Collections.ObjectModel;

namespace Scrumptiospoc.Interfaces
{
    public interface IProductInterface
    {
        public ObservableCollection<Product> Products { get; set; }
        public Task ArchiveProduct(Product product );
        public Task AddProduct();
    }
}
