using Microsoft.EntityFrameworkCore;
using PizzaHome.DataAccess;
using PizzaHome.Models;
using PizzaHome.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Services.Services
{
    public class ProductService : IProductRepository
    {
        private readonly PizzaContext _context = new PizzaContext();

        public async Task<IEnumerable<Product>> GetProducts() {
            var products = await _context.Product.ToListAsync();
            return products;
        }

        public async Task<Product> PostProduct(Product product) {
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> PutProduct(int id, Product product) {

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                
                    throw e;
              
            }

            return product;
        }

        public async Task<Boolean> DeleteProduct(int id) {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
