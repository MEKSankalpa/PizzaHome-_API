using PizzaHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Services.Interfaces
{
    public interface IProductRepository
    {
        public  Task<IEnumerable<Product>> GetProducts();

        public  Task<Product> PostProduct(Product product);

        public Task<Product> PutProduct(int id, Product product);

        public Task<bool> DeleteProduct(int id);
    }
}
