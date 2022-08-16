using PizzaHome.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Core.Interfaces
{
    public interface IProductService
    {
        
        public  Task<IEnumerable<Product>> GetProducts();

        public Task<Product> GetProductById(int id);

        public  Task<Product> CreateProduct(Product product);

        public Task UpdateProduct(int id, Product product);

        public Task<bool> DeleteProduct(int id);  
    }
}
