using Dapper;
using PizzaHome.Core.Entities;
using PizzaHome.Core.Interfaces;
using PizzaHome.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly DbService _service;

        public ProductService(DbService service)
        {
            _service = service;
        }

        
        public async Task<IEnumerable<Product>> GetProducts() {

            string query = "SELECT * FROM public.products";
            var result = await _service.GetAll<Product>(query, new { });
            return result;

        }

        public async Task<Product> GetProductById(int id)
        {
            var query = "SELECT * FROM public.products WHERE id = @id";
            var result = await _service.Get<Product>(query, new { id });
            return result;


        }

        public async Task<Product> CreateProduct(Product product) {
            var query = "INSERT INTO public.products (name, description, price, categoryId ) VALUES (@Name, @Description, @Price, @CategoryId)"; 

            int id = await _service.CreateAndEdit(query, product);
            var createdResult = new Product
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
            };
            return createdResult;

        }

        public async Task UpdateProduct(int id, Product product) {

            var query = "UPDATE public.products SET name = @Name, description = @Description, price = @Price, categoryId =@CategoryId   WHERE id = @id";
            await _service.CreateAndEdit(query, product);
        }

        public async Task<bool> DeleteProduct(int id) {
         
            var query = "DELETE FROM public.products WHERE id = @id";
            var Id = await _service.CreateAndEdit(query, new { id });

            if (Id == 0)
            {
                return false;
            }

            return true;


        }
        
    }
}
